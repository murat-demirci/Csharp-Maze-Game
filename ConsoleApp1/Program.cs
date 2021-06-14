using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            IMainUserInterface userInterface = new MainUserInterface();
            IGameLoop gameLoop = new GameLoop();
            IGame game = new Game(21, 35, '@', 10);

            userInterface.StartGame(gameLoop, game);
        }
    }
    
        enum Direction
        {
            Top,
            Right,
            Bottom,
            Left
        }

    public interface IMainUserInterface
    {
        void StartGame(IGameLoop gameLoop, IGame game);
    }

    // Game process
    public interface IGameLoop
    {
        void Run(IGame game);
    }

    // This interface should be implemented by game object
    public interface IGame
    {
        void Initialize();
        void DisplayField();
        void DisplayPlayer();
        void HandleKey(ConsoleKeyInfo cki);
        bool IsWon();
    }

    class MainUserInterface : IMainUserInterface
    {
        public void StartGame(IGameLoop gameLoop, IGame game)
        {
            Console.CursorVisible = false;

            do
            {
                string key;
                do
                {
                    Console.WriteLine("1. New game");
                    Console.WriteLine("2. Quit");
                    key = Console.ReadLine();
                    Console.Clear();
                } while (key != "1" && key != "2");
                switch (key)
                {
                    case "1":
                        game.Initialize();
                        gameLoop.Run(game);
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            } while (true);
        }
    }
    class GameLoop : IGameLoop
    {
        Stopwatch stopwatch;

        public void Run(IGame game)
        {
            stopwatch = new Stopwatch();

            // Display generation of a labyrinth
            game.DisplayField();

            // Start timer
            stopwatch.Start();

            do
            {
                if (Console.KeyAvailable == true)
                {
                    game.HandleKey(Console.ReadKey(true));
                    game.DisplayPlayer();
                }
            } while (!game.IsWon());

            // Stop timer
            stopwatch.Stop();

            Thread.Sleep(500);

            TimeSpan time = stopwatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}.{2:00}", time.Minutes, time.Seconds, time.Milliseconds / 10);

            // Show elapsed time
            Console.WriteLine("\nYou've completed a labyrinth in {0}!\n", elapsedTime);
            Console.WriteLine("Press any button to continue or Escape to quit to menu.");

            ConsoleKeyInfo cki = Console.ReadKey(true);
            Console.Clear();

            if (cki.Key != ConsoleKey.Escape)
            {
                game.Initialize();
                Run(game);
            }
        }
    }
    class Game : IGame
    {
        public int Height { get; }
        public int Width { get; }
        public char PlayerSym { get; }
        public int GenSpeed { get; }

        Labyrinth labyrinth;
        Player player;

        Cell Start { get; }
        Cell End { get; }

        public Game(int height, int width, char playerSym, int genSpeed)
        {
            Height = height;
            Width = width;
            PlayerSym = playerSym;
            GenSpeed = genSpeed;

            Initialize();

            Start = new Cell(1, 1);
            End = new Cell(Height - 2, Width - 2);
        }

        public void Initialize()
        {
            labyrinth = new Labyrinth(Height, Width, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue);
            player = new Player(PlayerSym, labyrinth.Walls);
        }

        // Display field, each time generating a new one
        public void DisplayField()
        {
            labyrinth.Generate(GenSpeed);

            Start.Display(ConsoleColor.Green, ConsoleColor.Green);
            End.Display(ConsoleColor.Red, ConsoleColor.Red);
        }

        // Display player cell
        public void DisplayPlayer()
        {
            player.Display(ConsoleColor.Green, ConsoleColor.DarkMagenta);
        }

        // Move player
        public void HandleKey(ConsoleKeyInfo cki)
        {
            player.Erase(labyrinth.FieldColor, labyrinth.FieldColor);
            player.HandleKey(cki);
        }

        // Game is won when player gets to end cell
        public bool IsWon()
        {
            return player.IsCollidingWith(End);
        }
    }
    abstract class Point : Color
    {
        // Point value and coordinates
        public virtual char Value { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }

        public Point(char value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
        }

        // Display point with certain colors
        public virtual void Display(ConsoleColor fgColor, ConsoleColor bgColor)
        {
            Console.SetCursorPosition(Y, X);
            ColorDisplay(Value.ToString(), fgColor, bgColor);
        }

        // Erase point with certain colors
        public virtual void Erase(ConsoleColor fgColor, ConsoleColor bgColor)
        {
            Console.SetCursorPosition(Y, X);
            ColorDisplay(" ", fgColor, bgColor);
        }

        // Check if point colliding with another point
        public virtual bool IsCollidingWith(Point p)
        {
            return X == p.X && Y == p.Y;
        }
    }
    class Cell : Point
    {
        // 4 walls on each direction
        public bool[] walls;

        public bool isVisited;

        public Cell(int x, int y) : base(' ', x, y)
        {
            walls = new bool[] { true, true, true, true };

            isVisited = false;
        }

        public void Display(ConsoleColor bgColor)
        {
            // Not displaying cell that hasn't been visited
            if (isVisited)
                Display(Console.ForegroundColor, bgColor);
            else
                Display(Console.ForegroundColor, Console.BackgroundColor);

            // Displaying available walls around each cell
            DisplayWalls(bgColor);
        }

        void DisplayWalls(ConsoleColor bgColor)
        {
            Cell c = null;

            for (int i = 0; i < walls.Length; i++)
            {
                if (i == (int)Direction.Top)
                    c = new Cell(X - 1, Y);
                else if (i == (int)Direction.Right)
                    c = new Cell(X, Y + 1);
                else if (i == (int)Direction.Bottom)
                    c = new Cell(X + 1, Y);
                else if (i == (int)Direction.Left)
                    c = new Cell(X, Y - 1);

                // If wall is enabled - don't display anything
                // If wall is disabled - display as normal cell
                if (walls[i])
                    c.Display(Console.ForegroundColor, Console.BackgroundColor);
                else
                    c.Display(Console.ForegroundColor, bgColor);
            }
        }

        public void Highlight()
        {
            Display(ConsoleColor.Green, ConsoleColor.Green);
        }
    }
    class Player : Point
    {
        List<Cell> walls;

        // Walls initialization, value and coordinates go to base constructor
        public Player(char value, List<Cell> walls) : base(value, 1, 1)
        {
            this.walls = walls;
        }

        // Handle pressed key
        public void HandleKey(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Y += 1;
                    if (IsCollidingWithWall())
                        Y -= 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Y -= 1;
                    if (IsCollidingWithWall())
                        Y += 1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    X += 1;
                    if (IsCollidingWithWall())
                        X -= 1;
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    X -= 1;
                    if (IsCollidingWithWall())
                        X += 1;
                    break;
            }
        }

        // Check if walls list contains player coordinates 
        bool IsCollidingWithWall()
        {
            return walls.Any(c => c.IsCollidingWith(this));
        }
    }
    abstract class Color
    {
        // Any hereditary class can use this function to display string in color
        protected void ColorDisplay(string str, ConsoleColor fgColor, ConsoleColor bgColor)
        {
            ConsoleColor defaultFg = Console.ForegroundColor;
            ConsoleColor defaultBg = Console.BackgroundColor;

            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            Console.Write(str);

            Console.ForegroundColor = defaultFg;
            Console.BackgroundColor = defaultBg;
        }
    }
    class Labyrinth
    {
        // Field parameters
        public int Height { get; }
        public int Width { get; }

        // Cells for player to move on
        public List<Cell> Cells { get; }
        // Walls to form an actual labyrinth
        public List<Cell> Walls { get; }

        // Beginning generation with this cell
        Cell currentCell;

        // Stack for recursive generation algorithm
        Stack<Cell> stack = new Stack<Cell>();

        // Colors
        public ConsoleColor FieldColor { get; }
        public ConsoleColor WallsColor { get; }

        public Labyrinth(int height, int width, ConsoleColor fieldColor, ConsoleColor wallsColor)
        {
            Cells = new List<Cell>();
            Walls = new List<Cell>();

            // Setting an odd number even if it's even
            Height = height % 2 == 0 ? height - 1 : height;
            Width = width % 2 == 0 ? width - 1 : width;

            // Adding cells
            for (int i = 1; i < Height; i += 2)
            {
                for (int j = 1; j < Width; j += 2)
                {
                    Cells.Add(new Cell(i, j));
                }
            }

            // Adding walls
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Walls.Add(new Cell(i, j));
                }
            }

            // Adding colors
            FieldColor = fieldColor;
            WallsColor = wallsColor;

            // Always beginning generation with a first cell (1, 1)
            currentCell = Cells.First();
        }

        // Generating a labyrinth with latency to see the process
        public void Generate(int latency)
        {
            do
            {
                // Always marking current cell as visited
                currentCell.isVisited = true;

                // Getting random neighbor cell as a next one
                Cell nextCell = GetNeighbor(currentCell);

                // If there is at least one available neighbor - remove walls between current and next cell
                if (nextCell != null)
                    RemoveWalls(currentCell, nextCell);

                // Removing wall that is equal to current cell
                foreach (Cell wall in Walls)
                {
                    if (wall.X == currentCell.X && wall.Y == currentCell.Y)
                    {
                        Walls.Remove(wall);
                        break;
                    }
                }

                currentCell.Display(FieldColor);

                // If there is available next cell - pushing current cell to stack and assigning next to current
                // Else - backtracking to cell that has at least one available neighbor
                if (nextCell != null)
                {
                    stack.Push(currentCell);
                    currentCell = nextCell;
                }
                else if (stack.Count > 0)
                    currentCell = stack.Pop();

                // Highlight current cell
                currentCell.Display(ConsoleColor.Green, ConsoleColor.Green);

                Thread.Sleep(latency);

                // Algorithm is done when current cell is back at the beginning
            } while (!IsCompleted());

            // Display walls
            foreach (Cell c in Walls)
                c.Display(WallsColor, WallsColor);
        }

        void RemoveWalls(Cell a, Cell b)
        {
            // Assigning coordinates of a wall between a and b
            int x = (a.X != b.X) ? (a.X > b.X ? a.X - 1 : a.X + 1) : a.X;
            int y = (a.Y != b.Y) ? (a.Y > b.Y ? a.Y - 1 : a.Y + 1) : a.Y;

            // Removing wall
            foreach (Cell wall in Walls)
            {
                if (wall.X == x && wall.Y == y)
                {
                    Walls.Remove(wall);
                    break;
                }
            }

            // Disabling corresponding wall for each cell
            if (a.X - b.X == 2)
            {
                a.walls[(int)Direction.Top] = false;
                b.walls[(int)Direction.Bottom] = false;
            }
            else if (a.X - b.X == -2)
            {
                a.walls[(int)Direction.Bottom] = false;
                b.walls[(int)Direction.Top] = false;
            }

            if (a.Y - b.Y == 2)
            {
                a.walls[(int)Direction.Left] = false;
                b.walls[(int)Direction.Right] = false;
            }
            else if (a.Y - b.Y == -2)
            {
                a.walls[(int)Direction.Right] = false;
                b.walls[(int)Direction.Left] = false;
            }
        }

        Cell GetNeighbor(Cell cell)
        {
            Random rand = new Random();

            List<Cell> neighbors = new List<Cell>();

            // Assigning available neighbor cells
            Cell top = (cell.X - 2 > 0) ? Cells.Find(c => c.X == cell.X - 2 && c.Y == cell.Y) : null;
            Cell right = (cell.Y + 2 < Width - 1) ? Cells.Find(c => c.Y == cell.Y + 2 && c.X == cell.X) : null;
            Cell bottom = (cell.X + 2 < Height - 1) ? Cells.Find(c => c.X == cell.X + 2 && c.Y == cell.Y) : null;
            Cell left = (cell.Y - 2 > 0) ? Cells.Find(c => c.Y == cell.Y - 2 && c.X == cell.X) : null;

            if (top != null && !top.isVisited)
            {
                neighbors.Add(top);
            }
            if (right != null && !right.isVisited)
            {
                neighbors.Add(right);
            }
            if (bottom != null && !bottom.isVisited)
            {
                neighbors.Add(bottom);
            }
            if (left != null && !left.isVisited)
            {
                neighbors.Add(left);
            }

            // Returning random neigbor from a list
            if (neighbors.Count > 0)
            {
                int index = rand.Next(neighbors.Count);
                return neighbors[index];
            }
            // Else return no neighbor
            return null;
        }

        // If stack is empty then labyrinth is generated successfully
        bool IsCompleted()
        {
            return stack.Count == 0;
        }
    }


}




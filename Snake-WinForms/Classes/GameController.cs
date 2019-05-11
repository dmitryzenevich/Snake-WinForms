using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Classes
{
    public class GameController
    {
        private static GameController instance;


        private Snake snake;
        private Food[] foods;
        private const int rectWidth = 600;
        private const int rectHeight = 600;

        private const int startTailCount = 3;
        private const int startFoodCount = 3;
        private int xMax;
        private int yMax;
        private int cellSize;
        private bool isStepLocked = false;
        private Random random;

        private Brush brushSnakeHead;
        private Brush brushSnakeTail;
        private Brush brushFood;
        private Pen penArea;
        private Pen penGrid;

        private List<IFigure> figures;

        public event Action Redraw;
        public event Action EatFoodEvent;
        public event Action ResetEvent;

        public static GameController Instance => instance ?? (instance = new GameController());

        protected GameController()
        {
            Init();
        }
        private void Init()
        {
            random = new Random(DateTime.Now.Millisecond);
            cellSize = rectWidth / 10;
            xMax = rectWidth / cellSize;
            yMax = rectHeight / cellSize;

            brushSnakeHead = Brushes.LawnGreen;
            brushSnakeTail = Brushes.Aqua;
            brushFood = Brushes.Crimson;
            penArea = new Pen(Color.Coral, 5);
            penGrid = new Pen(Color.Coral, 1);

            figures = new List<IFigure>();

            foods = new Food[startFoodCount];
            for (int i = 0; i < foods.Length; i++)
                foods[i] = new Food(cellSize, brushFood);

            snake = new Snake(cellSize, brushSnakeHead, brushSnakeTail, startTailCount);
            figures.AddRange(foods);
            figures.AddRange(snake.Tail);

            for (int i = 0; i < foods.Length; i++)
                SetFoodNewPosition(i);

            snake.SetDirection(Vector2.right);
        }
        public void GameLoop()
        {
            snake.Move();
            CheckArea();
            isStepLocked = false;
            CheckFood();
            CheckTail();
        }

        public void SetDirection(Vector2 direction)
        {
            if (isStepLocked)
                return;
            snake.SetDirection(direction);
            isStepLocked = true;
        }

        public void DrawObjects(Graphics eGraphics)
        {
            DrawArea(eGraphics);
            for (int i = figures.Count - 1; i >= 0; i--)
                figures[i].Draw(eGraphics);
        }

        private void DrawArea(Graphics graphics)
        {
            Point[] points = new Point[]
            {
                new Point(0, 0),
                new Point(rectWidth, 0),
                new Point(rectWidth, rectHeight),
                new Point(0, rectHeight),
                new Point(0, 0),
            };
            for (int i = 0; i < points.Length - 1; i++)
            {
                graphics.DrawLine(penArea, points[i], points[i + 1]);
            }

            Pen penGrid = new Pen(Color.Coral, 1);
            for (int i = 1; i < 10; i++)
            {
                graphics.DrawLine(penGrid,
                    new Point(i * (int)(rectWidth*0.1f), 0),
                    new Point(i * (int)(rectWidth * 0.1f), rectWidth));
                graphics.DrawLine(penGrid, 
                    new Point(0, i * (int)(rectHeight * 0.1f)), 
                    new Point(rectWidth, i * (int)(rectHeight * 0.1f)));
            }

        }

        private void CheckArea()
        {
            if (snake.Position.x >= xMax)
                snake.Position = new Vector2(0, snake.Position.y);
            else if (snake.Position.x < 0)
                snake.Position = new Vector2(xMax - 1, snake.Position.y);

            if (snake.Position.y >= yMax)
                snake.Position = new Vector2(snake.Position.x, 0);
            else if (snake.Position.y < 0)
                snake.Position = new Vector2(snake.Position.x, yMax - 1);
        }

        private void CheckFood()
        {
            for (int i = 0; i < foods.Length; i++)
            {
                if (foods[i].Position == snake.Position)
                {
                    IFigure figure = snake.AddToTail();
                    figures.Add(figure);
                    snake.PushToTail(snake.Position);
                    SetFoodNewPosition(i);
                    EatFoodEvent?.Invoke();
                }
            }
        }

        private void SetFoodNewPosition(int index)
        {
            while (true)
            {
                int x = random.Next(0, xMax);
                int y = random.Next(0, yMax);
                Vector2 randomPosition = new Vector2(x, y);
                bool overlap = false;
                for (int i = 1; i < snake.Tail.Count; i++)
                    if (randomPosition == snake.Tail[i].Position) overlap = true;

                for (int i = 0; i < foods.Length; i++)
                    if (randomPosition == foods[i].Position) overlap = true;

                if (!overlap)
                    foods[index].Position = randomPosition;
                else
                    continue;
                break;
            }
        }

        private void CheckTail()
        {
            for (int i = 2; i < snake.Tail.Count; i++)
            {
                if (snake.Tail[0].Position == snake.Tail[i].Position)
                    Reset();
            }
        }

        private void Reset()
        {
            //for (int i = 0; i < snake.Tail.Count; i++)
            //    snake.Tail[i].Position = Vector2.zero;
            figures.Clear();
            snake = null;
            snake = new Snake(cellSize, brushSnakeHead, brushSnakeTail, startTailCount);

            figures.AddRange(foods);
            figures.AddRange(snake.Tail);

            snake.SetDirection(Vector2.right);

            for (int i = 0; i < foods.Length; i++)
                SetFoodNewPosition(i);

            ResetEvent?.Invoke();
        }
    }
}

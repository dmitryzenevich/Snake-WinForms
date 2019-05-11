using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Interfaces;

namespace Snake.Classes
{
    class Snake : IMoveable
    {
        private Vector2 size;
        private Brush brushHead;
        private Brush brushTail;

        public List<IFigure> Tail { get; }

        public Vector2 Direction { get; private set; }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                PushToTail(position);
            }
        }

        public Snake(int sizePartOfSnake, Brush brushHead, Brush brushTail, int tailLenght = 3)
        {
            this.size = new Vector2(sizePartOfSnake, sizePartOfSnake);
            this.brushHead = brushHead;
            this.brushTail = brushTail;

            Tail = new List<IFigure>();
            Tail.Add(new SnakePart(size, this.brushHead));
            for (int i = 0; i < tailLenght; i++)
                AddToTail();

            Position = Vector2.zero;
            Direction = Vector2.zero;
        }
        public IFigure AddToTail()
        {
            IFigure figure = new SnakePart(size, brushTail);
            Tail.Add(figure);
            return figure;
        }

        public void Move()
        {
            Position += Direction;
            //PushToTail(Position);
        }

        public void PushToTail(Vector2 pos)
        {
            for (int i = Tail.Count - 1; i > 0; i--)
                Tail[i].Position = Tail[i - 1].Position;

            Tail[0].Position = pos;
        }

        public void SetDirection(Vector2 direction)
        {
            Vector2 vector2 = Direction + direction;
            if (vector2 == Vector2.zero)
                return;
            Direction = direction;
        }
    }
}

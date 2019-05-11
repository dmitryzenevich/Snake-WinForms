using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakePart : IFigure
    {
        private Rectangle rectangle;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }
        public Brush Brush { get; }
        public SnakePart(Vector2 size, Brush brush)
        {
            Size = size;
            Brush = brush;
        }
        public SnakePart(Vector2 position, Vector2 size, Brush brush)
        {
            Position = position;
            Size = size;
            Brush = brush;
            rectangle = new Rectangle((int)Position.x * (int)Size.x, (int)Position.y * (int)Size.y, (int)Size.x, (int)Size.y);
        }
        public void Draw(Graphics graphics)
        {
            rectangle = new Rectangle((int)Position.x * (int)Size.x, (int)Position.y * (int)Size.y, (int)Size.x, (int)Size.y);
            graphics.FillRectangle(Brush, rectangle);
        }
    }
}

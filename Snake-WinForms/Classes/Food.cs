using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Classes
{
    class Food: IFigure
    {
        private Rectangle rectangle;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }
        public Brush Brush { get; }
        public Food(int size, Brush brush)
        {
            Size = new Vector2(size, size);
            Brush = brush;
            rectangle = new Rectangle((int)Position.x * (int)Size.x, (int)Position.y * (int)Size.y, (int)Size.x, (int)Size.y);
        }
        public void Draw(Graphics graphics)
        {
            rectangle = new Rectangle((int)Position.x * (int)Size.x, (int)Position.y * (int)Size.y, (int)Size.x, (int)Size.y);
            graphics.FillEllipse(Brush, rectangle);
        }
    }
}

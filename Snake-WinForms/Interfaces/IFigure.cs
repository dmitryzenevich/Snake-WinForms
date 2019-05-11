using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    interface IFigure
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; }
        Brush Brush { get; }
        void Draw(Graphics graphics);
    }
}

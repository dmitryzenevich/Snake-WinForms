using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Interfaces
{
    interface IMoveable
    {
        Vector2 Direction { get; }
        void Move();
        void SetDirection(Vector2 direction);
    }
}

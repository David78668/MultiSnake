using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSnake.Client.Game
{
    public interface ISnakePart
    {
        public void Move(Point newLocation);
        public Point Point { get; set; }
        public ISnakePart? Next { get; set; }
        public void Render(Graphics g);
    }
}

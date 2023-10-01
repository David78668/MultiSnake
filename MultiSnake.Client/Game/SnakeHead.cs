using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSnake.Client.Game
{
    public class SnakeHead : ISnakePart
    {
        public Point Point { get; set; }
        public ISnakePart? Next { get ; set ; }

        public void Move(Point newLocation)
        {
            var current = Point;
            Point = newLocation;

            if(Next != null)
                Next.Move(current);
        }

        public const int HEAD_SIZE = 24;

        public void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Red, Point.X, Point.Y, HEAD_SIZE, HEAD_SIZE);

            if (Next != null)
                Next.Render(g);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSnake.Client.Game
{
    public class SnakePart : ISnakePart
    {
        public Point Point { get; set; }
        public ISnakePart? Next { get; set; }

        public void Move(Point newLocation)
        {
            var current = Point;
            Point = newLocation;

            if (Next != null)
                Next.Move(current);
        }

        public const int PART_SIZE = 24;

        public void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Black, Point.X, Point.Y,PART_SIZE,PART_SIZE);

            if(Next != null)
                Next.Render(g);
        }
    }
}

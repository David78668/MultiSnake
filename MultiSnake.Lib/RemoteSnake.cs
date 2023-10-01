using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSnake.Lib
{
    public class RemoteSnake
    {
        public Guid Id { get; set; }
        public Point[] Parts { get; set; }
    }
}

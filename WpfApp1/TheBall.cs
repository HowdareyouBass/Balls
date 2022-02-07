using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    class TheBall
    {
        public Vector velocity = new Vector(300,0);
        public Vector acceleration = new Vector(0,700);
        public Point position = new Point(204, 204);
        public Point leftPoint = new Point(),
                     topPoint = new Point(),
                     rightPoint = new Point(),
                     bottomPoint = new Point();
    }
}

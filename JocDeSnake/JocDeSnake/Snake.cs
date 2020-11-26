using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JocDeSnake
{
    class Snake
    {
        public double x, y;
        public Rectangle capoSerp = new Rectangle();
        public Snake(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void posicioSnake()
        {
            capoSerp.Width = capoSerp.Height = 20;
            capoSerp.Fill = Brushes.Green;
            Canvas.SetLeft(capoSerp, x);
            Canvas.SetTop(capoSerp, y);
        }
    }
}

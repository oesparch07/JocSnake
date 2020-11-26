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
    class Pomes
    {
        public double x, y;
        public Ellipse ellPomes = new Ellipse();
        public Pomes(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void posicioPomes()
        {
            ellPomes.Width = ellPomes.Height = 20;
            ellPomes.Fill = Brushes.Red;
            Canvas.SetLeft(ellPomes, x);
            Canvas.SetTop(ellPomes, y);
        }
    }
}

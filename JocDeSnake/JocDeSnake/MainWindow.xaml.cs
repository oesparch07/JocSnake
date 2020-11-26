using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JocDeSnake
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        List<Snake> capSnake;//Array de Snakes per a l'hora de afeguir-ne en el canvas.
        List<Pomes> pomes;//Array de Pomes per a l'hora de afeguir-ne en el canvas.
        Random random = new Random();
        //Dimensions Snake
        double width = 100;
        double height = 100;

        //Direccio en la que es moura la Snake.
        DirrecioSnake direccio;
        public DirrecioSnake Direccio { get => direccio; set => direccio = value; }
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            capSnake = new List<Snake>();//Cap snake fa ref a una Llista de la clase Snake.
            pomes = new List<Pomes>();//pomes fa ref a una Llista de la clase Pomes.
            capSnake.Add(new Snake(width, height));//Inicialitzem la primera part de l'snake.
            pomes.Add(new Pomes(random.Next(0, 37) * 10, random.Next(0, 35) * 10));//Afeguim una poma de manera aleatoria al canvas.
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);//Velovcitat amb la que es moura l'snake. 
          
            timer.Tick += Timer_Tick;//Funcionalitat de mov de la serp.
        }

        //Métode per afeguir pomes.
        public void afeguirPomes()
        {
            //Inserima la List una poma amb les propietats de la clase Poma.
            pomes[0].posicioPomes();
            pantallaJoc.Children.Insert(0, pomes[0].ellPomes);//Inserim la poma al canvas.

        }

        //Métode per afeguir cos de la serp al canvas.
        public void afeguirSnake()
        {
            //Per a cada Snake "cos" que hi hagui en el joc.
            foreach (Snake snake in capSnake)
            {
                //Incialitzem una snake amb les seves propietats.
                snake.posicioSnake();
                pantallaJoc.Children.Add(snake.capoSerp);//Inserim una nova snake al canvas.
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            //Comprovem si hi ha direccio, es a dir si hi ha un nou capSnake per que vagui al redera del primer i així sucesivament.
            if (direccio != 0)
            {
                for (int i = capSnake.Count - 1; i > 0; i--)
                {
                    capSnake[i] = capSnake[i - 1];
                }
            }

            //Segons la direcció augemnta cap a un costat o cap a l'altre per "moures".
            if (direccio == DirrecioSnake.Up)
                height -= 10;
            if (direccio == DirrecioSnake.Down)
                height += 10;
            if (direccio == DirrecioSnake.Left)
                width -= 10;
            if (direccio == DirrecioSnake.Right)
                width += 10;

            //Quan l'snake xoca amb la poma fa el seguent.
            if (capSnake[0].x == pomes[0].x && capSnake[0].y == pomes[0].y)
            {
                capSnake.Add(new Snake(pomes[0].x, pomes[0].y));//Afeguim una nova sanke a la posicio on estava la poma.
                pomes[0] = new Pomes(random.Next(0, 37) * 10, random.Next(0, 35) * 10);//Afeguim una nova poma al joc de manera aleatoria.
                pantallaJoc.Children.RemoveAt(0);//Eliminem la poma del canvas.
                afeguirPomes();//Afeguimm Poma
            }

            //Inicialitzem a cap sanke una Snake.
            capSnake[0] = new Snake(width, height);
            //Quan toca la pantalla perdem.
            if (capSnake[0].x > 370 || capSnake[0].y > 350 || capSnake[0].x < 0 || capSnake[0].y < 0)
            {
                this.Close();
            }

            //Quan es toca a si mateixa perdem.
            for (int i = 1; i < capSnake.Count; i++)
            {
                if (capSnake[0].x == capSnake[i].x && capSnake[0].y == capSnake[i].y)
                    this.Close();
            }

            //COmpte els capSnake que hi ha en el joc pq no sigui u bucle de caps tot el rato, es a dir que no es vaguin generant cada vegada rectangles.
            for (int i = 0; i < pantallaJoc.Children.Count; i++)
            {
                if (pantallaJoc.Children[i] is Rectangle)
                    count++;
            }
            pantallaJoc.Children.RemoveRange(1, count);//Eliminem el rectangle que es crea a redera.
            count = 0;//Inicialitzem el contador de rectangles (capSnake) a zero.
            afeguirSnake();//AfeguimSnake.
        }

        //Métode per moure la serp amb les feltxes del teclat.
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && direccio != DirrecioSnake.Down)
                direccio = DirrecioSnake.Up;
            if (e.Key == Key.Down && direccio != DirrecioSnake.Up)
                direccio = DirrecioSnake.Down;
            if (e.Key == Key.Left && direccio != DirrecioSnake.Right)
                direccio = DirrecioSnake.Left;
            if (e.Key == Key.Right && direccio != DirrecioSnake.Left)
                direccio = DirrecioSnake.Right;
        }

        //Clase enum on "Guardem les direccions" 
        public enum DirrecioSnake
        {
            Up,
            Down,
            Left,
            Right
        }

        private void btnInicaJoc_Click(object sender, RoutedEventArgs e)
        {
            afeguirSnake();
            afeguirPomes();
            timer.Start();
        }
    }
}

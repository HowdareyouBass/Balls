using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TheBall ball = new TheBall();
        Stopwatch stopWatch = new Stopwatch();
        bool Collision = false;
        public MainWindow()
        {
            InitializeComponent();
            Stopwatch stopWatch = new Stopwatch();
            DrawBall(104,104);
            DrawBorder();
            TimeSpan s = new TimeSpan(0, 0, 0, 0, 0);
            stopWatch.Start();
            DrawFrame(s);
            stopWatch.Stop();
            CompositionTarget.Rendering += UpdateFrame;
        }

        protected void UpdateFrame(object sender, EventArgs e)
        {
            DrawFrame(stopWatch.Elapsed);
            stopWatch.Restart();
        }

        private void DrawBall(double X1,double Y1)
        {
            Ellipse ball = new Ellipse()
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Green,
                Width = 25,
                Height = 25
            };
            ball.Margin = new Thickness(X1, Y1, 0, 0);
            Field.Children.Add(ball);
        }

        private void DrawBorder()
        {
            Rectangle border = new Rectangle() {
                Stroke = Brushes.LightGray,
                StrokeThickness = 4,
                Width = Field.Width-8,
                Height = Field.Height-8
            };
            Field.Children.Add(border);
        }
        private void DrawFrame(TimeSpan tsa)
        {
            Field.Children.Clear();
            DrawBorder();
            SetPoints();
            CheckCollision();
            if (Collision == false)
            {
                ball.velocity = Vector.Add(Vector.Multiply(tsa.TotalSeconds, ball.acceleration), ball.velocity);
            }
            ball.position = Vector.Add(Vector.Multiply(tsa.TotalSeconds, ball.velocity), ball.position);
            DrawBall(ball.position.X, ball.position.Y);
//            DrawVectors();
        }
        private void SetPoints()
        {
            ball.leftPoint = new Point(ball.position.X, ball.position.Y + 25 / 2);
            ball.rightPoint = new Point(ball.position.X + 25, ball.position.Y + 25 / 2);
            ball.topPoint = new Point(ball.position.X + 25 / 2, ball.position.Y);
            ball.bottomPoint = new Point(ball.position.X + 25 / 2, ball.position.Y + 25);
        }
        private void CheckCollision()
        {
            if(ball.leftPoint.X < 8 || ball.rightPoint.X > Field.Width - 8)
            {
                ball.velocity = new Vector(-ball.velocity.X, ball.velocity.Y);
                Collision = true;
                return;
            }
            if(ball.bottomPoint.Y > Field.Height - 8 || ball.topPoint.Y < 8)
            {
                ball.velocity = new Vector(ball.velocity.X, -ball.velocity.Y);
                Collision = true;
                return;
            }
            Collision = false;
        }
        private void DrawVectors()
        {
            Line acc = new Line()
            {
                Stroke = Brushes.Red,
                X1 = ball.position.X + 25 / 2,
                Y1 = ball.position.Y + 25 / 2,
                X2 = ball.position.X + 25 / 2 + ball.acceleration.X / 4,
                Y2 = ball.position.Y + 25 / 2 + ball.acceleration.Y / 4
            };
            Line vel = new Line()
            {
                Stroke = Brushes.Yellow,
                X1 = ball.position.X + 25 / 2,
                Y1 = ball.position.Y + 25 / 2,
                X2 = ball.position.X + 25 / 2 + ball.velocity.X / 4,
                Y2 = ball.position.Y + 25 / 2 + ball.velocity.Y / 4
            };
            Field.Children.Add(acc);
            Field.Children.Add(vel);
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

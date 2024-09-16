using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp8
{
    public class Toy : Thing
    {
        static Random r = new Random();
        public Toy()
        {
            Objects objekt = (Objects)r.Next(3);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            Speed = 2;
            switch (objekt)
            {
                case Objects.Ball:
                    Objects = Objects.Ball;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBall.png"));
                    break;
                case Objects.Bear:
                    Objects = Objects.Bear;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBear.png"));
                    break;
                case Objects.Car:
                    Objects = Objects.Car;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggCar.png"));
                    break;
            }
        }
        public override void Move()
        {
            Position = new Point(Position.X * 1.2, Position.Y + Speed);
        }
        public override void Interact(Basket basket)
        {
            base.Interact(basket);
        }
    }
}

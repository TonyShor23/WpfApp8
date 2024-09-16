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
    public class RedFood : Food
    {
        static Random r = new Random();
        public RedFood()
        {
            Objects objekt = (Objects)r.Next(2);
            Imagee = new ImageBrush();
            Position = new Point(r.Next(100, 1000), 0);
            Speed = 5;
            switch (objekt)
            {
                case Objects.Apple:
                    Objects = Objects.Apple;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggApple.png"));
                    break;
                case Objects.Tomato:
                    Objects = Objects.Tomato;
                    Imagee.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggTomato.png"));
                    break;
            }
        }
        public override void Benefit()
        {
            score++;
        }

        public override void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public override void Interact(Basket basket)
        {
            if (!basket.IsFull())
            {
                basket.AddThing(this);
                // Додайте очки за квадратний подарунок
                // ...
            }
        }
    }
}

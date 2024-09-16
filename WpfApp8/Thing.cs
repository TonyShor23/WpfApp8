using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp8
{
    public abstract class Thing
    {
        public ImageBrush Imagee { get; set; }
        public Point Position { get; set; }
        protected double Speed { get; set; }
        protected Objects Objects { get; set; }
        public virtual void Move()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }
        public virtual void Interact(Basket basket)
        {
            if (!basket.IsFull())
            {
                basket.AddThing(this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8
{
    public class Food : Thing
    {
        protected int score = 0;
        public virtual void Benefit()
        {
            score++;
        }
    }
}

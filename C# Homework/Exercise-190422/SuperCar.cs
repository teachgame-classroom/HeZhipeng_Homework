using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190422
{
    public class SuperCar: Car
    {

        public SuperCar()
        {
            engine = new SuperEngine();
        }

        public override void Drive()
        {
            if (engine.IsOn())
            {
                Console.WriteLine("开超跑");
            }
            else
            {
                Console.WriteLine("开不动超跑");
            }
        }
    }
}

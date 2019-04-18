using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190418
{
    public class CarEngine
    {
        protected bool isOn;

        public CarEngine()
        {
            isOn = false;
            Console.WriteLine("创建引擎");
        }

        public virtual void TurnOn()
        {
            isOn = true;
            Console.WriteLine("引擎发动");
        }

        public virtual void TurnOff()
        {
            isOn = false;
            Console.WriteLine("引擎熄火");
        }

        public bool IsOn()
        {
            return isOn;
        }
    }
}

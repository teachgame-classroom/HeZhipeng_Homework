using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190422
{
    public class Car
    {
        protected CarEngine engine;

        public Car()
        {
            engine = new CarEngine();
        }

        public void Start()
        {
            engine.TurnOn();
        }

        public void Stop()
        {
            engine.TurnOff();
        }

        public virtual void Drive()
        {
            if (engine.IsOn())
            {
                Console.WriteLine("开车");
            }
            else
            {
                Console.WriteLine("无法开车");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190418
{
    class Program
    {
        static void Main(string[] args)
        {
            CarEngine carEngine = new CarEngine();
            SuperEngine superEngine = new SuperEngine();

            CarEngine engine = carEngine;
            engine.TurnOn();
            engine.TurnOff();

            engine = superEngine;
            engine.TurnOn();
            engine.TurnOff();
        }
    }
}

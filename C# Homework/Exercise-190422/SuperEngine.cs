using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190422
{
    public class SuperEngine : CarEngine
    {
        private void Turbo()
        {
            Console.WriteLine("超级引擎增压");
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Turbo();
            Console.WriteLine("超级引擎发动");
        }

        public override void TurnOff()
        {
            base.TurnOff();
            Console.WriteLine("超级引擎熄火");
        }
    }
}

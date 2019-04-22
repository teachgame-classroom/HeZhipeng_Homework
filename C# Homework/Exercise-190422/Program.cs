using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190422
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();
            SuperCar superCar = new SuperCar();

            Console.Write("\n");

            car.Drive();
            car.Start();
            car.Drive();
            car.Stop();
            car.Drive();

            Console.Write("\n");

            superCar.Drive();
            superCar.Start();
            superCar.Drive();
            superCar.Stop();
            superCar.Drive();

            Console.ReadKey();
        }
    }
}

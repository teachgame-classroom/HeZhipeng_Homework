using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190319
{
    class Program
    {
        static void Main(string[] args)
        {
            Student xiaolang = new Student("小狼", "男", 17, 95, 95, 90);

            xiaolang.SayHello();
            xiaolang.PrintScore();
        }
    }
}

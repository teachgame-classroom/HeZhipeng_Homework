using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190321
{
    class Program
    {
        static void Main(string[] args)
        {
            Game maze = new Game();

            maze.start();
            while (maze.isRunning)
            {
                maze.Loop();
            }

            maze.End();
        }
    }
}

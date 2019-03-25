using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190322
{
    class Program
    {
        static void Main(string[] args)
        {
            Game maze = new Game();

            maze.Start();
            while (maze.isRunning)
            {
                maze.Loop();
            }

            maze.End();
        }
    }
}

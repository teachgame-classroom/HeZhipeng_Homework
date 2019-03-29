using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameLoopExercise_Hezhipeng
{
    class Program
    {
        private static Game game = new Game();

        static void Main(string[] args)
        {

            game.Start();
            while (game.IsRunning())
            {
                game.Update();
            }
            game.End();
        }

        private static void UpdateLoop()
        {
            while (game.IsRunning())
            {
                game.Update();
                Thread.Sleep(200);
            }
        }
    }
}

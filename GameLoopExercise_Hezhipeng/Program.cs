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
        private static Thread renderThread;
        private static Thread updateThread;

        static void Main(string[] args)
        {

            game.Start();
            StartThread();
            while (game.IsRunning())
            {
                //game.Update();
                //game.Render();
            }
            AbortThread();
            game.End();

            //Console.ReadLine();
        }

        private static void StartThread()
        {
            renderThread = new Thread(RenderLoop);
            renderThread.Start();

            updateThread = new Thread(UpdateLoop);
            updateThread.Start();
        }

        private static void RenderLoop()
        {
            while (game.IsRunning())
            {
                game.Render();
                Thread.Sleep(200);
            }
        }

        private static void UpdateLoop()
        {
            while (game.IsRunning())
            {
                game.Update();
                Thread.Sleep(200);
            }
        }

        private static void AbortThread()
        {
            if(renderThread != null)
            {
                renderThread.Abort();
            }

            if (updateThread != null)
            {
                updateThread.Abort();
            }
        }
    }
}

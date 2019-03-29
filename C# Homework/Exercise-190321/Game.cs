using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190321
{
    class Game
    {
        private Stage mazeStage;
        private ConsoleKeyInfo inputKey;
        public bool isRunning { get; private set; }
        private bool isWon;

        public void start()
        {
            mazeStage = new Stage();
            isWon = false;
            isRunning = true;
            Render();
        }

        public void Loop()
        {
            UpdateInput();
            UpdateLogic();
            Render();
        }

        public void End()
        {
            //Render();
        }

        private void UpdateInput()
        {
            inputKey = Console.ReadKey(true);
        }

        private void UpdateLogic()
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.W:
                    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY - 1);
                    break;
                case ConsoleKey.S:
                    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY + 1);
                    break;
                case ConsoleKey.A:
                    mazeStage.PlayerMove(mazeStage.playerPosX - 1, mazeStage.playerPosY);
                    break;
                case ConsoleKey.D:
                    mazeStage.PlayerMove(mazeStage.playerPosX + 1, mazeStage.playerPosY);
                    break;
                case ConsoleKey.Escape:
                    isRunning = false;
                    break;
                case ConsoleKey.R:
                    mazeStage.Reset();
                    isWon = false;
                    break;
                default:
                    break;
            }
            if(!isWon && IsWon())
            {
                isWon = !mazeStage.NextLevel();
            }
        }

        private void Render()
        {
            if (!mazeStage.isMoved)
            {
                return;
            }
            Console.Clear();
            Console.WriteLine("欢迎进入游戏,{0}为终点,移动到终点则胜利", Stage.GOAL);
            Console.WriteLine("WASD-控制方向, R-重置本关, ESC-退出");
            mazeStage.Display();
            if (isWon)
            {
                Console.WriteLine("恭喜你赢了");
            }
            switch (inputKey.Key)
            {
                //case ConsoleKey.W:
                //    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY - 1);
                //    break;
                //case ConsoleKey.S:
                //    mazeStage.PlayerMove(mazeStage.playerPosX, mazeStage.playerPosY + 1);
                //    break;
                //case ConsoleKey.A:
                //    mazeStage.PlayerMove(mazeStage.playerPosX - 1, mazeStage.playerPosY);
                //    break;
                //case ConsoleKey.D:
                //    mazeStage.PlayerMove(mazeStage.playerPosX + 1, mazeStage.playerPosY);
                //    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("感谢你的使用");
                    break;
                case ConsoleKey.R:
                    Console.WriteLine("重置完毕");
                    break;
                default:
                    break;
            }
        }

        private bool IsWon()
        {
            return mazeStage.goalPosX == mazeStage.playerPosX && mazeStage.goalPosY == mazeStage.playerPosY;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exercise_190321
{
    class Stage
    {
        public const char PLAYER = 'p';
        public const char GOAL = 'o';
        public const char WALL = ' ';

        public char[] stageCharSet { get; private set; }
        public int mazeLevel { get; private set; }
        public int col { get; private set; }
        public int row { get; private set; }
        public int playerStartPosX { get; private set; }
        public int playerStartPosY { get; private set; }
        public int playerPosX { get; private set; }
        public int playerPosY { get; private set; }
        public int goalPosX { get; private set; }
        public int goalPosY { get; private set; }
        public bool isMoved { get; private set; }

        public Stage()
        {
            Init(1);
        }

        public Stage(int level)
        {
            col = 0;
            Init(level);
        }

        public void Reset()
        {
            SetStageChar(playerPosX, playerPosY, WALL);
            SetStageChar(goalPosX, goalPosY, GOAL);
            SetStageChar(playerStartPosX, playerStartPosY, PLAYER);
            playerPosX = playerStartPosX;
            playerPosY = playerStartPosY;
            isMoved = true;
        }

        public void Init(int level)
        {
            try
            {
                ReadMapFile(level);
                mazeLevel = level;
            }
            catch (Exception)
            {
                if (stageCharSet == null || stageCharSet.Length <= 0)
                {
                    stageCharSet = new char[] { PLAYER, GOAL };
                    col = 2;
                    row = 1;
                    playerStartPosX = 0;
                    playerStartPosY = 0;
                    playerPosX = 0;
                    playerPosY = 0;
                    goalPosX = 1;
                    goalPosY = 0;
                }
            }
            isMoved = true;
        }

        private void ReadMapFile(int level)
        {
            string mapPath = string.Format("./Map_{0}.txt", level);
            try
            {
                string[] mapLines = File.ReadAllLines(mapPath);
                col = mapLines[0].Length;
                row = mapLines.Length;
                stageCharSet = new char[col * row];
                for (int y = 0; y < row; y++)
                {
                    for (int x = 0; x < col; x++)
                    {
                        char thisChar = mapLines[y][x];
                        SetStageChar(x, y, thisChar);
                        if (PLAYER.Equals(thisChar))
                        {
                            playerStartPosX = x;
                            playerStartPosY = y;
                            playerPosX = x;
                            playerPosY = y;
                        }
                        else if (GOAL.Equals(thisChar))
                        {
                            goalPosX = x;
                            goalPosY = y;
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("没有找到指定的地图文件");
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine("无法读取地图文件");
                throw e;
            }
        }

        public void PlayerMove(int newPosX, int newPosY)
        {
            if(IsWall(newPosX, newPosY))
            {
                SetStageChar(playerPosX, playerPosY, WALL);
                SetStageChar(goalPosX, goalPosY, GOAL);
                SetStageChar(newPosX, newPosY, PLAYER);
                playerPosX = newPosX;
                playerPosY = newPosY;
                isMoved = true;
            }
            else
            {
                isMoved = false;
            }
        }

        public void Display()
        {
            for(int y = 0; y < row; y++)
            {
                for(int x = 0; x < col; x++)
                {
                    Console.Write(GetStageChar(x, y));
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
        }

        public bool NextLevel()
        {
            bool hasNext = false;
            int nextLevel = mazeLevel + 1;

            Init(nextLevel);
            hasNext = nextLevel == mazeLevel;

            return hasNext;
        }

        private bool IsWall(int posX, int posY)
        {
            int index = posX + posY * col;
            return index>=0 && index <stageCharSet.Length 
                && (WALL.Equals(GetStageChar(posX, posY)) || GOAL.Equals(GetStageChar(posX, posY)));
        }

        private char GetStageChar(int x, int y)
        {
            char ret = char.MinValue;
            int index = x + y * col;
            if(index>=0 && index < stageCharSet.Length)
            {
                ret = stageCharSet[index];
            }
            return ret;
        }

        private void SetStageChar(int x, int y, char ch)
        {
            int index = x + y * col;
            if (index >= 0 && index < stageCharSet.Length)
            {
                stageCharSet[index] = ch;
            }
        }


    }
}

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
            stageCharSet = new char[] { 'p', 'o' };
            col = 2;
            row = 1;
            playerStartPosX = 0;
            playerStartPosY = 0;
            playerPosX = 0;
            playerPosY = 0;
            goalPosX = 1;
            goalPosY = 0;
            isMoved = true;
            mazeLevel = level;
            ReadMapFile();
        }

        private void ReadMapFile()
        {
            string mapPath = string.Format("./Map_{0}.txt", mazeLevel);
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

            char[] stageCharSetBak = stageCharSet;
            int mazeLevelBak = mazeLevel;
            int colBak = col;
            int rowBak = row;
            int playerStartPosXBak = playerStartPosX;
            int playerStartPosYBak = playerStartPosY;
            int playerPosXBak = playerPosX;
            int playerPosYBak = playerPosY;
            int goalPosXBak = goalPosX;
            int goalPosYBak = goalPosY;
            bool isMovedBak = isMoved;
            
            try
            {
                Init(mazeLevel + 1);
                mazeLevel++;
                hasNext = true;
            }
            catch(Exception)
            {
                stageCharSet = stageCharSetBak;
                mazeLevel = mazeLevelBak;
                col = colBak;
                row = rowBak;
                playerStartPosX = playerStartPosXBak;
                playerStartPosY = playerStartPosYBak;
                playerPosX = playerPosXBak;
                playerPosY = playerPosYBak;
                goalPosX = goalPosXBak;
                goalPosY = goalPosYBak;
                isMoved = isMovedBak;
                hasNext = false;
            }
            return hasNext;
        }

        private bool IsWall(int posX, int posY)
        {
            return WALL.Equals(GetStageChar(posX, posY)) || GOAL.Equals(GetStageChar(posX, posY));
        }

        private char GetStageChar(int x, int y)
        {
            return stageCharSet[x + y * col];
        }

        private void SetStageChar(int x, int y, char ch)
        {
            stageCharSet[x + y * col] = ch;
        }


    }
}

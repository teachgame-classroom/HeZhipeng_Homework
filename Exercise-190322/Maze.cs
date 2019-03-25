using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exercise_190322
{
    class Maze : Stage
    {
        // 声明迷宫对象可用的场地方块
        public const char WALL = '*';
        public const char OBSTACLE = '#';
        public const char PLAYER = 'p';
        public const char GOAL = 'o';
        public const char ROAD = ' ';
        // 声明迷宫对象读写地图文件路径格式
        public const string MAP_PATH_FORMAT = "./Map_{0}.txt";

        // 迷宫等级(关卡等级)
        public int level { get; private set; } = 1;
        // 玩家初始位置坐标
        public int playerStartPosX { get; private set; } = 0;
        public int playerStartPosY { get; private set; } = 0;
        // 玩家当前位置坐标
        public int playerPosX { get; private set; } = 0;
        public int playerPosY { get; private set; } = 0;
        // 终点位置坐标
        public int goalPosX { get; private set; } = 1;
        public int goalPosY { get; private set; } = 0;
        // 已移动标识
        public bool isMoved { get; private set; } = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Maze(): this(1)
        {
        }

        public Maze(int level) : base(1, 2)
        {
            stageCharSet = new char[] { PLAYER, GOAL };
            Init(level);
        }
        /// <summary>
        /// 重置当前关卡
        /// </summary>
        public void Reset()
        {
            SetStageChar(playerPosX, playerPosY, ROAD);
            SetStageChar(goalPosX, goalPosY, GOAL);
            SetStageChar(playerStartPosX, playerStartPosY, PLAYER);
            playerPosX = playerStartPosX;
            playerPosY = playerStartPosY;
            isMoved = true;
        }
        /// <summary>
        /// 初始化关卡
        /// </summary>
        /// <param name="level"></param>
        public void Init(int level)
        {
            ReadMapFile(level);
            this.level = level;
            isMoved = true;
        }
        /// <summary>
        /// 读取地图文件
        /// </summary>
        /// <param name="level"></param>
        private void ReadMapFile(int level)
        {
            string mapPath = string.Format(MAP_PATH_FORMAT, level);
            if (File.Exists(mapPath))
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
        }
        /// <summary>
        /// 移动玩家位置
        /// </summary>
        /// <param name="newPosX"></param>
        /// <param name="newPosY"></param>
        public void PlayerMove(int newPosX, int newPosY)
        {
            if (IsRoad(newPosX, newPosY))
            {
                SetStageChar(playerPosX, playerPosY, ROAD);
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
        /// <summary>
        /// 判断是否有下一关卡
        /// </summary>
        /// <returns></returns>
        public bool HasNext()
        {
            return File.Exists(string.Format(MAP_PATH_FORMAT, level + 1));
        }
        /// <summary>
        /// 进入下一关卡
        /// </summary>
        /// <returns></returns>
        public bool NextLevel()
        {
            bool hasNext = HasNext();
            if (hasNext)
            {
                level++;
                Init(level);
            }
            return hasNext;
        }
        /// <summary>
        /// 判断是否可移动到指定坐标处
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        protected bool IsRoad(int posX, int posY)
        {
            return !IsBorder(posX, posY)
                && (ROAD.Equals(GetStageChar(posX, posY)) || GOAL.Equals(GetStageChar(posX, posY)));
        }

    }
}

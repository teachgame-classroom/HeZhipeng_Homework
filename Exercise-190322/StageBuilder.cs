using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190322
{
    class StageBuilder : Stage
    {
        // 声明默认行数(高)
        private const int DEFAULT_ROW = 10;
        // 声明默认列数(宽)
        private const int DEFAULT_COL = 10;
        // 光标位置坐标
        public int cursorPosX { get; private set; } = 0;
        public int cursorPosY { get; private set; } = 0;
        // 场地初始默认字符
        public char defaultChar { get; set; }
        // 光标已移动标识
        public bool isMoved { get; private set; } = false;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public StageBuilder() : this(DEFAULT_ROW, DEFAULT_COL, (char)0)
        {
        }

        public StageBuilder(char defaultChar) : this(DEFAULT_ROW, DEFAULT_COL, defaultChar)
        {
        }

        public StageBuilder(int row, int col) : this(row, col, (char)0)
        {
        }

        public StageBuilder(int row, int col, char defaultChar) : base(row, col)
        {
            this.defaultChar = defaultChar;
            Reset();
            isMoved = true;
        }
        /// <summary>
        /// 重置场地
        /// </summary>
        public void Reset()
        {
            for(int i = 0; i < stageCharSet.Length; i++)
            {
                stageCharSet[i] = defaultChar;
            }
        }
        /// <summary>
        /// 移动光标
        /// </summary>
        /// <param name="newPosX"></param>
        /// <param name="newPosY"></param>
        public void CursorMove(int newPosX, int newPosY)
        {
            if (!IsBorder(newPosX, newPosY))
            {
                cursorPosX = newPosX;
                cursorPosY = newPosY;
                isMoved = true;
            }
            else
            {
                isMoved = false;
            }
        }
        /// <summary>
        /// 重写Display
        /// </summary>
        public new void Display()
        {
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    Console.Write(GetStageChar(x, y));
                    // 在当前位置右侧显示←字符
                    if (x == cursorPosX && y == cursorPosY)
                    {
                        Console.Write("<");
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.Write('\n');
            }
        }
        /// <summary>
        /// 获取光标处场地方块
        /// </summary>
        /// <returns></returns>
        public char GetBlock()
        {
            return GetStageChar(cursorPosX, cursorPosY);
        }
        /// <summary>
        /// 设置光标处场地方块
        /// </summary>
        /// <param name="block"></param>
        public void SetBlock(char block)
        {
            SetStageChar(cursorPosX, cursorPosY, block);
        }
        /// <summary>
        /// 保存场地为地图文件
        /// </summary>
        /// <param name="path">保存路径</param>
        public void Save(string path)
        {
            FileStream fs = File.Create(path);
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    fs.WriteByte((byte)GetStageChar(x, y));
                }
                fs.WriteByte((byte)'\n');
                fs.Flush();
            }
            fs.Close();
        }
        /// <summary>
        /// 保存场地为地图文件
        /// </summary>
        public void Save()
        {
            int level = 1;
            while(File.Exists(string.Format(Maze.MAP_PATH_FORMAT, level)))
            {
                level++;
            }
            Save(string.Format(Maze.MAP_PATH_FORMAT, level));
        }
    }
}

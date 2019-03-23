using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190322
{
    class Stage
    {
        // 场地方块数组
        public char[] stageCharSet { get; protected set; }
        // 行数(高)
        public int row { get; protected set; }
        // 列数(宽)
        public int col { get; protected set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row">指定的行数</param>
        /// <param name="col">指定的列数</param>
        public Stage(int row, int col)
        {
            this.row = row<=0?1:row;
            this.col = col <= 0 ? 1 : col;
            stageCharSet = new char[row * col];
        }
        /// <summary>
        /// 显示方法
        /// </summary>
        public void Display()
        {
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    Console.Write(GetStageChar(x, y));
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
        }
        /// <summary>
        /// 判断指定坐标处是否超越边界
        /// </summary>
        /// <param name="posX">横坐标</param>
        /// <param name="posY">纵坐标</param>
        /// <returns></returns>
        protected bool IsBorder(int posX, int posY)
        {
            return posX < 0 || posX >= col || posY < 0 || posY >= row;
        }
        /// <summary>
        /// 获取指定坐标处场地方块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected char GetStageChar(int x, int y)
        {
            char ret = char.MinValue;
            int index = x + y * col;
            if (index >= 0 && index < stageCharSet.Length)
            {
                ret = stageCharSet[index];
            }
            return ret;
        }
        /// <summary>
        /// 设置指定坐标处为指定字符
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ch"></param>
        protected void SetStageChar(int x, int y, char ch)
        {
            int index = x + y * col;
            if (index >= 0 && index < stageCharSet.Length)
            {
                stageCharSet[index] = ch;
            }
        }


    }
}

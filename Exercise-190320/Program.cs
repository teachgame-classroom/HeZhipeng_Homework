using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190320
{
    class Program
    {
        private static int[] intArray;


        static void Main(string[] args)
        {
            QuestionOne();
            DisplayCutOffRule();
            QuestionTwo();
            DisplayCutOffRule();
            QuestionThreeToSeven();

        }

        private static void DisplayCutOffRule()
        {
            Console.WriteLine();
            Console.WriteLine("===================================================================");
            Console.WriteLine();
        }

        /// <summary>
        /// 用for循环在屏幕上打印以下图案
        /// *****
        /// *****
        /// *****
        /// </summary>
        private static void QuestionOne()
        {
            Console.WriteLine("Q1:");
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 5; x++)
                {
                    Console.Write("*");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// 2.用for循环在屏幕上打印以下图案
        /// * * * * * * * *
        /// *             *
        /// *             *
        /// * * * * * * * *
        /// </summary>
        private static void QuestionTwo()
        {
            int row = 4;
            int col = 8;
            Console.WriteLine("Q2:");
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    if (x == 0 || x == (col - 1) || y == 0 || y == (row - 1))
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void QuestionThreeToSeven()
        {
            // 3.创建一个可以存放64个整数的整型数组，用变量名intArray代表
            intArray = new int[64];

            // 4.用for循环往第3题中创建的intArray数组中存入64个整数，存入的整数的数值和元素编号相等
            for(int i = 0; i < intArray.Length; i++)
            {
                intArray[i] = i;
            }

            //(0,0)  = 0
            Console.WriteLine("(0,0) = " + GetIndexOfCell(0, 0));
            //(1,0)  = 1
            Console.WriteLine("(1,0) = " + GetIndexOfCell(1, 0));
            //(0,1)  = 16
            Console.WriteLine("(0,1) = " + GetIndexOfCell(0, 1));
            //(1,1)  = 17
            Console.WriteLine("(1,1) = " + GetIndexOfCell(1, 1));
            //(7,1)  = 23
            Console.WriteLine("(7,1) = " + GetIndexOfCell(7, 1));
            //(8,2)  = 40
            Console.WriteLine("(8,2) = " + GetIndexOfCell(8, 2));
            //(15,3) = 63
            Console.WriteLine("(15,3) = " + GetIndexOfCell(15, 3));

        }

        private static int GetIndexOfCell(int x, int y)
        {
            return intArray[x + y * 16];
        }
    }
}

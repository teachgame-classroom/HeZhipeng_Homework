using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190325
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Q1:");
            QuestionOne(20, 5, 10);
            Console.WriteLine("===========================================================");
            Console.WriteLine("Q2:");
            QuestionTwo(1, 100);
            Console.WriteLine("===========================================================");
            Console.WriteLine("Q3:");
            QuestionThree(3);
            QuestionThree(5);
            QuestionThree(15);
            QuestionThree(30);
            QuestionThree(40);
            Console.WriteLine("===========================================================");
            Console.WriteLine("Q4:");
            QuestionFour();


        }

        // 输入3个整型参数a，b，c，按从大到小的顺序将它们打印出来。
        private static void QuestionOne(int a, int b, int c)
        {
            int[] nums = new int[] { a, b, c };
            Sort(nums);
            for(int i = 0; i < nums.Length; i++)
            {
                Console.Write(nums[i] + " ");
            }
            Console.Write('\n');
        }
        // 排序方法
        private static int[] Sort(int[] numArray)
        {
            int maxIndex = 0;
            int tempNum = 0;
            for (int i = 0; i < numArray.Length; i++)
            {
                maxIndex = i;
                for (int j = i + 1; j < numArray.Length; j++)
                {
                    if (numArray[j] > numArray[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                if(maxIndex != i)
                {
                    tempNum = numArray[i];
                    numArray[i] = numArray[maxIndex];
                    numArray[maxIndex] = tempNum;
                }
            }
            return numArray;
        }

        // 求 1 + 2 + 3 + …… + 100 的和。
        private static int QuestionTwo(int start, int end)
        {
            int amount = (start + end) *(end - start + 1) / 2;
            Console.WriteLine("{0}累加到{1} = {2}", start, end, amount);
            return amount;
        }

        // 判断一个数 n 能否同时被 3 和 5 整除。
        private static bool QuestionThree(int num)
        {
            bool ret = num % 3 == 0 && num % 5 == 0;

            if (ret)
            {
                Console.WriteLine("{0}能同时被3和5整除", num);
            }
            else
            {
                Console.WriteLine("{0}不能同时被3和5整除", num);
            }

            return ret;
        }

        // 将 100 以内的素数打印出来
        private static void QuestionFour()
        {
            int mersenneNum = 0;
            bool isMersenne = false;
            for (int i = 1; i <= 100; i++)
            {
                isMersenne = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isMersenne = false;
                        break;
                    }
                }
                if (isMersenne)
                {
                    mersenneNum++;
                    Console.Write(i + "\t");
                    if(mersenneNum % 10 == 0)
                    {
                        Console.Write('\n');
                    }
                }
            } 
        }
    }
}

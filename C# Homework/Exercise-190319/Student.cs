using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190319
{
    public class Student
    {
        public string name;
        public string sex;
        public int age;
        public int yuwen;
        public int math;
        public int english;

        public Student(string var_name, string var_sex, int var_age
            , int var_yuwen, int var_math, int var_english)
        {
            name = var_name;
            sex = var_sex;
            age = var_age;
            yuwen = var_yuwen;
            math = var_math;
            english = var_english;
        }

        public void SayHello()
        {
            Console.WriteLine("我叫{0}, 今年{1}岁了,是{2}同学"
                , name
                , age
                , sex);
        }

        public int Total()
        {
            return yuwen + math + english;
        }

        public int Average()
        {
            return Total() / 3;
        }

        public string Grade()
        {
            int avg = Average();

            // 评价
            string grade = "不及格";

            // 优
            if (avg >= 90)
            {
                grade = "优";
            }
            else if (avg >= 80) // 良
            {
                grade = "良";
            }
            else if (avg >= 60) // 中
            {
                grade = "中";
            }
            else // 不及格
            {
                grade = "不及格";
            }

            return grade;
        }

        public void PrintScore()
        {
            Console.WriteLine("我叫{0},这次考试总成绩为{1}分,平均成绩为{2},评价是{3}"
                , name
                , Total()
                , Average()
                , Grade());
        }
    }
}

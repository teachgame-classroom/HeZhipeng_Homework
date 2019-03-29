using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190318
{
    public class Human
    {
        public string name;
        public string sex;
        public int age;

        public Human(string var_name, string var_sex, int var_age)
        {
            name = var_name;
            sex = var_sex;
            age = var_age;
        }

        public void ChangeName(string pNewName)
        {
            name = pNewName;
        }

        public void ChangeAge(int pNewAge)
        {
            age = pNewAge;
        }

        public void ShowProfile()
        {
            Console.WriteLine("姓名:{0}, 性别:{1}, 年龄:{2}", name, sex, age);
        }
    }
}

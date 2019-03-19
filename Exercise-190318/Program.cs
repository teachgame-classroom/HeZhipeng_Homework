using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190318
{
    class Program
    {
        private static Human human;
        private static Warrior warrior;

        static void Main(string[] args)
        {
            human = new Human("HumanTest", "男", 20);
            warrior = new Warrior("WarriorTest", 100, 10);

            // 修改前打印信息
            human.ShowProfile();

            // 调用方法修改属性
            human.ChangeName("HumanChangeNameTest");
            human.ChangeAge(25);

            // 修改后打印信息
            human.ShowProfile();

            // 修改前打印信息
            warrior.ShowProfile();

            // 调用方法修改属性
            warrior.ChangeName("WarriorChangeNameTest");
            // 修改名字后打印信息
            warrior.ShowProfile();

            // 调用方法修改属性
            warrior.HpUp(10);
            warrior.AtkUp(1);
            // 修改生命值 攻击力后打印信息
            warrior.ShowProfile();

            // 调用方法修改属性
            warrior.LevelUp(20, 3);
            // 升级后打印信息
            warrior.ShowProfile();

            // 调用方法修改属性
            warrior.Hurt(50);
            // 受伤后打印信息
            warrior.ShowProfile();
        }
    }
}

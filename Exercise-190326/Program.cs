using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exercise_190326
{
    class Program
    {
        static void Main(string[] args)
        {
            // 读取装备文件,并创建装备对象数组
            Equipment[] equipments = ReadEquipments("./EquipmentInfo.csv");
            // 进攻方
            Character attacker = new Character(100, 10);
            attacker.Equip(equipments[0]);

            // 防御方
            Character player2 = new Character(100, 10);
            player2.Equip(equipments[1]);

            // 攻击前显示双方信息
            Console.WriteLine("攻击方:");
            attacker.ShowInfo();
            Console.WriteLine("防御方:");
            player2.ShowInfo();

            // 调用攻击方法
            attacker.Attack(player2);

            // 攻击后显示双方信息
            Console.WriteLine("======================================");
            Console.WriteLine("攻击方:");
            attacker.ShowInfo();
            Console.WriteLine("防御方:");
            player2.ShowInfo();

        }

        private static Equipment[] ReadEquipments(string equipmentFilePath)
        {
            // 如果无法打开文件, 返回null
            if (!File.Exists(equipmentFilePath))
            {
                return null;
            }
            List<Equipment> equipmentList = new List<Equipment>();
            string[] strLines = File.ReadAllLines(equipmentFilePath);

            for(int i = 1; i < strLines.Length; i++)
            {
                equipmentList.Add(CreateEquipment(strLines[i]));
            }

            return equipmentList.ToArray();
        }

        private static Equipment CreateEquipment(string strEquipmentInfo)
        {
            // 分割装备信息字符串
            string[] infoStrings = strEquipmentInfo.Split(',');
            
            int no = int.Parse(infoStrings[0]);
            string name = infoStrings[1];
            int atk = int.Parse(infoStrings[2]);
            int def = int.Parse(infoStrings[3]);
            int price = int.Parse(infoStrings[4]);
            string summary = infoStrings[5];

            Equipment result = new Equipment(no, name, atk, def, price, summary);

            return result;
        }
    }
}

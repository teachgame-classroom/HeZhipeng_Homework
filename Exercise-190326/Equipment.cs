using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190326
{
    class Equipment
    {
        int equipmentNo;
        string equipmentName;
        int atk;
        int def;
        int price;
        // 装备摘要 简介
        string summary;

        public Equipment(int equipmentNo, string equipmentName, int atk, int def, int price, string summary)
        {
            this.equipmentNo = equipmentNo;
            this.equipmentName = equipmentName;
            this.atk = atk;
            this.def = def;
            this.price = price;
            this.summary = summary;
        }

        public string GetName()
        {
            return equipmentName;
        }

        public int GetAtk()
        {
            return atk;
        }

        public int GetDef()
        {
            return def;
        }
    }
}

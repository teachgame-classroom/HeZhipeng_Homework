using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190326
{
    class Character
    {
        int hp;
        int atk;
        Equipment equipment;

        public Character(int hp, int atk)
        {
            this.hp = hp;
            this.atk = atk;
        }

        public void Hurt(int amount)
        {
            if (amount <= 0)
            {
                return;
            }
            hp -= amount;
        }

        public void Attack(Character target)
        {
            int damage = atk + equipment.GetAtk() - target.GetDefense();
            target.Hurt(damage);
        }

        public void Equip(Equipment equipment)
        {
            this.equipment = equipment;
        }

        public int GetDefense()
        {
            return equipment.GetDef();
        }

        public void ShowInfo()
        {
            Console.WriteLine("生命值:{0},攻击力:{1},装备:{2}",hp,atk,equipment.GetName());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_190318
{
    public class Warrior
    {
        public string name;
        public int hp;
        public int atk;

        public Warrior(string var_name, int var_hp, int var_atk)
        {
            name = var_name;
            hp = var_hp;
            atk = var_atk;
        }

        public void ChangeName(string pNewName)
        {
            name = pNewName;
        }

        public void Hurt(int pAtk)
        {
            hp = hp - pAtk;
        }

        public void AtkUp(int pAmount)
        {
            atk = atk + pAmount;
        }

        public void HpUp(int pAmount)
        {
            hp = hp + pAmount;
        }

        public void LevelUp(int pHpUp, int pAtkUp)
        {
            HpUp(pHpUp);
            AtkUp(pAtkUp);
        }

        public void ShowProfile()
        {
            Console.WriteLine("姓名:{0}, 生命值:{1}, 攻击力:{2}", name, hp, atk);
        }
    }
}

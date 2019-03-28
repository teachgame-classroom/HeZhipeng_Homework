using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    class Goblin : Charactar
    {
        public Goblin()
        {
            // 基础生命值
            int baseHP = 15;
            // 基础魔力值
            int baseMP = 0;
            // 基础攻击力
            int baseAct = 4;

            // 初始化名字
            name = "哥布林";
            // 初始化生命值
            maxHP = baseHP + Tools.GetRandom(-2, 5);
            HP = maxHP;
            // 初始化自然生命恢复量
            //recoverHP = Tools.GetRandom(1, 4);
            recoverHP = 0;
            // 初始化魔力值
            //maxMP = baseMP + Tools.GetRandom(0, 6);
            maxMP = baseMP;
            MP = maxMP;
            // 初始化自然魔力恢复量
            //recoverMP = Tools.GetRandom(0, 3);
            recoverMP = 0;
            // 初始化攻击力
            atk = baseAct + Tools.GetRandom(-1, 1);
            // 初始化技能
            skills = new List<Skill>();
            skills.Add(new Attack());
            // 
            isFighting = false;
            // 初始化死亡标示
            isDea = false;
        }
    }
}

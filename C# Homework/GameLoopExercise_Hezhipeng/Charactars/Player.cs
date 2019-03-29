using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public class Player : Charactar
    {

        public Player(string varName)
        {
            // 基础生命值
            int baseHP = 85;
            // 基础魔力值
            int baseMP = 15;
            // 基础攻击力
            int baseAct = 7;

            // 初始化名字
            name = varName;
            // 初始化生命值
            maxHP = baseHP + Tools.GetRandom(-5, 15);
            HP = maxHP;
            // 初始化自然生命恢复量
            recoverHP = Tools.GetRandom(1, 3);
            // 初始化魔力值
            maxMP = baseMP + Tools.GetRandom(-5, 5);
            MP = maxMP;
            // 初始化自然魔力恢复量
            recoverMP = Tools.GetRandom(0, 2);
            // 初始化攻击力
            atk = baseAct + Tools.GetRandom(-1, 3);
            // 初始化技能
            skills = new List<Skill>();
            skills.Add(new Attack());
            isFighting = false;
            // 初始化死亡标示
            isDea = false;
        }
        
    }
}

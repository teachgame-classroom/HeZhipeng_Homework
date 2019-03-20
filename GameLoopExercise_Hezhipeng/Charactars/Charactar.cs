using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public abstract class Charactar
    {
        // 角色名称
        public string name { get; set; }
        // 角色生命值上限
        protected int maxHP;
        // 角色当前生命值
        protected int HP;
        // 角色自然恢复生命量
        protected int recoverHP;
        // 角色魔力值上限
        protected int maxMP;
        // 角色当前魔力值
        protected int MP;
        // 角色自然恢复魔力量
        protected int recoverMP;
        // 攻击力
        protected int atk;
        // 技能
        public List<Skill> skills { get; set; }
        // 战斗中标示
        protected bool isFighting { get; set; }
        protected bool isDea;
        
        // 攻击
        public void Attack(Charactar target, Skill usedSkill)
        {
            target.UnderAttack(target, usedSkill);
        }
        // 受到攻击
        public void UnderAttack(Charactar user, Skill underSkill)
        {
            HP -= underSkill.GetDamage(user);
            if (HP <= 0)
            {
                isDea = true;
            }
        }
        // 恢复生命值
        public void RecoverHP(int extraRecover = 0)
        {
            // 0 < 生命值 < 生命值上限时有效
            if(HP < maxHP && HP > 0)
            {
                // 获得恢复后生命值
                int recoveredHP = HP + recoverHP + extraRecover;
                // 恢复后生命值不能超过生命值上限
                HP = recoveredHP > maxHP ? maxHP : recoveredHP;
            }
        }
        // 恢复魔力值
        public void RecoverMP(int extraRecover = 0)
        {
            // 魔力值 < 魔力值上限时有效
            if(MP < maxMP)
            {
                // 获得恢复后魔力值
                int recoveredMP = MP + recoverMP + extraRecover;
                // 恢复后魔力值不不能超过魔力值上限
                MP = recoveredMP > maxMP ? maxMP : recoveredMP;
            }
        }
        // 获取属性值
        public int GetMaxHP()
        {
            return maxHP;
        }
        public int GetHP()
        {
            return HP;
        }
        public int GetRecoverHP()
        {
            return recoverHP;
        }
        public int GetMaxMP()
        {
            return maxMP;
        }
        public int GetMP()
        {
            return MP;
        }
        public int GetRecoverMP()
        {
            return recoverMP;
        }
        public int GetAtk()
        {
            return atk;
        }
        // 获取死亡标示
        public bool IsDea()
        {
            return isDea;
        }
    }
}

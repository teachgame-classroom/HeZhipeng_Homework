using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public class Attack: Skill
    {
        public Attack()
        {
            name = "普通攻击";
            type = SkillType.Physics;
            effects = null;
        }
        
        public override int GetDamage(Charactar user)
        {
            return user.act;
        }
    }
}

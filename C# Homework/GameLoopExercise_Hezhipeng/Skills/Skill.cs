using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoopExercise_Hezhipeng
{
    public abstract class Skill
    {
        // 技能名称
        public string name { get; set; }
        // 技能伤害
        public int damage { get; set; }
        // 技能类型 物理:魔法
        public SkillType type { get; set; }
        // 技能效果
        public List<SkillEffect> effects { get; set; }

        public abstract int GetDamage(Charactar user);
    }

    // 技能类型
    public enum SkillType
    {
        // 物理技能
        Physics = 0,
        // 魔法技能
        Magic = 1
    }

    // 技能效果
    public enum SkillEffect
    {
        // 贯通
        Perforation = 0,
        // 减速
        SpeetDown = 1,
        // 提速
        SpeetUp = 2,
        // 灼烧
        Flaming = 3,
        // 出血
        Bleeding = 4
    }
}

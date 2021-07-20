using Bossaz.Abstracts;
using Bossaz.Enums;
namespace Bossaz.Entities
{
    public abstract class SkillBase : Id
    {
        public string Name { get; set; }
        public SkillLevelEnum Level { get; set; }
        public override string ToString()
        {
            return $@"{base.ToString()}
Name: {Name}
Level: {Level}";
        }
    }
}
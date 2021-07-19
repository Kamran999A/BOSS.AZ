using Bossaz.Abstracts;

namespace Bossaz.Entities
{
    public class WorkPlace:Id
    {
        public string Company { get; set; }
        public Timeline Timeline { get; set; }


        public override string ToString()
        {
            return $@"{base.ToString()}
Company: {Company}
{Timeline}";
        }
    }
}
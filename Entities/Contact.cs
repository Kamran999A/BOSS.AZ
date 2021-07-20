using System.Collections.Generic;
using System.Text;
using Bossaz.Abstracts;

namespace Bossaz.Entities
{
    public class Contact : Id
    {
        public string Mail { get; set; }
        public IList<string> Phones { get; set; }


        public override string ToString()
        {
            return $@"{base.ToString()}
Mail: {Mail}
Phones: 
{GetPhones()}";
        }

        public Contact()
        {
            Phones = new List<string>();
        }


        public string GetPhones()
        {
            if (Phones.Count == 0)
                return "";

            var sb = new StringBuilder();

            for (var i = 0; i < Phones.Count; i++)
            {
                sb.Append($" Phone number {Phones[i]}\n");
            }

            return sb.ToString();
        }
    }
}
using Bossaz.Abstracts;

namespace Bossaz.Entities
{
    public class Advert : Id
    {
        public string Category { get; set; }
        public string Position { get; set; }
        public string Region { get; set; }
        public SalaryRange SalaryRange { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }

        public string Requirements { get; set; }
        public string JobDescription { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }

        public override string ToString()
        {
            return $@"{base.ToString()}
Category: {Category}
Position: {Position}
Region: {Region}
Salary: 
{SalaryRange}
Education: {Education}
Experience: {Experience}
Requirements: {Requirements}
JobDescription: {JobDescription}
Company: {Company}
Contact: {Contact}";
        }
    }
}
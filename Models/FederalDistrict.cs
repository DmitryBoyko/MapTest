using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MapTest.Models
{
    public class FederalDistrict
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual IList<FederalSubject> FederalSubjects { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MapTest.Models
{
    public class FederalSubject
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; } 
        
        public int FederalDistrictID { get; set; }

        public virtual FederalDistrict FederalDistrict { get; set; }
    }
}

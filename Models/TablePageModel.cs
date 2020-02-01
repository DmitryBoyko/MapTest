using System;
using System.Collections.Generic;

namespace MapTest.Models
{
    public sealed class TablePageModel
    {
        public DateTime Updated { get { return DateTime.Now; } } 

        public List<FederalDistrict> FederalDistricts { get; set; }
    }
}

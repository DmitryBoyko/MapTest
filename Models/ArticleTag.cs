using System;
using System.ComponentModel.DataAnnotations;

namespace MapTest.Models
{

    public class ArticleTag
    {
     

        [Key] 
        public Guid ArticleID { get; set; }
 
        public virtual Article Article { get; set; }

        [Key]       
        public Guid TagID { get; set; }


        public virtual Tag Tag { get; set; }
    }
}

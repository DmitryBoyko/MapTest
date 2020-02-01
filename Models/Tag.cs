using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapTest.Models
{
    public class Tag
    {
        public Tag()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; } 

        [Column("Name", TypeName = "nvarchar(150)")]
        [Required]
        public string Name { get; set; }

        public virtual IList<ArticleTag> ArticleTags { get; set; }
    }
}

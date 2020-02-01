using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapTest.Models
{

    public partial class Article
    {
        public Article()
        {
           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Subject { get; set; }

       
        public virtual IList<ArticleTag> ArticleTags { get; set; }
    }
}

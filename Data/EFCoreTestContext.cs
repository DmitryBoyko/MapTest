using MapTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapTest.Data
{
    public class EFCoreTestContext : DbContext
    {
        public EFCoreTestContext(DbContextOptions<EFCoreTestContext> options)
            : base(options)
        {

        }

        public EFCoreTestContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<ArticleTag>().ToTable("ArticleTags");
            modelBuilder.Entity<FederalDistrict>().ToTable("FederalDistricts");
            modelBuilder.Entity<FederalSubject>().ToTable("FederalSubjects");
             

            modelBuilder.Entity<ArticleTag>().HasKey(bc => new { bc.TagID, bc.ArticleID }); 
            modelBuilder.Entity<ArticleTag>().HasOne(bc => bc.Article).WithMany(b => b.ArticleTags)
                                            .HasForeignKey(bc => bc.ArticleID); 
            modelBuilder.Entity<ArticleTag>().HasOne(bc => bc.Tag).WithMany(c => c.ArticleTags)
                                            .HasForeignKey(bc => bc.TagID); 

            modelBuilder.Entity<FederalDistrict>().HasMany(c => c.FederalSubjects)
                                                    .WithOne(e => e.FederalDistrict)
                                                    .HasForeignKey(p => p.FederalDistrictID).IsRequired();
            modelBuilder.Entity<FederalDistrict>().HasMany(c => c.FederalSubjects).WithOne(e => e.FederalDistrict)
                                        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<FederalDistrict> FederalDistricts { get; set; }

        public DbSet<FederalSubject> FederalSubjects { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Models
{
    public class UploadImageDBContext : DbContext
    {
        public UploadImageDBContext(DbContextOptions<UploadImageDBContext> options):base(options)
        {

        }
            

        public DbSet<Images> Image { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   // optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=UpdateImagesDb;Integrated Security=True");
        //}
    }
}

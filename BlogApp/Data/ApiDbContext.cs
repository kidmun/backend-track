using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Data;

    public class ApiDbContext : DbContext
    {
      
        public DbSet<Post> Posts {get;set;}
        public DbSet<Comment> Comments {get;set;}
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired(false);  ;
        }
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }
        

    }

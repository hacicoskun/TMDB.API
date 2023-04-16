using HC.Shared.Application.Interfaces;
using HC.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace HC.Shared.Infrastructure
{
    public class PostgreDbContext : DbContext, IPostgreDbContext
    {

        public PostgreDbContext(DbContextOptions<PostgreDbContext> opt) : base(opt)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
            modelBuilder.Entity<Movie>().Property(b => b.original_language).IsRequired(false);
            modelBuilder.Entity<Movie>().Property(b => b.original_title).IsRequired(false);
            modelBuilder.Entity<Movie>().Property(b => b.overview).IsRequired(false); 
            modelBuilder.Entity<Movie>().Property(b => b.poster_path).IsRequired(false);
            modelBuilder.Entity<Movie>().Property(b => b.release_date).IsRequired(false);
            modelBuilder.Entity<Movie>().Property(b => b.title).IsRequired(false);  
            modelBuilder.Entity<Movie>().Property(b => b.note).IsRequired(false);  
            modelBuilder.Entity<Movie>().Property(b => b.UserId).IsRequired(false);  
            modelBuilder.Entity<MovieComments>().Property(b => b.note).IsRequired(false);  
            modelBuilder.Entity<MovieComments>().Property(b => b.user_id).IsRequired(false);  
             

        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieComments> MovieComments { get; set; }



    }
}
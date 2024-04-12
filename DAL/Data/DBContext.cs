using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DAL.Data.Configurations;
using DAL.Entities;

namespace DAL.Data
{
    public class DBContext : IdentityDbContext<User, Role, Guid>
    {
        public DBContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }


        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }

    }
}

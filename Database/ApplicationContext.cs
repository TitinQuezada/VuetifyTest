using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Database
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationContext)));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SystemUser> SystemUsers { get; set; }
    }
}

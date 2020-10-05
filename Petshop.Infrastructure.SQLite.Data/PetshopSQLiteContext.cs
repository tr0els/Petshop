using Microsoft.EntityFrameworkCore;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.SQLite.Data
{
    public class PetshopSQLiteContext: DbContext
    {
        // When constructor is called, use this context with the provided options.
        // Context represents the database connection and a set of tables
        public PetshopSQLiteContext(DbContextOptions<PetshopSQLiteContext> options) : base(options) { }

        // Table representation
        public DbSet<Pet> Pets { get; set; } // why get/set here? Does it not look at the entity get/set?
        public DbSet<Owner> Owners { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

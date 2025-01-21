using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data
{
    public class PersonContext : DbContext
    {
        public DbSet<PersonModel> Peaple { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source=person.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ParsingTest.Models;

namespace DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

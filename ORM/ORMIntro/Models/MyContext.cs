using Microsoft.EntityFrameworkCore;

namespace ORMIntro.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<Sundae> Sundaes {get;set;}
    }
}
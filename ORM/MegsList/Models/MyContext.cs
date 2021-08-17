using Microsoft.EntityFrameworkCore;

namespace MegsList.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<Listing> Listings {get;set;}
    }
}
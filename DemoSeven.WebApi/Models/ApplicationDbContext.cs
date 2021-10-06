using DemoSeven.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoSeven.WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
    }
    
 
    
}
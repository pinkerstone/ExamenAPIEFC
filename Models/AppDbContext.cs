using Microsoft.EntityFrameworkCore;

namespace ExamenAPIEFC.Models
{
    public class AppDbContext : DbContext
    {
        //Entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //Connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-RMV50JMD\\SQLEXPRESS; " +
                "Database=DBExamenApp; Integrated Security=True;" +
                "Trust Server Certificate=True ");
        }
    }
}

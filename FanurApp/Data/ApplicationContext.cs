using FanurApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FanurApp.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new User 
                { 
                    Id = 1,
                    Email = "ruzimurodabdunazarov2003@mail.ru",
                    Name = "Ruzimurod Abdunazarov",
                    Password = "parol2003"
                },
                new User
                {
                    Id = 2,
                    Email = "eldoraxmedov2003@mail.ru",
                    Name = "Eldorbek Axmedov",
                    Password = "parol2003"
                }
            }
        );
    }
}
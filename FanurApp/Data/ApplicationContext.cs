using FanurApp.Enums;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FanurApp.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Administrator"
                },
                new Role
                {
                    Id = 2,
                    Name = "Teacher"
                },
                new Role
                {
                    Id = 3,
                    Name = "User"
                }
            }
        );
        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "ruzimurodabdunazarov2003@mail.ru",
                    Name = "Ruzimurod Abdunazarov",
                    Password = "parol2003",
                    RoleId = (int)RolesEnum.Administrator
                },
                new User
                {
                    Id = 2,
                    Email = "eldoraxmedov2003@mail.ru",
                    Name = "Eldorbek Axmedov",
                    Password = "parol2003",
                    RoleId = (int)RolesEnum.Teacher
                }
            }
        );
    }
}
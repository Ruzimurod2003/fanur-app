using FanurApp.Enums;
using FanurApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FanurApp.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Culture> Cultures { get; set; }
    public DbSet<Resource> Resources { get; set; }
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
        modelBuilder.Entity<Course>().HasData(
            new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Beginner kursi",
                    Description = "Bu kurs ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
                new Course
                {
                    Id = 2,
                    Name = "Elementary kursi",
                    Description = "Bu kurs ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
            }
        );
        modelBuilder.Entity<Topic>().HasData(
            new List<Topic>
            {
                new Topic
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Beginner kursiga kirish",
                    Description = "Bu kurs ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
                new Topic
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "'To be' fe'li",
                    Description = "Bu kurs ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
            }
        );
        modelBuilder.Entity<Video>().HasData(
            new List<Video>
            {
                new Video
                {
                    Id = 1,
                    TopicId = 1,
                    URLName = "https://www.youtube.com/embed/qgInM6FH8Lk?rel=0",
                    Caption = "Bu video ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
                new Video
                {
                    Id = 2,
                    TopicId = 1,
                    URLName = "https://www.youtube.com/embed/qgInM6FH8Lk?rel=0",
                    Caption = "Bu video ingliz tilini o'rganuvchilar uchun",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Author = "Ruzimurod Abdunazarov"
                },
            }
        );
    }
}
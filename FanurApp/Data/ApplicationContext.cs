using FanurApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FanurApp.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
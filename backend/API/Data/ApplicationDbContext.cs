using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     var usersJson = File.ReadAllText("users.json");

    //     List<ApplicationUser> users = JsonSerializer.Deserialize<List<ApplicationUser>>(usersJson)!;

    //     modelBuilder.Entity<ApplicationUser>().HasData(users);

    //     base.OnModelCreating(modelBuilder);
    // }
}
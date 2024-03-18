using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using User_Roles_Application.Models;

namespace MyApplication.Db;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Tasks>().ToTable("Tasks");
        builder.Entity<User>().HasNoKey();
        builder.Entity<TaskReport>()
            .HasOne(tr => tr.Task)
            .WithMany()
            .HasForeignKey(tr => tr.TaskId);
    }

    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<TaskReport> TaskReport { get; set; }
    public DbSet<User> CreateUser { get; set; }
}
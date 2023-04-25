using BecaworkService.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace BecaworkService.Respository
{
    public class BecaworkDbContext : DbContext
    {
        public BecaworkDbContext(DbContextOptions<BecaworkDbContext> options) : base(options) { }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            builder.Entity<Mail>().ToTable("Mail");
            builder.Entity<Notification>().ToTable("Notification");
        }
    }
}

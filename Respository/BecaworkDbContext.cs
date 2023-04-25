using BecaworkService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace BecaworkService.Respository
{
    public class BecaworkDbContext : DbContext
    {
        public BecaworkDbContext(DbContextOptions<BecaworkDbContext> options) : base(options) { }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FCMTokenLog> FCMTokenLogs { get; set; }


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
            builder.Entity<FCMTokenLog>().ToTable("FCMTokenLog");
        }
    }
}

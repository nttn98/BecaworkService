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
        public DbSet<FCMTokenLog> FCMTokenLogs { get; set; }
        public DbSet<FCMToken> FCMTokens { get; set; }
        public DbSet<ElectrolyticToken> ElectrolyticTokens { get; set; }
        public DbSet<ElectrolyticTokenLog> ElectrolyticTokenLogs { get; set; }



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
            builder.Entity<FCMToken>().ToTable("FCMToken");
            builder.Entity<ElectrolyticTokenLog>().ToTable("ElectrolyticTokenLog");
            builder.Entity<ElectrolyticToken>().ToTable("ElectrolyticToken");

        }
    }
}

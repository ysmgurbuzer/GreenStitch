using EntityLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class GreenContext :IdentityDbContext<Member,Role,int>
    {
        public GreenContext(DbContextOptions<GreenContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FabricType> FabricTypes { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RecyclingHistory> RecyclingHistories { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Favorites>()
     .HasOne(f => f.Advertisements)
     .WithMany(a => a.Favorites)
     .HasForeignKey(f => f.AdvertId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Member)
                .WithMany(m => m.Favorites)
                .HasForeignKey(f => f.MemberId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<OrderHistory>()
.HasOne(f => f.Advertisements)
.WithMany(a => a.OrderHistories)
.HasForeignKey(f => f.AdvertId)
.OnDelete(DeleteBehavior.Restrict);

         
            modelBuilder.Entity<MemberRole>()
            .HasKey(mr => mr.Id);

            modelBuilder.Entity<MemberRole>()
                .HasOne(mr => mr.Member)
                .WithMany(m => m.MemberRoles)
                .HasForeignKey(mr => mr.MemberId);

            modelBuilder.Entity<MemberRole>()
                .HasOne(mr => mr.Role)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(mr => mr.RoleId);

            modelBuilder.Entity<Advertisement>()
           .HasOne(a => a.Member)
           .WithMany(m => m.Advertisement)
           .HasForeignKey(a => a.MemberId)
           .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RecyclingHistory>()
            .HasOne(r => r.Member)
            .WithMany(m => m.RecyclingHistories)
            .HasForeignKey(r => r.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
      
        }
    }
}

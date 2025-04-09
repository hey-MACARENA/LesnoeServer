using System.Collections.Generic;
using LesnoeServer.Tables;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Fire_hazard_levels> Fire_hazard_levels { get; set; }
        public DbSet<Leave_types> Leave_types { get; set; }
        public DbSet<Leaves> Leaves { get; set; }
        public DbSet<LeavesDetails> LeavesDetails { get; set; }
        public DbSet<Order_Employee> Order_Employee { get; set; }
        public DbSet<Order_types> Order_types { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersDetails> OrdersDetails { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<ReportsDetails> ReportsDetails { get; set; }
        public DbSet<Sections> Sections { get; set; }
        public DbSet<SectionsDetailsRaw> SectionsDetailsRaws { get; set; }
        public DbSet<SectionsDetails> SectionsDetails { get; set; }
        public DbSet<SectionsDetailsWithIds> SectionsDetailsWithIds { get; set; }
        public DbSet<SectionsFire> SectionsWithFireSafetyMeasures { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Territories> Territories { get; set; }
        public DbSet<Travel_sheets> Travel_sheets { get; set; }
        public DbSet<Travel_sheetsDetails> Travel_sheetsDetails { get; set; }
        public DbSet<Work_Employee> Work_Employee { get; set; }
        public DbSet<Work_types> Work_types { get; set; }
        public DbSet<Works> Works { get; set; }
        public DbSet<WorksDetails> WorksDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<EmployeeDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<LeavesDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<OrdersDetails>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView(null); 
            });

            modelBuilder.Entity<ReportsDetails>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView(null);
            });

            modelBuilder.Entity<SectionsDetailsRaw>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            modelBuilder.Entity<SectionsDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
            modelBuilder.Entity<SectionsDetailsWithIds>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<SectionsFire>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); 
            });

            modelBuilder.Entity<Travel_sheetsDetails>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView(null);
            });

            modelBuilder.Entity<WorksDetails>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView(null); 
            });
        }
    }
}

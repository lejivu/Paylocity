using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityServices.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext() {}

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Paycheck> Paycheck { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.AnnualPackageExpense).HasColumnType("money");

                entity.Property(e => e.AnnualSalary).HasColumnType("money");

                entity.Property(e => e.NetPay).HasColumnType("money");
            });

            modelBuilder.Entity<Paycheck>(entity =>
            {
                entity.Property(e => e.TotalEarnings).HasColumnType("money");

                entity.Property(e => e.TotalTaxes).HasColumnType("money");

                entity.Property(e => e.TotalDeductions).HasColumnType("money");

                entity.Property(e => e.NetPay).HasColumnType("money");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DepositDate).HasColumnType("datetime");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

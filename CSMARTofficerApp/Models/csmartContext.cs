using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CSMARTofficerApp.Models
{
    public partial class csmartContext : DbContext
    {
        public csmartContext()
        {
        }

        public csmartContext(DbContextOptions<csmartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Officer> Officers { get; set; }
        public virtual DbSet<OfficerAgency> OfficerAgencies { get; set; }
        public virtual DbSet<OfficerLoginCred> OfficerLoginCreds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LIN80018902\\SQLEXPRESS;Database=csmart;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Officer>(entity =>
            {
                entity.HasKey(e => e.OfficerKey)
                    .HasName("PK__Officer__D30F561C01DA94D9");

                entity.ToTable("Officer");

                entity.Property(e => e.OfficerKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OfficerAgency>(entity =>
            {
                entity.HasKey(e => e.AgencyCode)
                    .HasName("PK__OfficerA__549CB4B1F47AC6A5");

                entity.ToTable("OfficerAgency");

                entity.Property(e => e.AgencyCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyState)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OfficerLoginCred>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

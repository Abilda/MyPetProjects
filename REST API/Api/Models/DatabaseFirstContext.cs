using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class DatabaseFirstContext : DbContext
    {
        public DatabaseFirstContext()
        {
        }

        public DatabaseFirstContext(DbContextOptions<DatabaseFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conclusions> Conclusions { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Patientdoctors> Patientdoctors { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DatabaseFirst;Username=test;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conclusions>(entity =>
            {
                entity.ToTable("conclusions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Complaints)
                    .IsRequired()
                    .HasColumnName("complaints");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Diagnosis)
                    .IsRequired()
                    .HasColumnName("diagnosis");

                entity.Property(e => e.Patientdoctorid).HasColumnName("patientdoctorid");

                entity.Property(e => e.Visitdate).HasColumnName("visitdate");

                entity.HasOne(d => d.Patientdoctor)
                    .WithMany(p => p.Conclusions)
                    .HasForeignKey(d => d.Patientdoctorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("conclusions_patientdoctorid_fkey");
            });

            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.ToTable("doctors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Familyname)
                    .IsRequired()
                    .HasColumnName("familyname");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname");

                entity.Property(e => e.Middlename).HasColumnName("middlename");

                entity.Property(e => e.Specializationid).HasColumnName("specializationid");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.Specializationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctors_specializationid_fkey");
            });

            modelBuilder.Entity<Patientdoctors>(entity =>
            {
                entity.ToTable("patientdoctors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Patientid).HasColumnName("patientid");

                entity.Property(e => e.Visitdate).HasColumnName("visitdate");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Patientdoctors)
                    .HasForeignKey(d => d.Doctorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("patientdoctors_doctorid_fkey");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Patientdoctors)
                    .HasForeignKey(d => d.Patientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("patientdoctors_patientid_fkey");
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.ToTable("patients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Familyname)
                    .IsRequired()
                    .HasColumnName("familyname");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname");

                entity.Property(e => e.Iin).IsRequired();

                entity.Property(e => e.Middlename).HasColumnName("middlename");

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("specialization");

                entity.HasIndex(e => e.Id)
                    .HasName("specialization_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

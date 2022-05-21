using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TTGServer.Models.DBModels;

namespace TTGServer
{
    public partial class TTG_ver3Context : DbContext
    {
        public TTG_ver3Context()
        {
        }

        public TTG_ver3Context(DbContextOptions<TTG_ver3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<Way> Ways { get; set; } = null!;
        public virtual DbSet<WorkDay> WorkDays { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TTG_ver3;Username=postgres;Password=New08TTGpass");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owner");

                entity.HasIndex(e => e.Login, "UniqueOwnerLogin")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.License).HasMaxLength(20);

                entity.Property(e => e.Login).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Password).HasMaxLength(60);
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("Passenger");

                entity.HasIndex(e => e.Login, "UniquePassengerLogin")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Password).HasMaxLength(60);

                entity.Property(e => e.StationId).HasColumnName("StationID");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("StationPassenger_FK");
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.ToTable("Station");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.WayId).HasColumnName("WayID");

                entity.HasOne(d => d.Way)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.WayId)
                    .HasConstraintName("WayStations_pkey");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.WorkdayId).HasColumnName("Workday_ID");

                entity.HasOne(d => d.Workday)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.WorkdayId)
                    .HasConstraintName("WorkDayTrip_FK");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.HasIndex(e => e.Passport, "UniquePassport")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "UniqueUnitLogin")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Number).HasMaxLength(12);

                entity.Property(e => e.Passport).HasMaxLength(10);

                entity.Property(e => e.Password).HasMaxLength(60);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.WayId).HasColumnName("WayID");

                entity.HasOne(d => d.Way)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.WayId)
                    .HasConstraintName("WayUnit_FK");
            });

            modelBuilder.Entity<Way>(entity =>
            {
                entity.ToTable("Way");

                entity.HasIndex(e => e.Name, "UniqueWayName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(5);

                entity.Property(e => e.OwnerId).HasColumnName("Owner_ID");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Ways)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("OwnerWay_FK");
            });

            modelBuilder.Entity<WorkDay>(entity =>
            {
                entity.ToTable("WorkDay");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UnitId).HasColumnName("Unit_ID");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.WorkDays)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("UnitWorkDay_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

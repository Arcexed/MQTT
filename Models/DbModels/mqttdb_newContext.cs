using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Models.DbModels
{
    public partial class mqttdb_newContext : DbContext
    {
        public mqttdb_newContext()
        {
        }

        public mqttdb_newContext(DbContextOptions<mqttdb_newContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<EventsDevice> EventsDevices { get; set; }
        public virtual DbSet<EventsUser> EventsUsers { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatingDate).HasColumnName("creating_date");

                entity.Property(e => e.Descr)
                    .HasColumnType("text")
                    .HasColumnName("descr");

                entity.Property(e => e.EditingDate).HasColumnName("editing_date");

                entity.Property(e => e.Geo)
                    .HasColumnType("text")
                    .HasColumnName("geo");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__device__id_user__403A8C7D");
            });

            modelBuilder.Entity<EventsDevice>(entity =>
            {
                entity.ToTable("events_device");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdDevice).HasColumnName("id_device");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.HasOne(d => d.IdDeviceNavigation)
                    .WithMany(p => p.EventsDevices)
                    .HasForeignKey(d => d.IdDevice)
                    .HasConstraintName("FK__events_de__id_de__4BAC3F29");
            });

            modelBuilder.Entity<EventsUser>(entity =>
            {
                entity.ToTable("events_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.EventsUsers)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__events_us__id_us__47DBAE45");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("measurements");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AirHumidity).HasColumnName("air_humidity");

                entity.Property(e => e.AtmosphericPressure).HasColumnName("atmospheric_pressure");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdDevice).HasColumnName("id_device");

                entity.Property(e => e.LightLevel).HasColumnName("light_level");

                entity.Property(e => e.RadiationLevel).HasColumnName("radiation_level");

                entity.Property(e => e.SmokeLevel).HasColumnName("smoke_level");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.HasOne(d => d.IdDeviceNavigation)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.IdDevice)
                    .HasConstraintName("FK__measureme__id_de__440B1D61");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descr)
                    .HasColumnType("text")
                    .HasColumnName("descr");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("accessToken")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("ip");

                entity.Property(e => e.IsBlock).HasColumnName("is_block");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__users__id_role__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

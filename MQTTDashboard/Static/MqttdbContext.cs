using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MQTTWebApi.Models
{
    public partial class MqttdbContext : DbContext
    {
        public MqttdbContext()
        {
        }

        public MqttdbContext(DbContextOptions<MqttdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descr)
                    .HasColumnType("text")
                    .HasColumnName("descr");

                entity.Property(e => e.Geo)
                    .HasColumnType("text")
                    .HasColumnName("geo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.CreateDate)
                    .IsRequired()
                    .HasColumnName("creating_date");

                entity.Property(e => e.EditDate)
                    .IsRequired()
                    .HasColumnName("editing_date");

            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdDevice).HasColumnName("id_device");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.IdDevice)
                    .HasConstraintName("FK__events__id_devic__45F365D3");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("measurements");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AirHumidity).HasColumnName("air_humidity");

                entity.Property(e => e.AtmosphericPressure).HasColumnName("atmospheric_pressure");

                entity.Property(e => e.IdDevice).HasColumnName("id_device");

                entity.Property(e => e.LightLevel).HasColumnName("light_level");

                entity.Property(e => e.SmokeLevel).HasColumnName("smoke_level");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.IdDevice)
                    .HasConstraintName("FK__measureme__id_de__4222D4EF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MirIgryshek.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Polzovatel> Polzovatel { get; set; }
        public virtual DbSet<PolzovatelRole> PolzovatelRole { get; set; }
        public virtual DbSet<PolzovatelZakaz> PolzovatelZakaz { get; set; }
        public virtual DbSet<Postavshik> Postavshik { get; set; }
        public virtual DbSet<Proizvoditel> Proizvoditel { get; set; }
        public virtual DbSet<Pynkti> Pynkti { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tovar> Tovar { get; set; }
        public virtual DbSet<TovarKategoria> TovarKategoria { get; set; }
        public virtual DbSet<TovarPostavshik> TovarPostavshik { get; set; }
        public virtual DbSet<TovarProizvoditel> TovarProizvoditel { get; set; }
        public virtual DbSet<Zakaz> Zakaz { get; set; }
        public virtual DbSet<ZakazPynkti> ZakazPynkti { get; set; }
        public virtual DbSet<ZakazStatus> ZakazStatus { get; set; }
        public virtual DbSet<ZakazTovar> ZakazTovar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=MirIgryshek;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasKey(e => e.IdKategoria);

                entity.Property(e => e.IdKategoria)
                    .HasColumnName("ID_Kategoria")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Polzovatel>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasMaxLength(50);

                entity.Property(e => e.Parol)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PolzovatelRole>(entity =>
            {
                entity.HasKey(e => new { e.Login, e.IdRole });

                entity.ToTable("Polzovatel_Role");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.PolzovatelRole)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polzovatel_Role_Role");

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.PolzovatelRole)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polzovatel_Role_Polzovatel");
            });

            modelBuilder.Entity<PolzovatelZakaz>(entity =>
            {
                entity.HasKey(e => new { e.Login, e.Nomer });

                entity.ToTable("Polzovatel_Zakaz");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.HasOne(d => d.LoginNavigation)
                    .WithMany(p => p.PolzovatelZakaz)
                    .HasForeignKey(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polzovatel_Zakaz_Polzovatel");

                entity.HasOne(d => d.NomerNavigation)
                    .WithMany(p => p.PolzovatelZakaz)
                    .HasForeignKey(d => d.Nomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Polzovatel_Zakaz_Zakaz");
            });

            modelBuilder.Entity<Postavshik>(entity =>
            {
                entity.HasKey(e => e.IdPostavshik);

                entity.Property(e => e.IdPostavshik)
                    .HasColumnName("ID_Postavshik")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Proizvoditel>(entity =>
            {
                entity.HasKey(e => e.IdProizvoditel);

                entity.Property(e => e.IdProizvoditel)
                    .HasColumnName("ID_Proizvoditel")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Pynkti>(entity =>
            {
                entity.HasKey(e => e.IdPynkt);

                entity.Property(e => e.IdPynkt)
                    .HasColumnName("ID_Pynkt")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.Property(e => e.IdRole)
                    .HasColumnName("ID_Role")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.Property(e => e.IdStatus)
                    .HasColumnName("ID_Status")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tovar>(entity =>
            {
                entity.HasKey(e => e.Articul);

                entity.Property(e => e.Articul).HasMaxLength(10);

                entity.Property(e => e.Cena).HasColumnType("money");

                entity.Property(e => e.Ima)
                    .IsRequired()
                    .HasColumnName("IMA")
                    .HasMaxLength(200);

                entity.Property(e => e.Izmerenie)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Opisanie)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TovarKategoria>(entity =>
            {
                entity.HasKey(e => new { e.Articul, e.IdKategoria });

                entity.ToTable("Tovar_Kategoria");

                entity.Property(e => e.Articul).HasMaxLength(10);

                entity.Property(e => e.IdKategoria).HasColumnName("ID_Kategoria");

                entity.HasOne(d => d.ArticulNavigation)
                    .WithMany(p => p.TovarKategoria)
                    .HasForeignKey(d => d.Articul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Kategoria_Tovar");

                entity.HasOne(d => d.IdKategoriaNavigation)
                    .WithMany(p => p.TovarKategoria)
                    .HasForeignKey(d => d.IdKategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Kategoria_Kategoria");
            });

            modelBuilder.Entity<TovarPostavshik>(entity =>
            {
                entity.HasKey(e => new { e.Articul, e.IdPostavshik });

                entity.ToTable("Tovar_Postavshik");

                entity.Property(e => e.Articul).HasMaxLength(10);

                entity.Property(e => e.IdPostavshik).HasColumnName("ID_Postavshik");

                entity.HasOne(d => d.ArticulNavigation)
                    .WithMany(p => p.TovarPostavshik)
                    .HasForeignKey(d => d.Articul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Postavshik_Tovar");

                entity.HasOne(d => d.IdPostavshikNavigation)
                    .WithMany(p => p.TovarPostavshik)
                    .HasForeignKey(d => d.IdPostavshik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Postavshik_Postavshik");
            });

            modelBuilder.Entity<TovarProizvoditel>(entity =>
            {
                entity.HasKey(e => new { e.Articul, e.IdProizvoditel });

                entity.ToTable("Tovar_Proizvoditel");

                entity.Property(e => e.Articul).HasMaxLength(10);

                entity.Property(e => e.IdProizvoditel).HasColumnName("ID_Proizvoditel");

                entity.HasOne(d => d.ArticulNavigation)
                    .WithMany(p => p.TovarProizvoditel)
                    .HasForeignKey(d => d.Articul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Proizvoditel_Tovar");

                entity.HasOne(d => d.IdProizvoditelNavigation)
                    .WithMany(p => p.TovarProizvoditel)
                    .HasForeignKey(d => d.IdProizvoditel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tovar_Proizvoditel_Proizvoditel");
            });

            modelBuilder.Entity<Zakaz>(entity =>
            {
                entity.HasKey(e => e.Nomer);

                entity.Property(e => e.Nomer).ValueGeneratedNever();

                entity.Property(e => e.DataDostavka).HasColumnType("date");

                entity.Property(e => e.DataZakaza).HasColumnType("date");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ZakazPynkti>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Zakaz_Pynkti");

                entity.Property(e => e.IdPynkt).HasColumnName("ID_Pynkt");

                entity.HasOne(d => d.IdPynktNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPynkt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Pynkti_Pynkti");

                entity.HasOne(d => d.NomerNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Nomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Pynkti_Zakaz");
            });

            modelBuilder.Entity<ZakazStatus>(entity =>
            {
                entity.HasKey(e => new { e.Nomer, e.IdStatus });

                entity.ToTable("Zakaz_Status");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.ZakazStatus)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Status_Status");

                entity.HasOne(d => d.NomerNavigation)
                    .WithMany(p => p.ZakazStatus)
                    .HasForeignKey(d => d.Nomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Status_Zakaz");
            });

            modelBuilder.Entity<ZakazTovar>(entity =>
            {
                entity.HasKey(e => new { e.Nomer, e.Articul });

                entity.ToTable("Zakaz_Tovar");

                entity.Property(e => e.Articul).HasMaxLength(10);

                entity.HasOne(d => d.ArticulNavigation)
                    .WithMany(p => p.ZakazTovar)
                    .HasForeignKey(d => d.Articul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Tovar_Tovar");

                entity.HasOne(d => d.NomerNavigation)
                    .WithMany(p => p.ZakazTovar)
                    .HasForeignKey(d => d.Nomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zakaz_Tovar_Zakaz");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

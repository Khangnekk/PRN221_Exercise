using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaExerciseRazerPage.Models
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieStar> MovieStars { get; set; } = null!;
        public virtual DbSet<Star> Stars { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI;Database=Cinema;user=Khang;password=123;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<MovieStar>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Movie_Star");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .HasColumnName("position");

                entity.Property(e => e.StarId).HasColumnName("star_id");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Movie_Sta__movie__3A81B327");

                entity.HasOne(d => d.Star)
                    .WithMany()
                    .HasForeignKey(d => d.StarId)
                    .HasConstraintName("FK__Movie_Sta__star___3B75D760");
            });

            modelBuilder.Entity<Star>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .HasColumnName("fullname");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(255)
                    .HasColumnName("nationality");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

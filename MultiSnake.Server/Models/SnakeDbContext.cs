using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MultiSnake.Server.Models;

public partial class SnakeDbContext : DbContext
{
    public SnakeDbContext()
    {
    }

    public SnakeDbContext(DbContextOptions<SnakeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SnakeDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.ToTable("ChatMessage");

            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Message).HasColumnOrder(2);
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .HasColumnOrder(3);
            entity.Property(e => e.User).HasColumnOrder(1);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

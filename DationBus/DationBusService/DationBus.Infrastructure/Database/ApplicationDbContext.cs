using System;
using System.Collections.Generic;
using DationBus.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DationBus.Infrastructure.Database;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<QueueLog> QueueLogs { get; set; }

    public virtual DbSet<QueueStatus> QueueStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QueueLog>(entity =>
        {
            entity.HasOne(d => d.StatusNameNavigation).WithMany(p => p.QueueLogs)
                .HasPrincipalKey(p => p.QueueStatusName)
                .HasForeignKey(d => d.StatusName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QueueLog_QueueStatus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

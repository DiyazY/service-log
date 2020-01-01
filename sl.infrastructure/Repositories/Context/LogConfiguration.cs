using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace sl.infrastructure.Repositories.Context
{
    internal static class LogConfiguration
    {
        internal static void ApplyLogConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<LogEntity>(entity =>
               {
                   entity
                    .ToTable("log")
                    .HasKey(p => p.Id)
                    .HasName("pk_log_id");
                   entity
                    .Property(e => e.Id)
                    .HasColumnName("id");
                   entity
                    .Property(e => e.Message)
                    .HasColumnName("message");
                   entity
                    .Property(e => e.RegisteredAt)
                    .HasColumnName("registered_at");
                   entity
                    .Property(e => e.SystemId)
                    .HasColumnName("system_id");
                   entity
                    .Property(e => e.StackTrace)
                    .HasColumnName("stack_trace");
                   entity
                    .Property(e => e.Level)
                    .HasColumnName("level");
                   entity
                     .Property(e => e.Labels)
                     .HasColumnType("text[]")
                     .HasColumnName("labels");
                   entity
                     .Property(e => e.SearchVector)
                     .HasColumnName("s_vector");
                   entity
                     .HasIndex(p => p.SearchVector)
                     .HasMethod("GIN");
               });
        }
    }
}

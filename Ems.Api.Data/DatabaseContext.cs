// <copyright file="DatabaseContext.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
    using Microsoft.EntityFrameworkCore;

    [ExcludeFromCodeCoverage]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            this.Database.EnsureCreated();

            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<EmployeeDto>? Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=test.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmployeeDto>(b =>
            {
                b.HasKey(e => e.EmployeeId);
                b.Property(e => e.EmployeeId).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(builder);
        }
    }
}

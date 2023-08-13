using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetProject.Models;

    public class DotNetProjectContext : DbContext
    {
        public DotNetProjectContext (DbContextOptions<DotNetProjectContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetProject.Models.Movies> Movies { get; set; } = default!;

        public DbSet<DotNetProject.Models.PricingDetails> PricingDetails { get; set; } = default!;

        public DbSet<DotNetProject.Models.Booking> Booking { get; set; } = default!;

        public DbSet<DotNetProject.Models.MovieHall> MovieHall { get; set; } = default!;

        public DbSet<DotNetProject.Models.ShowTiming> ShowTiming { get; set; } = default!;
    public DbSet<UserCred> userCreds { get; set; } = default!;
    public DbSet<AdminCred> adminCreds { get; set; } = default!;
    }

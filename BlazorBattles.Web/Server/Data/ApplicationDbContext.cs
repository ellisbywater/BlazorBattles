﻿using BlazorBattles.Web.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Web.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using freddo_index.Models;

namespace freddo_index.Data
{
    public class FreddoIndexContext : DbContext
    {
        public FreddoIndexContext(DbContextOptions<FreddoIndexContext> options) : base(options)
        {
        }
        public DbSet<PriceChangePoint> PriceChangePoints { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceChangePoint>().ToTable("PriceChangePoint");
        }
    }
}

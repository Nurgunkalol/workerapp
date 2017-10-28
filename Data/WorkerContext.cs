using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkersApp.Entities;

namespace WorkersApp.Data
{
    public class WorkersContext : DbContext
    {
        public WorkersContext(DbContextOptions<WorkersContext> options): base(options) { }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

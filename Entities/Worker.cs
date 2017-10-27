using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WorkersApp.Entities
{
    public class WorkersContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=workers.db");
        }
    }

    public class Worker
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public DateTime EntryWorkDate { get; set; }
        public double BaseRate { get; set; }
    }

    public enum Group
    {
        Employee,
        Manager,
        Salesman
    }
}

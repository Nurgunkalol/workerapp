using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkersApp.Entities
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public Group Group { get; set; }
        public DateTime EntryWorkDate { get; set; }
        public decimal BaseRate { get; set; }
        public Worker Chief { get; set; }        
        public int? ChiefId { get; set; }
    }

    public enum Group
    {
        Employee,
        Manager,
        Salesman
    }
}

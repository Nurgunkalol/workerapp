using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkersApp.Entities;

namespace WorkersApp.REST.Models
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя сотрудника")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выберите группу сотрудника")]
        public Group? Group { get; set; }
        [Required]
        public DateTime? EntryWorkDate { get; set; }
        [Required(ErrorMessage = "Выберите базовую ставку")]
        public decimal? BaseRate { get; set; }
        public WorkerViewModel Chief { get; set; }
        public int? ChiefId { get; set; }
    }

    public class WorkerSalaryViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }

    public class StatusViewModel
    {
        public bool Success { get; set; }

        public StatusViewModel(bool success)
        {
            Success = success;
        }
    }
}

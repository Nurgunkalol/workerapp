using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkersApp.Entities;
using WorkersApp.Data;

namespace WorkersApp.Services
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkersContext _context;

        public WorkerRepository(WorkersContext context)
        {
            _context = context;
        }

        public List<Worker> GetAll()
        {
            return _context.Workers.ToList();
        }

        public Worker Get(int id)
        {
            var worker = _context.Workers.FirstOrDefault(t => t.Id == id);
            return worker;
        }

        [HttpPost]
        public void Post(Worker worker)
        {
            _context.Workers.Add(worker);
            _context.SaveChanges();
        }

        public void Put(int id, [FromBody]Worker worker)
        {
            _context.Workers.Update(worker);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var worker = _context.Workers.FirstOrDefault(t => t.Id == id);
            if (worker == null)
                return;
            _context.Workers.Remove(worker);
            _context.SaveChanges();
        }
    }
}

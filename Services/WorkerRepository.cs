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

        public List<Worker> GetSubordinates(int id)
        {
            var subordinates = _context.Workers.Where(w => w.ChiefId == id).ToList();
            return subordinates;
        }

        public List<Worker> GetPotentialSubordinates(int id)
        {
            var worker = _context.Workers.FirstOrDefault(t => t.Id == id);
            var potentialSubs = _context.Workers.Where(w => w.Id != worker.Id && w.Id != worker.ChiefId && w.ChiefId != worker.Id).ToList();
            return potentialSubs;
        }

        public void CreateSubordinate(int workerId, int newSubId)
        {
            var worker = Get(newSubId);
            worker.ChiefId = workerId;
            _context.SaveChanges();
        }

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

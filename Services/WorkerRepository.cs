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

        public decimal GetWorkerSalary(int id, DateTime date)
        {

            var worker = Get(id);
            if (worker == null) // возможная ошибка при удалении сотрудника другим администратором во время данного сеанса
                return 0;

            var entryWorkDate = worker.EntryWorkDate;
            if (entryWorkDate > date) { return 0; } // ошибка выбора даты
            int totalYears = date.Year - entryWorkDate.Year; // разница лет между датой поступления на работу и выбранным датой
            var baseRate = worker.BaseRate;

            if (worker.Group == Group.Employee)
            {              
                if (totalYears >= 10) { return baseRate + baseRate * 0.3M; } // не больше 30% (3% в год)
                return baseRate + baseRate * 0.03M * totalYears;   
                
            } else if (worker.Group == Group.Manager)
            {
                var employeeSubs = GetSubordinates(worker.Id).Where(s => s.Group == Group.Employee).ToList(); // в более адекватной архитектуре, я бы привязал список подчиненных к начальнику, чтобы не тянуть список всех подчиненных с БД
                var allowanceEmpSubs = employeeSubs.Count * 0.005M;
                if (totalYears >= 8) { return baseRate + baseRate * 0.4M + allowanceEmpSubs; } // не больше 40% (5% в год)
                return baseRate + baseRate * 0.05M * totalYears + allowanceEmpSubs;

            } else if (worker.Group == Group.Salesman)
            {
                var subs = GetSubordinates(worker.Id).ToList();
                var allowanceSubs = subs.Count * 0.003M;
                if (totalYears >= 35) { return baseRate + baseRate * 0.35M + allowanceSubs; } // не больше 35% (1% в год)
                return baseRate + baseRate * 0.01M * totalYears + allowanceSubs;
            }
            return 0;
        }
    }
}

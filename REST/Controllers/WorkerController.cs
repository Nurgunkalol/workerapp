using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkersApp.Services;
using WorkersApp.REST.Models;
using WorkersApp.Entities;
using AutoMapper;
using WorkersApp.Infrastructure;

namespace WorkersApp.Controllers
{
    [ValidateModel]
    [Route("api/[controller]")]
    public class WorkerController : Controller
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IMapper _mapper;

        public WorkerController(IWorkerRepository workerRepository, IMapper mapper)
        {
            _workerRepository = workerRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IEnumerable<WorkerViewModel> GetWorkers()
        {
            var workers = _workerRepository.GetAll();
            return _mapper.Map<List<Worker>, List<WorkerViewModel>>(workers);
        }

        [HttpGet("{id}")]
        public WorkerViewModel Get(int id)
        {
            var worker = _workerRepository.Get(id);
            return _mapper.Map<Worker, WorkerViewModel>(worker);
        }

        [HttpGet("subordinates/{id}")]
        public IEnumerable<WorkerViewModel> GetSubordinates(int id)
        {
            var subordinates = _workerRepository.GetSubordinates(id);
            return _mapper.Map<List<Worker>, List<WorkerViewModel>>(subordinates);
        }

        [HttpGet("potentialsubs/{id}")]
        public IEnumerable<WorkerViewModel> GetPotentialSubordinates(int id)
        {
            var subordinates = _workerRepository.GetPotentialSubordinates(id);
            return _mapper.Map<List<Worker>, List<WorkerViewModel>>(subordinates);
        }

        [HttpGet("createsub/{workerId}/{newSubId}")]
        public void CreateSubordinate(int workerId, int newSubId)
        {
            _workerRepository.CreateSubordinate(workerId, newSubId);
        }

        [HttpPost]
        public void Post([FromBody]WorkerViewModel worker)
        {
            var workerUpdate = _mapper.Map<WorkerViewModel, Worker>(worker);
            _workerRepository.Post(workerUpdate);           
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]WorkerViewModel worker)
        {
            var workerUpdate = _mapper.Map<WorkerViewModel, Worker>(worker);
            _workerRepository.Put(id, workerUpdate);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _workerRepository.Delete(id);
        }
    }
}

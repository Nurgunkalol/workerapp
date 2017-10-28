using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorkersApp.Entities;
using WorkersApp.REST.Models;

namespace WorkersApp.Infrastructure
{
    public class AutoMapperConfig
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<Worker, WorkerViewModel>();
            config.CreateMap<WorkerViewModel, Worker>();
        }
    }
}

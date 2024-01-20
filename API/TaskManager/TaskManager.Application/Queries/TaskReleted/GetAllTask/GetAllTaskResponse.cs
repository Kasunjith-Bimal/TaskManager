using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Queries.TaskReleted.GetAllTask
{
    public class GetAllTaskResponse
    {
        public List<TaskDetail> tasks { get; set; }

    }
}

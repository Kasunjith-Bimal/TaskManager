using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Command.TaskReleted.UpdateTask
{
    public class UpdateTaskCommand
    {
        public TaskDetail taskDetail { get; set; }

        public long Id { get; set; }
     
    }
}

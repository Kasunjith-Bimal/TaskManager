using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence.EFCore
{
    public class TaskManagerDbContext : DbContext 
    {
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
        }

        public DbSet<TaskDetail> TaskDetails { get; set; }
    }
}

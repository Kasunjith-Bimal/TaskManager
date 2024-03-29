﻿using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Application.Command.TaskReleted.CreateTask;
using TaskManager.Application.Command.TaskReleted.DeleteTask;
using TaskManager.Application.Command.TaskReleted.UpdateTask;
using TaskManager.Application.Queries.TaskReleted.GetAllTask;
using TaskManager.Application.Queries.TaskReleted.GetById;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Intefaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<TaskController> logger;

       
        public TaskController(IMediator mediator, ILogger<TaskController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var client = this.mediator.CreateRequestClient<GetAllTaskQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetAllTaskResponse>>(new GetAllTaskQuery
                {
                   
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:GetAllTasks");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in TaskController:GetAllTasks. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:GetAllTasks. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetail>> GetTaskById(long id)
        {
            //var task = await taskService.GetTaskByIdAsync(id);
            //if (task == null)
            //    return NotFound();

            //return Ok(task);
            try
            {
                var client = this.mediator.CreateRequestClient<GetByIdQuery>();
                var response = await client.GetResponse<ResponseWrapper<GetByIdResponse>>(new GetByIdQuery
                {
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:GetTaskById");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in TaskController:GetTaskById. Message: {response.Message.Message}");
                    return NotFound(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:GetTaskById . Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskDetail task)
        {
            try
            {
                var client = this.mediator.CreateRequestClient<CreateTaskCommand>();
                var response = await client.GetResponse<ResponseWrapper<CreateTaskResponse>>(new CreateTaskCommand
                {
                    taskDetail = task
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:AddTask");
                    //return Ok(response.Message);
                    return CreatedAtAction(nameof(GetTaskById), new { id = response.Message.Payload.task.Id }, response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in TaskController:AddTask. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Exception occurred in TaskController:AddTask. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(long id, TaskDetail task)
        {
            try
            {
                //if (id != task.Id)
                //    return BadRequest();

                var client = this.mediator.CreateRequestClient<UpdateTaskCommand>();
                var response = await client.GetResponse<ResponseWrapper<UpdateTaskResponse>>(new UpdateTaskCommand
                {
                    taskDetail = task,
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:UpdateTask");
                    return Ok(response.Message);
                }
                else
                {
                    this.logger.LogInformation($"Event not succeeded in TaskController:UpdateTask. Message: {response.Message.Message}");
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {

                this.logger.LogInformation($"Exception occurred in TaskController:UpdateTask. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(long id)
        {
            //var result = await taskService.DeleteTask(id);
            //if (!result)
            //    return NotFound();

            //return NoContent();

            try
            {
                var client = this.mediator.CreateRequestClient<DeleteTaskCommand>();
                var response = await client.GetResponse<ResponseWrapper<DeleteTaskResponse>>(new DeleteTaskCommand
                {
                    Id = id
                });

                if (response.Message.Succeeded)
                {
                    this.logger.LogInformation($"Event succeeded in TaskController:DeleteTask");
                    return Ok(response.Message);
                }
                else
                { 
                    this.logger.LogInformation($"Event not succeeded in TaskController:DeleteTask. Message: {response.Message.Message}");
                    return NotFound(response.Message);

                }
            }
            catch (Exception ex)
            {

                this.logger.LogInformation($"Exception occurred in TaskController:UpdateTask. Message: {ex.Message} - Exception: {ex.InnerException?.ToString()} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex);
            }
        }

    }
}

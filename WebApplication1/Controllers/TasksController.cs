using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Models;
using todo_api.Services;
using System.Collections.Generic;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Task>> GetTasks(bool? isCompleted)
        {
            if (isCompleted.HasValue)
            {
                return Ok(_taskService.GetTasksByStatus(isCompleted.Value));
            }
            return Ok(_taskService.GetAllTasks());
        }


        [HttpGet("{id}")]
        public ActionResult<Models.Task> GetTask(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Models.Task> CreateTask([FromBody] Models.Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Title cannot be empty");
            }
            var createdTask = _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public ActionResult<Models.Task> UpdateTask(int id, [FromBody] Models.Task task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Title cannot be empty");
            }
            var updatedTask = _taskService.UpdateTask(id, task);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
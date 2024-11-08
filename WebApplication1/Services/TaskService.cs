using todo_api.Data;
using System.Collections.Generic;
using System.Linq;
using todo_api.Data;
using todo_api.Models;

namespace todo_api.Services
{
    public interface ITaskService
    {
        IEnumerable<Models.Task> GetAllTasks();
        IEnumerable<Models.Task> GetTasksByStatus(bool isCompleted);
        Models.Task GetTaskById(int id);
        Models.Task CreateTask(Models.Task task);
        Models.Task UpdateTask(int id, Models.Task task);
        bool DeleteTask(int id);
    }

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Task> GetAllTasks()
        {
            return (IEnumerable<Models.Task>)_context.Tasks.ToList();
        }

        public IEnumerable<Models.Task> GetTasksByStatus(bool isCompleted)
        {
            return _context.Tasks.Where(t => t.IsCompleted == isCompleted).ToList();
        }

        public Models.Task GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public Models.Task CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public Models.Task UpdateTask(int id, Models.Task task)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return null;
            }
            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
            _context.SaveChanges();
            return existingTask;
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
    }
}

using System;
using System.Linq;
using TodoList.Data;
using TodoList.Data.Models;
using TodoList.Models;
using TodoList.Services.Contracts;

namespace TodoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext context;

        public TaskService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task Create(TaskInputModel model, string userId)
        {
            Task task = new Task
            {
                Content = model.Content,
                IdentityUserId = userId,
                EndDate = model.EndDate,
                StartDate = DateTime.UtcNow
            };

            this.context.Tasks.Add(task);
            this.context.SaveChanges();
            return task;
        }

        public void Delete(Task task)
        {
            this.context.Tasks.Remove(task);
            this.context.SaveChanges();
        }

        public Task Edit(TaskEditInputModel model)
        {
            Task task = this.GetById(model.Id);
            if(task == null)
            {
                throw new ArgumentException("The task id is invalid!");
            }
            if(model.EndDate < task.StartDate)
            {
                throw new ArgumentException("The end date must be after start date!");
            }

            task.Content = model.Content;
            task.EndDate = model.EndDate;
            context.SaveChanges();
            return task;
        }

        public Task GetById(int taskId)
        {
            return context.Tasks.Find(taskId);
        }

        public string GetCreatorId(int taskId)
        {
            Task task = this.GetById(taskId);
            return task?.IdentityUserId;
        }

        public TaskViewModel[] TasksByUser(string userId)
        {
            return this.context.Tasks
                .Where(t => t.IdentityUserId == userId)
                .Select(t => new TaskViewModel
                {
                    Content = t.Content,
                    EndDate = t.EndDate,
                    StratDate = t.StartDate,
                    Id = t.Id
                })
                .ToArray();
        }
    }
}

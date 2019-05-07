using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data.Models;
using TodoList.Models;
using TodoList.Services.Contracts;

namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        private const string TaskDoNotExistMessage = "There is no such task!";

        private readonly ITaskService taskService;
        private readonly UserManager<IdentityUser> userManager;

        public TaskController(ITaskService taskService, UserManager<IdentityUser> userManager)
        {
            this.taskService = taskService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            string userId = userManager.GetUserId(this.User);
            TaskViewModel[] tasks = taskService.TasksByUser(userId);
            return View(tasks);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Task> Create(TaskInputModel model)
        {
            bool endDateIsValid = model.EndDate < DateTime.Now;
            if (!ModelState.IsValid || endDateIsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                if (endDateIsValid)
                {
                    errors.Add("The end date must be after start date!");
                }
                return BadRequest(errors);
            }
            string userId = this.userManager.GetUserId(this.User);
            Task  task = taskService.Create(model, userId);
            return task;
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Task> Edit(TaskEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            string creatorId = taskService.GetCreatorId(model.Id);
            if(creatorId == null || creatorId != userManager.GetUserId(this.User))
            {
                return Unauthorized();
            }

            Task task = null;
            try
            {
                task = taskService.Edit(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return task;
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Task task = taskService.GetById(id);

            if (task == null)
            {
                return BadRequest(TaskDoNotExistMessage);
            }

            if (task.IdentityUserId != this.userManager.GetUserId(this.User))
            {
                return Unauthorized();
            }

            taskService.Delete(task);
            return NoContent();
        }
    }
}
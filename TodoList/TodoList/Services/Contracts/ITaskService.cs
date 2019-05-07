using TodoList.Data.Models;
using TodoList.Models;

namespace TodoList.Services.Contracts
{
    public interface ITaskService
    {
        Task Create(TaskInputModel model, string userId);

        Task Edit(TaskEditInputModel model);

        void Delete(Task task);

        Task GetById(int taskId);

        TaskViewModel[] TasksByUser(string userId);

        string GetCreatorId(int taskId);
    }
}

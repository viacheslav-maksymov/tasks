using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Log.Models;

namespace Tasks.Log.Interfaces
{
    public interface ILogDatabaseRepository
    {
        Task<List<TaskUpdateLog>> GetAllTaskUpdateLogsAsync();

        Task<List<TaskUpdateLog>> GetTaskUpdateLogsByTaskAsync(int taskId);

        Task<bool> FileUpdateTaskLogAsync(TaskUpdateLog taskUpdateLog);

        Task<bool> DeleteUpdateTaskLogAsync(string taskId);
    }
}

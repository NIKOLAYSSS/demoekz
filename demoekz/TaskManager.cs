using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoekz
{
    public class TaskManager
    {
        private DatabaseManager databaseManager;

        public TaskManager(DatabaseManager databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public void AddTask(Task task)
        {
            databaseManager.AddTask(task);
        }

        public List<Task> GetTasks()
        {
            return databaseManager.GetTasks();
        }

        public void DeleteTask(Guid taskId)
        {
            databaseManager.DeleteTask(taskId);
        }

        public void UpdateTask(Task task)
        {
            databaseManager.UpdateTask(task);
        }

        public List<Task> SearchTasks(string searchTerm)
        {
            return databaseManager.SearchTasks(searchTerm);
        }

        public List<Task> CheckTaskDeadlines()
        {
            return databaseManager.CheckTaskDeadlines();
        }

        public (int completedTasks, double? averageCompletionTime, DataTable projectStatistics, DataTable executorStatistics) CalculateStatistics(DateTime startDate, DateTime endDate)
        {
            return databaseManager.CalculateStatistics(startDate, endDate);
        }
    }
}

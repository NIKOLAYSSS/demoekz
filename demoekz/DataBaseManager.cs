using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoekz
{
    public class DatabaseManager
    {
        private string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddTask(Task task)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Проверка и добавление проекта
                    cmd.CommandText = "SELECT ProjectId FROM Projects WHERE ProjectName = @ProjectName";
                    cmd.Parameters.AddWithValue("ProjectName", task.ProjectName);
                    var projectId = cmd.ExecuteScalar();
                    if (projectId == null)
                    {
                        cmd.CommandText = "INSERT INTO Projects (ProjectName) VALUES (@ProjectName) RETURNING ProjectId";
                        projectId = cmd.ExecuteScalar();
                    }

                    // Проверка и добавление приоритета
                    cmd.CommandText = "SELECT PriorityId FROM Priorities WHERE PriorityName = @Priority";
                    cmd.Parameters.AddWithValue("Priority", task.Priority);
                    var priorityId = cmd.ExecuteScalar();
                    if (priorityId == null)
                    {
                        cmd.CommandText = "INSERT INTO Priorities (PriorityName) VALUES (@Priority) RETURNING PriorityId";
                        priorityId = cmd.ExecuteScalar();
                    }

                    // Проверка и добавление статуса
                    cmd.CommandText = "SELECT StatusId FROM Statuses WHERE StatusName = @Status";
                    cmd.Parameters.AddWithValue("Status", task.Status);
                    var statusId = cmd.ExecuteScalar();
                    if (statusId == null)
                    {
                        cmd.CommandText = "INSERT INTO Statuses (StatusName) VALUES (@Status) RETURNING StatusId";
                        statusId = cmd.ExecuteScalar();
                    }

                    // Проверка и добавление исполнителя
                    cmd.CommandText = "SELECT AssigneeId FROM Assignees WHERE AssigneeName = @Assignee";
                    cmd.Parameters.AddWithValue("Assignee", task.Assignee);
                    var assigneeId = cmd.ExecuteScalar();
                    if (assigneeId == null)
                    {
                        cmd.CommandText = "INSERT INTO Assignees (AssigneeName) VALUES (@Assignee) RETURNING AssigneeId";
                        assigneeId = cmd.ExecuteScalar();
                    }

                    // Добавление задачи
                    cmd.CommandText = @"
                INSERT INTO Tasks (TaskNumber, CreationDate, ProjectId, TaskDescription, PriorityId, StatusId, AssigneeId, CompletionDate, CreatedBy)
                VALUES (@TaskNumber, @CreationDate, @ProjectId, @TaskDescription, @PriorityId, @StatusId, @AssigneeId, @CompletionDate, @CreatedBy)";
                    cmd.Parameters.AddWithValue("TaskNumber", task.TaskNumber);
                    cmd.Parameters.AddWithValue("CreationDate", task.CreationDate);
                    cmd.Parameters.AddWithValue("ProjectId", projectId);
                    cmd.Parameters.AddWithValue("TaskDescription", task.TaskDescription);
                    cmd.Parameters.AddWithValue("PriorityId", priorityId);
                    cmd.Parameters.AddWithValue("StatusId", statusId);
                    cmd.Parameters.AddWithValue("AssigneeId", assigneeId);
                    cmd.Parameters.AddWithValue("CompletionDate", task.CompletionDate);
                    cmd.Parameters.AddWithValue("CreatedBy", task.CreatedBy);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<Task> GetTasks()
        {
            var tasks = new List<Task>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        SELECT t.TaskId, t.TaskNumber, t.CreationDate, p.ProjectName, t.TaskDescription, pr.PriorityName, a.AssigneeName, s.StatusName, t.CompletionDate, t.CreatedBy
                        FROM Tasks t
                        JOIN Projects p ON t.ProjectId = p.ProjectId
                        JOIN Priorities pr ON t.PriorityId = pr.PriorityId
                        JOIN Statuses s ON t.StatusId = s.StatusId
                        JOIN Assignees a ON t.AssigneeId = a.AssigneeId";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                TaskId = Guid.Parse(reader["TaskId"].ToString()),
                                TaskNumber = int.Parse(reader["TaskNumber"].ToString()),
                                CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                                ProjectName = reader["ProjectName"].ToString(),
                                TaskDescription = reader["TaskDescription"].ToString(),
                                Priority = reader["PriorityName"].ToString(),
                                Assignee = reader["AssigneeName"].ToString(),
                                Status = reader["StatusName"].ToString(),
                                CompletionDate = DateTime.Parse(reader["CompletionDate"].ToString()),
                                CreatedBy = reader["CreatedBy"].ToString()
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public void DeleteTask(Guid taskId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Tasks WHERE TaskId = @TaskId";
                    cmd.Parameters.AddWithValue("TaskId", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTask(Task task)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        UPDATE Tasks
                        SET TaskNumber = @TaskNumber,
                            CreationDate = @CreationDate,
                            ProjectId = (SELECT ProjectId FROM Projects WHERE ProjectName = @ProjectName),
                            TaskDescription = @TaskDescription,
                            PriorityId = (SELECT PriorityId FROM Priorities WHERE PriorityName = @Priority),
                            StatusId = (SELECT StatusId FROM Statuses WHERE StatusName = @Status),
                            AssigneeId = (SELECT AssigneeId FROM Assignees WHERE AssigneeName = @Assignee),
                            CompletionDate = @CompletionDate
                        WHERE TaskId = @TaskId";
                    cmd.Parameters.AddWithValue("TaskId", task.TaskId);
                    cmd.Parameters.AddWithValue("TaskNumber", task.TaskNumber);
                    cmd.Parameters.AddWithValue("CreationDate", task.CreationDate);
                    cmd.Parameters.AddWithValue("ProjectName", task.ProjectName);
                    cmd.Parameters.AddWithValue("TaskDescription", task.TaskDescription);
                    cmd.Parameters.AddWithValue("Priority", task.Priority);
                    cmd.Parameters.AddWithValue("Assignee", task.Assignee);
                    cmd.Parameters.AddWithValue("Status", task.Status);
                    cmd.Parameters.AddWithValue("CompletionDate", task.CompletionDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Task> SearchTasks(string searchTerm)
        {
            var tasks = new List<Task>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        SELECT t.TaskId, t.TaskNumber, t.CreationDate, p.ProjectName, t.TaskDescription, pr.PriorityName, a.AssigneeName, s.StatusName, t.CompletionDate, t.CreatedBy
                        FROM Tasks t
                        JOIN Projects p ON t.ProjectId = p.ProjectId
                        JOIN Priorities pr ON t.PriorityId = pr.PriorityId
                        JOIN Statuses s ON t.StatusId = s.StatusId
                        JOIN Assignees a ON t.AssigneeId = a.AssigneeId
                        WHERE CAST(t.TaskNumber AS TEXT) = @searchTerm OR t.TaskDescription ILIKE @searchTerm";
                    cmd.Parameters.AddWithValue("searchTerm", searchTerm);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                TaskId = Guid.Parse(reader["TaskId"].ToString()),
                                TaskNumber = int.Parse(reader["TaskNumber"].ToString()),
                                CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                                ProjectName = reader["ProjectName"].ToString(),
                                TaskDescription = reader["TaskDescription"].ToString(),
                                Priority = reader["PriorityName"].ToString(),
                                Assignee = reader["AssigneeName"].ToString(),
                                Status = reader["StatusName"].ToString(),
                                CompletionDate = DateTime.Parse(reader["CompletionDate"].ToString()),
                                CreatedBy = reader["CreatedBy"].ToString()
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public List<Task> CheckTaskDeadlines()
        {
            var tasks = new List<Task>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        SELECT t.TaskNumber, t.TaskDescription
                        FROM Tasks t
                        JOIN Statuses s ON t.StatusId = s.StatusId
                        WHERE s.StatusName != 'выполнено' AND t.CompletionDate <= NOW()";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                TaskNumber = int.Parse(reader["TaskNumber"].ToString()),
                                TaskDescription = reader["TaskDescription"].ToString()
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public (int completedTasks, double? averageCompletionTime, DataTable projectStatistics, DataTable executorStatistics) CalculateStatistics(DateTime startDate, DateTime endDate)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Количество выполненных задач за указанный период
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM Tasks t
                JOIN Statuses s ON t.StatusId = s.StatusId
                WHERE s.StatusName = 'выполнено' AND t.CompletionDate BETWEEN @startDate AND @endDate";
                    cmd.Parameters.AddWithValue("startDate", startDate);
                    cmd.Parameters.AddWithValue("endDate", endDate);
                    int completedTasks = Convert.ToInt32(cmd.ExecuteScalar());

                    // Среднее время выполнения задачи в днях
                    cmd.CommandText = @"
                SELECT AVG(t.CompletionDate - t.CreationDate)
                FROM Tasks t
                JOIN Statuses s ON t.StatusId = s.StatusId
                WHERE s.StatusName = 'выполнено' AND t.CompletionDate BETWEEN @startDate AND @endDate";
                    cmd.Parameters.AddWithValue("startDate", startDate);
                    cmd.Parameters.AddWithValue("endDate", endDate);
                    object averageCompletionTimeObj = cmd.ExecuteScalar();
                    double? averageCompletionTime = averageCompletionTimeObj != DBNull.Value ? ((TimeSpan)averageCompletionTimeObj).TotalDays : (double?)null;

                    // Статистика по проектам
                    cmd.CommandText = @"
                SELECT p.ProjectName, COUNT(*)
                FROM Tasks t
                JOIN Projects p ON t.ProjectId = p.ProjectId
                JOIN Statuses s ON t.StatusId = s.StatusId
                WHERE s.StatusName = 'выполнено' AND t.CompletionDate BETWEEN @startDate AND @endDate
                GROUP BY p.ProjectName";
                    cmd.Parameters.AddWithValue("startDate", startDate);
                    cmd.Parameters.AddWithValue("endDate", endDate);
                    var projectStatistics = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        projectStatistics.Load(reader);
                    }

                    // Статистика по исполнителям
                    cmd.CommandText = @"
                SELECT a.AssigneeName, COUNT(*)
                FROM Tasks t
                JOIN Assignees a ON t.AssigneeId = a.AssigneeId
                JOIN Statuses s ON t.StatusId = s.StatusId
                WHERE s.StatusName = 'выполнено' AND t.CompletionDate BETWEEN @startDate AND @endDate
                GROUP BY a.AssigneeName";
                    cmd.Parameters.AddWithValue("startDate", startDate);
                    cmd.Parameters.AddWithValue("endDate", endDate);
                    var executorStatistics = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        executorStatistics.Load(reader);
                    }

                    return (completedTasks, averageCompletionTime, projectStatistics, executorStatistics);
                }
            }
        }

    }
}

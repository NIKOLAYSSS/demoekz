using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Xceed.Words.NET;
using System.Windows.Forms;
using System.IO;

namespace demoekz
{
    public partial class Form1 : Form
    {
        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=task_managment";
        private string currentUser;
        private string currentUserRole;
        private TaskManager taskManager;

        public Form1(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentUserRole = role;
            taskManager = new TaskManager(new DatabaseManager(connectionString));
            notificationTimer = new Timer();
            notificationTimer.Interval = 60000; // 60 секунд
            notificationTimer.Tick += notificationTimer_Tick;
            notificationTimer.Start();
            EnableControlsBasedOnRole();
            LoadTasks();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            var taskForm = new TaskForm(taskManager, currentUser);
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                LoadTasks();
            }
        }

        private void LoadTasks()
        {
            try
            {
                var tasks = taskManager.GetTasks();
                var dt = new DataTable();
                dt.Columns.Add("TaskId", typeof(Guid));
                dt.Columns.Add("TaskNumber", typeof(int));
                dt.Columns.Add("CreationDate", typeof(DateTime));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("TaskDescription", typeof(string));
                dt.Columns.Add("Priority", typeof(string));
                dt.Columns.Add("Assignee", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("CompletionDate", typeof(DateTime));
                dt.Columns.Add("CreatedBy", typeof(string));

                foreach (var task in tasks)
                {
                    dt.Rows.Add(task.TaskId, task.TaskNumber, task.CreationDate, task.ProjectName, task.TaskDescription, task.Priority, task.Assignee, task.Status, task.CompletionDate, task.CreatedBy);
                }

                dgvTasks.DataSource = dt;
                dgvTasks.Columns["TaskId"].HeaderText = "ID задачи";
                dgvTasks.Columns["TaskNumber"].HeaderText = "Номер задачи";
                dgvTasks.Columns["CreationDate"].HeaderText = "Дата создания";
                dgvTasks.Columns["ProjectName"].HeaderText = "Название проекта";
                dgvTasks.Columns["TaskDescription"].HeaderText = "Описание задачи";
                dgvTasks.Columns["Priority"].HeaderText = "Приоритет";
                dgvTasks.Columns["Assignee"].HeaderText = "Исполнитель";
                dgvTasks.Columns["Status"].HeaderText = "Статус";
                dgvTasks.Columns["CompletionDate"].HeaderText = "Дата завершения";
                dgvTasks.Columns["CreatedBy"].HeaderText = "Создано пользователем";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnRefreshTasks_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                Guid taskId = Guid.Parse(dgvTasks.SelectedRows[0].Cells["TaskId"].Value.ToString());
                try
                {
                    taskManager.DeleteTask(taskId);
                    MessageBox.Show("Задача успешно удалена!");
                    LoadTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void dgvTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTasks.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "в ожидании":
                        e.CellStyle.BackColor = Color.Yellow;
                        break;
                    case "в работе":
                        e.CellStyle.BackColor = Color.Orange;
                        break;
                    case "выполнено":
                        e.CellStyle.BackColor = Color.Green;
                        break;
                }
            }
        }

        private void btnEditTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count > 0)
            {
                Guid taskId = Guid.Parse(dgvTasks.SelectedRows[0].Cells["TaskId"].Value.ToString());
                var task = taskManager.GetTasks().Find(t => t.TaskId == taskId);
                var taskForm = new TaskForm(taskManager, currentUser, task);
                if (taskForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTasks();
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для редактирования.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                SearchTasks(searchTerm);
            }
            else
            {
                MessageBox.Show("Введите номер задачи или ключевые слова для поиска.");
            }
        }

        private void SearchTasks(string searchTerm)
        {
            try
            {
                var tasks = taskManager.SearchTasks(searchTerm);
                var dt = new DataTable();
                dt.Columns.Add("TaskId", typeof(Guid));
                dt.Columns.Add("TaskNumber", typeof(int));
                dt.Columns.Add("CreationDate", typeof(DateTime));
                dt.Columns.Add("ProjectName", typeof(string));
                dt.Columns.Add("TaskDescription", typeof(string));
                dt.Columns.Add("Priority", typeof(string));
                dt.Columns.Add("Assignee", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("CompletionDate", typeof(DateTime));
                dt.Columns.Add("CreatedBy", typeof(string));

                foreach (var task in tasks)
                {
                    dt.Rows.Add(task.TaskId, task.TaskNumber, task.CreationDate, task.ProjectName, task.TaskDescription, task.Priority, task.Assignee, task.Status, task.CompletionDate, task.CreatedBy);
                }

                dgvTasks.DataSource = dt;
                dgvTasks.Columns["TaskId"].HeaderText = "ID задачи";
                dgvTasks.Columns["TaskNumber"].HeaderText = "Номер задачи";
                dgvTasks.Columns["CreationDate"].HeaderText = "Дата создания";
                dgvTasks.Columns["ProjectName"].HeaderText = "Название проекта";
                dgvTasks.Columns["TaskDescription"].HeaderText = "Описание задачи";
                dgvTasks.Columns["Priority"].HeaderText = "Приоритет";
                dgvTasks.Columns["Assignee"].HeaderText = "Исполнитель";
                dgvTasks.Columns["Status"].HeaderText = "Статус";
                dgvTasks.Columns["CompletionDate"].HeaderText = "Дата завершения";
                dgvTasks.Columns["CreatedBy"].HeaderText = "Создано пользователем";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void notificationTimer_Tick(object sender, EventArgs e)
        {
            CheckTaskDeadlines();
        }

        private void CheckTaskDeadlines()
        {
            try
            {
                var tasks = taskManager.CheckTaskDeadlines();
                foreach (var task in tasks)
                {
                    MessageBox.Show($"Задача {task.TaskNumber}: {task.TaskDescription} просрочена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCalculateStatistics_Click(object sender, EventArgs e)
        {
            var statisticsForm = new StatisticsForm(taskManager);
            statisticsForm.ShowDialog();
        }

        private void BtnGenerateQRCode_Click(object sender, EventArgs e)
        {
            var qrCodeForm = new QRCodeForm();
            qrCodeForm.ShowDialog();
        }

        private void EnableControlsBasedOnRole()
        {
            switch (currentUserRole)
            {
                case "администратор":
                    EnableAllControls();
                    break;
                case "руководитель":
                    EnableManagerControls();
                    break;
                case "сотрудник":
                    EnableEmployeeControls();
                    break;
                default:
                    DisableControls();
                    break;
            }
        }

        private void EnableAllControls()
        {
            btnRefreshTasks.Visible = true;
            btnAddTask.Visible = true;
            btnDeleteTask.Visible = true;
            btnEditTask.Visible = true;
            btnCalculateStatistics.Visible = true;
        }

        private void EnableManagerControls()
        {
            btnRefreshTasks.Visible = true;
            btnAddTask.Visible = true;
            btnDeleteTask.Visible = true;
            btnEditTask.Visible = true;
            btnCalculateStatistics.Visible = true;
            btnGenerateQRCode.Visible = false;
        }

        private void EnableEmployeeControls()
        {
            btnRefreshTasks.Visible = true;
            btnAddTask.Visible = true;
            btnDeleteTask.Visible = true;
            btnEditTask.Visible = true;
            btnGenerateQRCode.Visible = true;
            btnCalculateStatistics.Visible = false;
        }

        private void DisableControls()
        {
            btnAddTask.Enabled = false;
            btnEditTask.Enabled = false;
            btnDeleteTask.Enabled = false;
            btnRefreshTasks.Enabled = false;
            btnSearch.Enabled = false;
            btnCalculateStatistics.Enabled = false;
        }

        private void btnDocumentation_Click(object sender, EventArgs e)
        {
            string docxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Николай\\source\\repos\\demoekz\\demoekz\\Demo.docx");

            try
            {
                if (File.Exists(docxPath))
                {
                    // Открываем документ в Microsoft Word
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = docxPath,
                        UseShellExecute = true // Используем системный обработчик по умолчанию
                    });
                }
                else
                {
                    MessageBox.Show("Файл документации не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия документации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

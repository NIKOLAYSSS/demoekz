using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using QRCoder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace demoekz
{
    public partial class Form1 : Form
    {
        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=task_managment";
        private Guid selectedTaskId;
        private string currentUser;
        private string currentUserRole;

        public Form1(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentUserRole = role;

            notificationTimer.Start();
            EnableControlsBasedOnRole();
            LoadTasks();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO tasks (TaskNumber, CreationDate, ProjectName, TaskDescription, Priority, Assignee, Status, CompletionDate, CreatedBy) VALUES (@номер_задачи, @дата_создания, @название_проекта, @описание_задачи, @приоритет, @исполнитель, @статус, @дата_завершения, @создано_пользователем)";
                        cmd.Parameters.AddWithValue("номер_задачи", int.Parse(txtTaskNumber.Text));
                        cmd.Parameters.AddWithValue("дата_создания", dtpCreationDate.Value.Date);
                        cmd.Parameters.AddWithValue("название_проекта", txtProjectName.Text);
                        cmd.Parameters.AddWithValue("описание_задачи", txtTaskDescription.Text);
                        cmd.Parameters.AddWithValue("приоритет", cmbPriority.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("исполнитель", txtExecutor.Text);
                        cmd.Parameters.AddWithValue("статус", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("дата_завершения", dtpDueDate.Value.Date);
                        cmd.Parameters.AddWithValue("создано_пользователем", currentUser);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Задача успешно добавлена!");
                LoadTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void LoadTasks()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT TaskId, TaskNumber, CreationDate, ProjectName, TaskDescription, Priority, Assignee, Status, CompletionDate, CreatedBy FROM Tasks";
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
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
                    }
                }
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
                selectedTaskId = Guid.Parse(dgvTasks.SelectedRows[0].Cells["TaskId"].Value.ToString());
                txtTaskNumber.Text = dgvTasks.SelectedRows[0].Cells["TaskNumber"].Value.ToString();
                dtpCreationDate.Value = (DateTime)dgvTasks.SelectedRows[0].Cells["CreationDate"].Value;
                txtProjectName.Text = dgvTasks.SelectedRows[0].Cells["ProjectName"].Value.ToString();
                txtTaskDescription.Text = dgvTasks.SelectedRows[0].Cells["TaskDescription"].Value.ToString();
                cmbPriority.SelectedItem = dgvTasks.SelectedRows[0].Cells["Priority"].Value.ToString();
                txtExecutor.Text = dgvTasks.SelectedRows[0].Cells["Assignee"].Value.ToString();
                cmbStatus.SelectedItem = dgvTasks.SelectedRows[0].Cells["Status"].Value.ToString();
                dtpDueDate.Value = (DateTime)dgvTasks.SelectedRows[0].Cells["CompletionDate"].Value;


                btnAddTask.Text = "Сохранить изменения";
                btnAddTask.Click -= btnAddTask_Click;
                btnAddTask.Click += btnSaveTask_Click;
            }
            else
            {
                MessageBox.Show("Выберите задачу для редактирования.");
            }
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE Tasks SET TaskNumber = @TaskNumber, CreationDate = @CreationDate, ProjectName = @ProjectName, TaskDescription = @TaskDescription, Priority = @Priority, Assignee = @Assignee, Status = @Status, CompletionDate = @CompletionDate WHERE TaskId = @TaskId";
                        cmd.Parameters.AddWithValue("TaskId", selectedTaskId);
                        cmd.Parameters.AddWithValue("TaskNumber", int.Parse(txtTaskNumber.Text));
                        cmd.Parameters.AddWithValue("CreationDate", dtpCreationDate.Value.Date);
                        cmd.Parameters.AddWithValue("ProjectName", txtProjectName.Text);
                        cmd.Parameters.AddWithValue("TaskDescription", txtTaskDescription.Text);
                        cmd.Parameters.AddWithValue("Priority", cmbPriority.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("Assignee", txtExecutor.Text);
                        cmd.Parameters.AddWithValue("Status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("CompletionDate", dtpDueDate.Value.Date);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Задача успешно обновлена!");
                LoadTasks();
                btnAddTask.Text = "Добавить задачу";
                btnAddTask.Click -= btnSaveTask_Click;
                btnAddTask.Click += btnAddTask_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
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
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT TaskId, TaskNumber, CreationDate, ProjectName, TaskDescription, Priority, Assignee, Status, CompletionDate, CreatedBy FROM Tasks WHERE CAST(TaskNumber AS TEXT) = @searchTerm OR TaskDescription ILIKE @searchTerm";
                        cmd.Parameters.AddWithValue("searchTerm", searchTerm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
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
                    }
                }
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
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT TaskNumber, TaskDescription FROM Tasks WHERE Status != 'выполнено' AND CompletionDate <= NOW()";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string taskNumber = reader["TaskNumber"].ToString();
                                string taskDescription = reader["TaskDescription"].ToString();
                                MessageBox.Show($"Задача {taskNumber}: {taskDescription} просрочена!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnCalculateStatistics_Click(object sender, EventArgs e)
        {
            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        // Количество выполненных задач за указанный период
                        cmd.CommandText = "SELECT COUNT(*) FROM Tasks WHERE Status = 'выполнено' AND CompletionDate BETWEEN @startDate AND @endDate";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        int completedTasks = Convert.ToInt32(cmd.ExecuteScalar());

                        // Среднее время выполнения задачи в днях
                        cmd.CommandText = @"
                    SELECT AVG(CompletionDate - CreationDate)
                    FROM Tasks
                    WHERE Status = 'выполнено' AND CompletionDate BETWEEN @startDate AND @endDate";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        object averageCompletionTimeObj = cmd.ExecuteScalar();
                        double? averageCompletionTime = averageCompletionTimeObj != DBNull.Value ? Convert.ToDouble(averageCompletionTimeObj) : (double?)null;

                        // Статистика по проектам
                        cmd.CommandText = @"
                    SELECT ProjectName, COUNT(*)
                    FROM Tasks
                    WHERE Status = 'выполнено' AND CompletionDate BETWEEN @startDate AND @endDate
                    GROUP BY ProjectName";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        var projectStatistics = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            projectStatistics.Load(reader);
                        }

                        // Статистика по исполнителям
                        cmd.CommandText = @"
                    SELECT Assignee, COUNT(*)
                    FROM Tasks
                    WHERE Status = 'выполнено' AND CompletionDate BETWEEN @startDate AND @endDate
                    GROUP BY Assignee";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        var executorStatistics = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            executorStatistics.Load(reader);
                        }

                        // Отображение статистики с новым абзацем
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"Количество выполненных задач: {completedTasks}\n");

                        sb.AppendLine($"Среднее время выполнения задачи: {(averageCompletionTime.HasValue ? averageCompletionTime.Value : 0)} дней\n");

                        sb.AppendLine("Статистика по проектам:");
                        foreach (DataRow row in projectStatistics.Rows)
                        {
                            sb.AppendLine($"{row[0]}: {row[1]} задач");
                        }

                        sb.AppendLine("\nСтатистика по исполнителям:");
                        foreach (DataRow row in executorStatistics.Rows)
                        {
                            sb.AppendLine($"{row[0]}: {row[1]} задач");
                        }

                        txtStatistics.Text = sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void GenerateQRCode()
        {
            try
            {
                // Ссылка на Google Forms
                string googleFormUrl = "https://docs.google.com/forms/d/10IDcmvxln8ZSzO3hYYyWRgrySgEXfX0nHD8AK1TDUo4/edit";

                // Генерация QR-кода
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(googleFormUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    // Сохранение QR-кода в файл
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PNG Files (*.png)|*.png",
                        Title = "Сохранить QR-код как",
                        FileName = "QRCode.png"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        bitmap.Save(filePath, ImageFormat.Png);
                        MessageBox.Show("QR-код успешно сгенерирован и сохранен!");

                        // Открытие QR-кода
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnGenerateQRCode_Click(object sender, EventArgs e)
        {
            GenerateQRCode();
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
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            dtpStartDate.Visible = true;
            dtpEndDate.Visible = true;
            btnCalculateStatistics.Visible = true;
            txtStatistics.Visible = true;
            lblStatistics.Visible = true;
        }

        private void EnableManagerControls()
        {
            btnRefreshTasks.Visible = true;
            btnAddTask.Visible = true;
            btnDeleteTask.Visible = true;
            btnEditTask.Visible = true;
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            dtpStartDate.Visible = true;
            dtpEndDate.Visible = true;
            btnCalculateStatistics.Visible = true;
            txtStatistics.Visible = true;
            lblStatistics.Visible = true;
            btnGenerateQRCode.Visible = false;
        }

        private void EnableEmployeeControls()
        {
            btnRefreshTasks.Visible = true;
            btnAddTask.Visible = true;
            btnDeleteTask.Visible = true;
            btnEditTask.Visible = true;
            btnGenerateQRCode.Visible = true;
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            dtpStartDate.Visible = false;
            dtpEndDate.Visible = false;
            btnCalculateStatistics.Visible = false;
            txtStatistics.Visible = false;
            lblStatistics.Visible = false;
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
    }
}

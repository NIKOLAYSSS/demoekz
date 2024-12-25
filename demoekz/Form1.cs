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
        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=task_management1";
        private int selectedTaskId;
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
                        cmd.CommandText = "SELECT id_задачи, номер_задачи, дата_создания, название_проекта, описание_задачи, приоритет, исполнитель, статус, дата_завершения, создано_пользователем FROM tasks";
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            dgvTasks.DataSource = dt;
                            dgvTasks.Columns["id_задачи"].HeaderText = "ID задачи";
                            dgvTasks.Columns["номер_задачи"].HeaderText = "Номер задачи";
                            dgvTasks.Columns["дата_создания"].HeaderText = "Дата создания";
                            dgvTasks.Columns["название_проекта"].HeaderText = "Название проекта";
                            dgvTasks.Columns["описание_задачи"].HeaderText = "Описание задачи";
                            dgvTasks.Columns["приоритет"].HeaderText = "Приоритет";
                            dgvTasks.Columns["исполнитель"].HeaderText = "Исполнитель";
                            dgvTasks.Columns["статус"].HeaderText = "Статус";
                            dgvTasks.Columns["дата_завершения"].HeaderText = "Дата завершения";
                            dgvTasks.Columns["создано_пользователем"].HeaderText = "Создано пользователем";
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
                int taskId = (int)dgvTasks.SelectedRows[0].Cells["id_задачи"].Value;
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "DELETE FROM tasks WHERE id_задачи = @id_задачи";
                            cmd.Parameters.AddWithValue("id_задачи", taskId);
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
            if (dgvTasks.Columns[e.ColumnIndex].Name == "статус" && e.Value != null)
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
                selectedTaskId = (int)dgvTasks.SelectedRows[0].Cells["id_задачи"].Value;
                txtTaskNumber.Text = dgvTasks.SelectedRows[0].Cells["номер_задачи"].Value.ToString();
                dtpCreationDate.Value = (DateTime)dgvTasks.SelectedRows[0].Cells["дата_создания"].Value;
                txtProjectName.Text = dgvTasks.SelectedRows[0].Cells["название_проекта"].Value.ToString();
                txtTaskDescription.Text = dgvTasks.SelectedRows[0].Cells["описание_задачи"].Value.ToString();
                cmbPriority.SelectedItem = dgvTasks.SelectedRows[0].Cells["приоритет"].Value.ToString();
                txtExecutor.Text = dgvTasks.SelectedRows[0].Cells["исполнитель"].Value.ToString();
                cmbStatus.SelectedItem = dgvTasks.SelectedRows[0].Cells["статус"].Value.ToString();
                dtpDueDate.Value = (DateTime)dgvTasks.SelectedRows[0].Cells["дата_завершения"].Value;

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
                        cmd.CommandText = "UPDATE tasks SET номер_задачи = @номер_задачи, дата_создания = @дата_создания, название_проекта = @название_проекта, описание_задачи = @описание_задачи, приоритет = @приоритет, исполнитель = @исполнитель, статус = @статус, дата_завершения = @дата_завершения WHERE id_задачи = @id_задачи";
                        cmd.Parameters.AddWithValue("id_задачи", selectedTaskId);
                        cmd.Parameters.AddWithValue("номер_задачи", int.Parse(txtTaskNumber.Text));
                        cmd.Parameters.AddWithValue("дата_создания", dtpCreationDate.Value.Date);
                        cmd.Parameters.AddWithValue("название_проекта", txtProjectName.Text);
                        cmd.Parameters.AddWithValue("описание_задачи", txtTaskDescription.Text);
                        cmd.Parameters.AddWithValue("приоритет", cmbPriority.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("исполнитель", txtExecutor.Text);
                        cmd.Parameters.AddWithValue("статус", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("дата_завершения", dtpDueDate.Value.Date);
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
                        cmd.CommandText = "SELECT id_задачи, номер_задачи, дата_создания, название_проекта, описание_задачи, приоритет, исполнитель, статус, дата_завершения, создано_пользователем FROM tasks WHERE CAST(номер_задачи AS TEXT) = @searchTerm OR описание_задачи ILIKE @searchTerm";
                        cmd.Parameters.AddWithValue("searchTerm", searchTerm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            dgvTasks.DataSource = dt;
                            dgvTasks.Columns["id_задачи"].HeaderText = "ID задачи";
                            dgvTasks.Columns["номер_задачи"].HeaderText = "Номер задачи";
                            dgvTasks.Columns["дата_создания"].HeaderText = "Дата создания";
                            dgvTasks.Columns["название_проекта"].HeaderText = "Название проекта";
                            dgvTasks.Columns["описание_задачи"].HeaderText = "Описание задачи";
                            dgvTasks.Columns["приоритет"].HeaderText = "Приоритет";
                            dgvTasks.Columns["исполнитель"].HeaderText = "Исполнитель";
                            dgvTasks.Columns["статус"].HeaderText = "Статус";
                            dgvTasks.Columns["дата_завершения"].HeaderText = "Дата завершения";
                            dgvTasks.Columns["создано_пользователем"].HeaderText = "Создано пользователем";
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
                        cmd.CommandText = "SELECT номер_задачи, описание_задачи FROM tasks WHERE статус != 'выполнено' AND дата_завершения <= NOW()";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string taskNumber = reader["номер_задачи"].ToString();
                                string taskDescription = reader["описание_задачи"].ToString();
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
                        cmd.CommandText = "SELECT COUNT(*) FROM tasks WHERE статус = 'выполнено' AND дата_завершения BETWEEN @startDate AND @endDate";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        int completedTasks = Convert.ToInt32(cmd.ExecuteScalar());

                        // Среднее время выполнения задачи в днях
                        cmd.CommandText = @"
                    SELECT AVG(дата_завершения - дата_создания)
                    FROM tasks
                    WHERE статус = 'выполнено' AND дата_завершения BETWEEN @startDate AND @endDate";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        object averageCompletionTimeObj = cmd.ExecuteScalar();
                        double? averageCompletionTime = averageCompletionTimeObj != DBNull.Value ? Convert.ToDouble(averageCompletionTimeObj) : (double?)null;

                        // Статистика по проектам
                        cmd.CommandText = @"
                    SELECT название_проекта, COUNT(*)
                    FROM tasks
                    WHERE статус = 'выполнено' AND дата_завершения BETWEEN @startDate AND @endDate
                    GROUP BY название_проекта";
                        cmd.Parameters.AddWithValue("startDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("endDate", dtpEndDate.Value.Date);
                        var projectStatistics = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            projectStatistics.Load(reader);
                        }

                        // Статистика по исполнителям
                        cmd.CommandText = @"
                    SELECT исполнитель, COUNT(*)
                    FROM tasks
                    WHERE статус = 'выполнено' AND дата_завершения BETWEEN @startDate AND @endDate
                    GROUP BY исполнитель";
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

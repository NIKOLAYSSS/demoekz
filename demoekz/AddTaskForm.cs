using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demoekz
{
    public partial class AddTaskForm : Form
    {
        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=task_management1";
        private string currentUser;
        private string currentUserRole;
        public AddTaskForm(string username, string role)
        {
            currentUser = username;
            currentUserRole = role;
            InitializeComponent();
            cmbPriority.Items.AddRange(new string[] { "низкий", "средний", "высокий" });
            cmbStatus.Items.AddRange(new string[] { "в ожидании", "в работе", "выполнено" });
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
                        cmd.CommandText = "INSERT INTO tasks (номер_задачи, дата_создания, название_проекта, описание_задачи, приоритет, исполнитель, статус, дата_завершения, создано_пользователем) VALUES (@номер_задачи, @дата_создания, @название_проекта, @описание_задачи, @приоритет, @исполнитель, @статус, @дата_завершения, @создано_пользователем)";
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
                //LoadTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}

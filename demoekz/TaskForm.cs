using System;
using System.Windows.Forms;

namespace demoekz
{
    public partial class TaskForm : Form
    {
        private TaskManager taskManager;
        private Task task;
        private string currentUser;

        public TaskForm(TaskManager taskManager, string currentUser, Task task = null)
        {
            InitializeComponent();
            this.taskManager = taskManager;
            this.task = task;
            this.currentUser = currentUser;

            if (task != null)
            {
                // Заполнить поля формы данными задачи
                txtTaskNumber.Text = task.TaskNumber.ToString();
                dtpCreationDate.Value = task.CreationDate;
                txtProjectName.Text = task.ProjectName;
                txtTaskDescription.Text = task.TaskDescription;
                cmbPriority.SelectedItem = task.Priority;
                txtExecutor.Text = task.Assignee;
                cmbStatus.SelectedItem = task.Status;
                dtpDueDate.Value = task.CompletionDate;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var updatedTask = new Task
                {
                    TaskId = task?.TaskId ?? Guid.NewGuid(),
                    TaskNumber = int.Parse(txtTaskNumber.Text),
                    CreationDate = dtpCreationDate.Value.Date,
                    ProjectName = txtProjectName.Text,
                    TaskDescription = txtTaskDescription.Text,
                    Priority = cmbPriority.SelectedItem?.ToString() ?? string.Empty,
                    Assignee = txtExecutor.Text,
                    Status = cmbStatus.SelectedItem?.ToString() ?? string.Empty,
                    CompletionDate = dtpDueDate.Value.Date,
                    CreatedBy = task?.CreatedBy ?? currentUser // Замените на текущего пользователя
                };

                if (task == null)
                {
                    taskManager.AddTask(updatedTask);
                    MessageBox.Show("Задача успешно добавлена!");
                }
                else
                {
                    taskManager.UpdateTask(updatedTask);
                    MessageBox.Show("Задача успешно обновлена!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}

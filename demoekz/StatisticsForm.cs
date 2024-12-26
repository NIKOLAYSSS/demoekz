using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace demoekz
{
    public partial class StatisticsForm : Form
    {
        private TaskManager taskManager;

        public StatisticsForm(TaskManager taskManager)
        {
            InitializeComponent();
            this.taskManager = taskManager;
        }

        private void btnCalculateStatistics_Click(object sender, EventArgs e)
        {
            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            try
            {
                var (completedTasks, averageCompletionTime, projectStatistics, executorStatistics) = taskManager.CalculateStatistics(dtpStartDate.Value.Date, dtpEndDate.Value.Date);

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
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}

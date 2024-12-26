namespace demoekz
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnRefreshTasks = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnEditTask = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCalculateStatistics = new System.Windows.Forms.Button();
            this.btnGenerateQRCode = new System.Windows.Forms.Button();
            this.btnDocumentation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTasks
            // 
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Location = new System.Drawing.Point(12, 12);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.Size = new System.Drawing.Size(466, 426);
            this.dgvTasks.TabIndex = 16;
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(487, 56);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(154, 34);
            this.btnAddTask.TabIndex = 17;
            this.btnAddTask.Text = "добавить задачу";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnRefreshTasks
            // 
            this.btnRefreshTasks.Location = new System.Drawing.Point(487, 12);
            this.btnRefreshTasks.Name = "btnRefreshTasks";
            this.btnRefreshTasks.Size = new System.Drawing.Size(154, 38);
            this.btnRefreshTasks.TabIndex = 18;
            this.btnRefreshTasks.Text = "Обновить";
            this.btnRefreshTasks.UseVisualStyleBackColor = true;
            this.btnRefreshTasks.Click += new System.EventHandler(this.btnRefreshTasks_Click);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(487, 140);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(154, 34);
            this.btnDeleteTask.TabIndex = 19;
            this.btnDeleteTask.Text = "Удалить";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // btnEditTask
            // 
            this.btnEditTask.Location = new System.Drawing.Point(487, 96);
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(154, 38);
            this.btnEditTask.TabIndex = 20;
            this.btnEditTask.Text = "Изменить";
            this.btnEditTask.UseVisualStyleBackColor = true;
            this.btnEditTask.Click += new System.EventHandler(this.btnEditTask_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSearch.Location = new System.Drawing.Point(9, 451);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(240, 15);
            this.lblSearch.TabIndex = 23;
            this.lblSearch.Text = "Поиск по номеру или ключевым словам:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(255, 451);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(142, 20);
            this.txtSearch.TabIndex = 22;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(403, 449);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCalculateStatistics
            // 
            this.btnCalculateStatistics.Location = new System.Drawing.Point(487, 180);
            this.btnCalculateStatistics.Name = "btnCalculateStatistics";
            this.btnCalculateStatistics.Size = new System.Drawing.Size(154, 34);
            this.btnCalculateStatistics.TabIndex = 37;
            this.btnCalculateStatistics.Text = "Показать статистику";
            this.btnCalculateStatistics.UseVisualStyleBackColor = true;
            this.btnCalculateStatistics.Click += new System.EventHandler(this.btnCalculateStatistics_Click);
            // 
            // btnGenerateQRCode
            // 
            this.btnGenerateQRCode.Location = new System.Drawing.Point(487, 220);
            this.btnGenerateQRCode.Name = "btnGenerateQRCode";
            this.btnGenerateQRCode.Size = new System.Drawing.Size(154, 41);
            this.btnGenerateQRCode.TabIndex = 54;
            this.btnGenerateQRCode.Text = "Генирировать qr для отзыва";
            this.btnGenerateQRCode.UseVisualStyleBackColor = true;
            this.btnGenerateQRCode.Click += new System.EventHandler(this.BtnGenerateQRCode_Click);
            // 
            // btnDocumentation
            // 
            this.btnDocumentation.Location = new System.Drawing.Point(487, 267);
            this.btnDocumentation.Name = "btnDocumentation";
            this.btnDocumentation.Size = new System.Drawing.Size(154, 37);
            this.btnDocumentation.TabIndex = 55;
            this.btnDocumentation.Text = "Документация";
            this.btnDocumentation.UseVisualStyleBackColor = true;
            this.btnDocumentation.Click += new System.EventHandler(this.btnDocumentation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 484);
            this.Controls.Add(this.btnDocumentation);
            this.Controls.Add(this.btnGenerateQRCode);
            this.Controls.Add(this.btnCalculateStatistics);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnEditTask);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.btnRefreshTasks);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.dgvTasks);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnRefreshTasks;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnEditTask;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCalculateStatistics;
        private System.Windows.Forms.Button btnGenerateQRCode;
        public System.Windows.Forms.Timer notificationTimer;
        private System.Windows.Forms.Button btnDocumentation;
    }
}


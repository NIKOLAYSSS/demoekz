namespace demoekz
{
    partial class TaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTaskNumber = new System.Windows.Forms.Label();
            this.txtTaskNumber = new System.Windows.Forms.TextBox();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblExecutor = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblTaskDescription = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblCreationDate = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtExecutor = new System.Windows.Forms.TextBox();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.dtpCreationDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTaskNumber
            // 
            this.lblTaskNumber.AutoSize = true;
            this.lblTaskNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTaskNumber.Location = new System.Drawing.Point(12, 9);
            this.lblTaskNumber.Name = "lblTaskNumber";
            this.lblTaskNumber.Size = new System.Drawing.Size(92, 15);
            this.lblTaskNumber.TabIndex = 69;
            this.lblTaskNumber.Text = "Номер задачи:";
            // 
            // txtTaskNumber
            // 
            this.txtTaskNumber.Location = new System.Drawing.Point(150, 6);
            this.txtTaskNumber.Name = "txtTaskNumber";
            this.txtTaskNumber.Size = new System.Drawing.Size(166, 20);
            this.txtTaskNumber.TabIndex = 68;
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDueDate.Location = new System.Drawing.Point(12, 170);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(157, 15);
            this.lblDueDate.TabIndex = 67;
            this.lblDueDate.Text = "Дата завершения задачи:";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new System.Drawing.Point(175, 165);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(141, 20);
            this.dtpDueDate.TabIndex = 66;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(12, 141);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(125, 15);
            this.lblStatus.TabIndex = 65;
            this.lblStatus.Text = "Статус выполнения:";
            // 
            // lblExecutor
            // 
            this.lblExecutor.AutoSize = true;
            this.lblExecutor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblExecutor.Location = new System.Drawing.Point(12, 115);
            this.lblExecutor.Name = "lblExecutor";
            this.lblExecutor.Size = new System.Drawing.Size(131, 15);
            this.lblExecutor.TabIndex = 64;
            this.lblExecutor.Text = "Исполнитель задачи:";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPriority.Location = new System.Drawing.Point(12, 88);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(118, 15);
            this.lblPriority.TabIndex = 63;
            this.lblPriority.Text = "Приоритет задачи:";
            // 
            // lblTaskDescription
            // 
            this.lblTaskDescription.AutoSize = true;
            this.lblTaskDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTaskDescription.Location = new System.Drawing.Point(12, 62);
            this.lblTaskDescription.Name = "lblTaskDescription";
            this.lblTaskDescription.Size = new System.Drawing.Size(110, 15);
            this.lblTaskDescription.TabIndex = 62;
            this.lblTaskDescription.Text = "Описание задачи:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProjectName.Location = new System.Drawing.Point(12, 36);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(118, 15);
            this.lblProjectName.TabIndex = 61;
            this.lblProjectName.Text = "Название проекта:";
            // 
            // lblCreationDate
            // 
            this.lblCreationDate.AutoSize = true;
            this.lblCreationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCreationDate.Location = new System.Drawing.Point(12, 198);
            this.lblCreationDate.Name = "lblCreationDate";
            this.lblCreationDate.Size = new System.Drawing.Size(97, 15);
            this.lblCreationDate.TabIndex = 60;
            this.lblCreationDate.Text = "Дата создания:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "выполнено",
            "в ожидании",
            "в работе"});
            this.cmbStatus.Location = new System.Drawing.Point(150, 138);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(166, 21);
            this.cmbStatus.TabIndex = 59;
            // 
            // txtExecutor
            // 
            this.txtExecutor.Location = new System.Drawing.Point(150, 112);
            this.txtExecutor.Name = "txtExecutor";
            this.txtExecutor.Size = new System.Drawing.Size(166, 20);
            this.txtExecutor.TabIndex = 58;
            // 
            // cmbPriority
            // 
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Items.AddRange(new object[] {
            "Низкий",
            "Средний",
            "Высокий"});
            this.cmbPriority.Location = new System.Drawing.Point(150, 85);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(166, 21);
            this.cmbPriority.TabIndex = 57;
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(150, 59);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(166, 20);
            this.txtTaskDescription.TabIndex = 56;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(150, 33);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(166, 20);
            this.txtProjectName.TabIndex = 55;
            // 
            // dtpCreationDate
            // 
            this.dtpCreationDate.Location = new System.Drawing.Point(150, 195);
            this.dtpCreationDate.Name = "dtpCreationDate";
            this.dtpCreationDate.Size = new System.Drawing.Size(166, 20);
            this.dtpCreationDate.TabIndex = 54;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 225);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(304, 23);
            this.btnSave.TabIndex = 70;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 264);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTaskNumber);
            this.Controls.Add(this.txtTaskNumber);
            this.Controls.Add(this.lblDueDate);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblExecutor);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.lblTaskDescription);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblCreationDate);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtExecutor);
            this.Controls.Add(this.cmbPriority);
            this.Controls.Add(this.txtTaskDescription);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.dtpCreationDate);
            this.Name = "TaskForm";
            this.Text = "TaskForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTaskNumber;
        private System.Windows.Forms.TextBox txtTaskNumber;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblExecutor;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblTaskDescription;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblCreationDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtExecutor;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.DateTimePicker dtpCreationDate;
        private System.Windows.Forms.Button btnSave;
    }
}
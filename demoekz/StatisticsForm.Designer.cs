namespace demoekz
{
    partial class StatisticsForm
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
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtStatistics = new System.Windows.Forms.TextBox();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.btnCalculateStatistics = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEndDate.Location = new System.Drawing.Point(12, 41);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(149, 15);
            this.lblEndDate.TabIndex = 38;
            this.lblEndDate.Text = "Конечная дата периода:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStartDate.Location = new System.Drawing.Point(12, 9);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(157, 15);
            this.lblStartDate.TabIndex = 37;
            this.lblStartDate.Text = "Начальная дата периода:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(169, 41);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(147, 20);
            this.dtpEndDate.TabIndex = 36;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(169, 9);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(147, 20);
            this.dtpStartDate.TabIndex = 35;
            // 
            // txtStatistics
            // 
            this.txtStatistics.Location = new System.Drawing.Point(12, 89);
            this.txtStatistics.Multiline = true;
            this.txtStatistics.Name = "txtStatistics";
            this.txtStatistics.ReadOnly = true;
            this.txtStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatistics.Size = new System.Drawing.Size(304, 100);
            this.txtStatistics.TabIndex = 40;
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatistics.Location = new System.Drawing.Point(9, 71);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(79, 15);
            this.lblStatistics.TabIndex = 39;
            this.lblStatistics.Text = "Статистика:";
            // 
            // btnCalculateStatistics
            // 
            this.btnCalculateStatistics.Location = new System.Drawing.Point(12, 195);
            this.btnCalculateStatistics.Name = "btnCalculateStatistics";
            this.btnCalculateStatistics.Size = new System.Drawing.Size(304, 31);
            this.btnCalculateStatistics.TabIndex = 41;
            this.btnCalculateStatistics.Text = "показать";
            this.btnCalculateStatistics.UseVisualStyleBackColor = true;
            this.btnCalculateStatistics.Click += new System.EventHandler(this.btnCalculateStatistics_Click);
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 235);
            this.Controls.Add(this.btnCalculateStatistics);
            this.Controls.Add(this.txtStatistics);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtStatistics;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Button btnCalculateStatistics;
    }
}
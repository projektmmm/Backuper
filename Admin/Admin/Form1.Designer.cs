namespace Admin
{
    partial class Form1
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
            this.dateTimePicker_RunAt = new System.Windows.Forms.DateTimePicker();
            this.comboBox_BackupType = new System.Windows.Forms.ComboBox();
            this.textBox_DestinationPath = new System.Windows.Forms.TextBox();
            this.textBox_SourcePath = new System.Windows.Forms.TextBox();
            this.comboBox_DaemonId = new System.Windows.Forms.ComboBox();
            this.button_Odeslat = new System.Windows.Forms.Button();
            this.button_ProvestHned = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker_RunAt
            // 
            this.dateTimePicker_RunAt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker_RunAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_RunAt.Location = new System.Drawing.Point(143, 60);
            this.dateTimePicker_RunAt.Name = "dateTimePicker_RunAt";
            this.dateTimePicker_RunAt.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_RunAt.TabIndex = 1;
            // 
            // comboBox_BackupType
            // 
            this.comboBox_BackupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BackupType.FormattingEnabled = true;
            this.comboBox_BackupType.Location = new System.Drawing.Point(143, 86);
            this.comboBox_BackupType.Name = "comboBox_BackupType";
            this.comboBox_BackupType.Size = new System.Drawing.Size(200, 21);
            this.comboBox_BackupType.TabIndex = 2;
            // 
            // textBox_DestinationPath
            // 
            this.textBox_DestinationPath.Location = new System.Drawing.Point(143, 139);
            this.textBox_DestinationPath.Name = "textBox_DestinationPath";
            this.textBox_DestinationPath.Size = new System.Drawing.Size(200, 20);
            this.textBox_DestinationPath.TabIndex = 4;
            // 
            // textBox_SourcePath
            // 
            this.textBox_SourcePath.Location = new System.Drawing.Point(143, 113);
            this.textBox_SourcePath.Name = "textBox_SourcePath";
            this.textBox_SourcePath.Size = new System.Drawing.Size(200, 20);
            this.textBox_SourcePath.TabIndex = 3;
            // 
            // comboBox_DaemonId
            // 
            this.comboBox_DaemonId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DaemonId.FormattingEnabled = true;
            this.comboBox_DaemonId.IntegralHeight = false;
            this.comboBox_DaemonId.Location = new System.Drawing.Point(143, 33);
            this.comboBox_DaemonId.Name = "comboBox_DaemonId";
            this.comboBox_DaemonId.Size = new System.Drawing.Size(200, 21);
            this.comboBox_DaemonId.TabIndex = 0;
            // 
            // button_Odeslat
            // 
            this.button_Odeslat.Location = new System.Drawing.Point(244, 221);
            this.button_Odeslat.Name = "button_Odeslat";
            this.button_Odeslat.Size = new System.Drawing.Size(75, 23);
            this.button_Odeslat.TabIndex = 5;
            this.button_Odeslat.Text = "Odeslat";
            this.button_Odeslat.UseVisualStyleBackColor = true;
            this.button_Odeslat.Click += new System.EventHandler(this.button_Odeslat_Click);
            // 
            // button_ProvestHned
            // 
            this.button_ProvestHned.Location = new System.Drawing.Point(325, 221);
            this.button_ProvestHned.Name = "button_ProvestHned";
            this.button_ProvestHned.Size = new System.Drawing.Size(90, 23);
            this.button_ProvestHned.TabIndex = 6;
            this.button_ProvestHned.Text = "Provést hned";
            this.button_ProvestHned.UseVisualStyleBackColor = true;
            this.button_ProvestHned.Click += new System.EventHandler(this.button_ProvestHned_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "RunAt:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "BackupType:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "SourcePath:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "DestinationPath:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "DaemonId:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 256);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_ProvestHned);
            this.Controls.Add(this.button_Odeslat);
            this.Controls.Add(this.comboBox_DaemonId);
            this.Controls.Add(this.textBox_SourcePath);
            this.Controls.Add(this.textBox_DestinationPath);
            this.Controls.Add(this.comboBox_BackupType);
            this.Controls.Add(this.dateTimePicker_RunAt);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_RunAt;
        private System.Windows.Forms.ComboBox comboBox_BackupType;
        private System.Windows.Forms.TextBox textBox_DestinationPath;
        private System.Windows.Forms.TextBox textBox_SourcePath;
        private System.Windows.Forms.ComboBox comboBox_DaemonId;
        private System.Windows.Forms.Button button_Odeslat;
        private System.Windows.Forms.Button button_ProvestHned;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}


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
            this.comboBox_BackupType = new System.Windows.Forms.ComboBox();
            this.textBox_DestinationPath = new System.Windows.Forms.TextBox();
            this.textBox_SourcePath = new System.Windows.Forms.TextBox();
            this.comboBox_DaemonId = new System.Windows.Forms.ComboBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_OpenRepeatPicker = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_BackupType
            // 
            this.comboBox_BackupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BackupType.FormattingEnabled = true;
            this.comboBox_BackupType.Location = new System.Drawing.Point(143, 60);
            this.comboBox_BackupType.Name = "comboBox_BackupType";
            this.comboBox_BackupType.Size = new System.Drawing.Size(200, 21);
            this.comboBox_BackupType.TabIndex = 2;
            this.comboBox_BackupType.SelectedIndexChanged += new System.EventHandler(this.comboBox_BackupType_SelectedIndexChanged);
            // 
            // textBox_DestinationPath
            // 
            this.textBox_DestinationPath.Location = new System.Drawing.Point(143, 113);
            this.textBox_DestinationPath.Name = "textBox_DestinationPath";
            this.textBox_DestinationPath.Size = new System.Drawing.Size(200, 20);
            this.textBox_DestinationPath.TabIndex = 4;
            // 
            // textBox_SourcePath
            // 
            this.textBox_SourcePath.Location = new System.Drawing.Point(143, 87);
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
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(340, 221);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 5;
            this.button_Send.Text = "Odeslat";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "BackupType:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "SourcePath:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 116);
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
            // button_OpenRepeatPicker
            // 
            this.button_OpenRepeatPicker.Location = new System.Drawing.Point(143, 139);
            this.button_OpenRepeatPicker.Name = "button_OpenRepeatPicker";
            this.button_OpenRepeatPicker.Size = new System.Drawing.Size(200, 23);
            this.button_OpenRepeatPicker.TabIndex = 15;
            this.button_OpenRepeatPicker.Text = "OpenRepeatSettings\r\n";
            this.button_OpenRepeatPicker.UseVisualStyleBackColor = true;
            this.button_OpenRepeatPicker.Click += new System.EventHandler(this.button_OpenRepeatPicker_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "RepeatSettings:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 256);
            this.Controls.Add(this.button_OpenRepeatPicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.comboBox_DaemonId);
            this.Controls.Add(this.textBox_SourcePath);
            this.Controls.Add(this.textBox_DestinationPath);
            this.Controls.Add(this.comboBox_BackupType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_BackupType;
        private System.Windows.Forms.TextBox textBox_DestinationPath;
        private System.Windows.Forms.TextBox textBox_SourcePath;
        private System.Windows.Forms.ComboBox comboBox_DaemonId;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_OpenRepeatPicker;
        private System.Windows.Forms.Label label6;
    }
}


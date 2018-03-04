namespace Admin
{
    partial class RepeatPicker
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMinutes = new System.Windows.Forms.TabPage();
            this.buttonSaveMinutes = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageHourly = new System.Windows.Forms.TabPage();
            this.buttonSaveHourly = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartsMinutesHourly = new System.Windows.Forms.ComboBox();
            this.comboBoxStartsHourHourly = new System.Windows.Forms.ComboBox();
            this.radioButtonEveryHours = new System.Windows.Forms.RadioButton();
            this.radioButtonStartsAtHours = new System.Windows.Forms.RadioButton();
            this.tabPageDaily = new System.Windows.Forms.TabPage();
            this.buttonSaveDaily = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxStartMinutesDaily = new System.Windows.Forms.ComboBox();
            this.comboBoxStartHourDaily = new System.Windows.Forms.ComboBox();
            this.radioButtonEveryDays = new System.Windows.Forms.RadioButton();
            this.radioButtonEveryWeekDay = new System.Windows.Forms.RadioButton();
            this.tabPageWeekly = new System.Windows.Forms.TabPage();
            this.buttonSaveWeekly = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxStartMinutesWeekly = new System.Windows.Forms.ComboBox();
            this.comboBoxStartHourWeekly = new System.Windows.Forms.ComboBox();
            this.checkBox_Sunday = new System.Windows.Forms.CheckBox();
            this.checkBox_Saturday = new System.Windows.Forms.CheckBox();
            this.checkBox_Friday = new System.Windows.Forms.CheckBox();
            this.checkBox_Thursday = new System.Windows.Forms.CheckBox();
            this.checkBox_Wednesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Tuesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Monday = new System.Windows.Forms.CheckBox();
            this.tabPageMonthly = new System.Windows.Forms.TabPage();
            this.buttonSaveMonthly = new System.Windows.Forms.Button();
            this.tabPageYearly = new System.Windows.Forms.TabPage();
            this.buttonSaveYearly = new System.Windows.Forms.Button();
            this.comboBoxEveryMinutes = new System.Windows.Forms.ComboBox();
            this.comboBoxEveryHours = new System.Windows.Forms.ComboBox();
            this.comboBoxEveryDays = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPageMinutes.SuspendLayout();
            this.tabPageHourly.SuspendLayout();
            this.tabPageDaily.SuspendLayout();
            this.tabPageWeekly.SuspendLayout();
            this.tabPageMonthly.SuspendLayout();
            this.tabPageYearly.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageMinutes);
            this.tabControl1.Controls.Add(this.tabPageHourly);
            this.tabControl1.Controls.Add(this.tabPageDaily);
            this.tabControl1.Controls.Add(this.tabPageWeekly);
            this.tabControl1.Controls.Add(this.tabPageMonthly);
            this.tabControl1.Controls.Add(this.tabPageYearly);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.ItemSize = new System.Drawing.Size(49, 18);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(517, 209);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageMinutes
            // 
            this.tabPageMinutes.Controls.Add(this.comboBoxEveryMinutes);
            this.tabPageMinutes.Controls.Add(this.buttonSaveMinutes);
            this.tabPageMinutes.Controls.Add(this.label5);
            this.tabPageMinutes.Controls.Add(this.label1);
            this.tabPageMinutes.Location = new System.Drawing.Point(4, 22);
            this.tabPageMinutes.Name = "tabPageMinutes";
            this.tabPageMinutes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMinutes.Size = new System.Drawing.Size(509, 183);
            this.tabPageMinutes.TabIndex = 0;
            this.tabPageMinutes.Text = "Minutes";
            this.tabPageMinutes.UseVisualStyleBackColor = true;
            // 
            // buttonSaveMinutes
            // 
            this.buttonSaveMinutes.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveMinutes.Name = "buttonSaveMinutes";
            this.buttonSaveMinutes.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveMinutes.TabIndex = 3;
            this.buttonSaveMinutes.Text = "Save";
            this.buttonSaveMinutes.UseVisualStyleBackColor = true;
            this.buttonSaveMinutes.Click += new System.EventHandler(this.buttonSaveMinutes_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "minute(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Every";
            // 
            // tabPageHourly
            // 
            this.tabPageHourly.Controls.Add(this.comboBoxEveryHours);
            this.tabPageHourly.Controls.Add(this.buttonSaveHourly);
            this.tabPageHourly.Controls.Add(this.label3);
            this.tabPageHourly.Controls.Add(this.comboBoxStartsMinutesHourly);
            this.tabPageHourly.Controls.Add(this.comboBoxStartsHourHourly);
            this.tabPageHourly.Controls.Add(this.radioButtonEveryHours);
            this.tabPageHourly.Controls.Add(this.radioButtonStartsAtHours);
            this.tabPageHourly.Location = new System.Drawing.Point(4, 22);
            this.tabPageHourly.Name = "tabPageHourly";
            this.tabPageHourly.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHourly.Size = new System.Drawing.Size(509, 183);
            this.tabPageHourly.TabIndex = 1;
            this.tabPageHourly.Text = "Hourly";
            this.tabPageHourly.UseVisualStyleBackColor = true;
            // 
            // buttonSaveHourly
            // 
            this.buttonSaveHourly.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveHourly.Name = "buttonSaveHourly";
            this.buttonSaveHourly.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveHourly.TabIndex = 11;
            this.buttonSaveHourly.Text = "Save";
            this.buttonSaveHourly.UseVisualStyleBackColor = true;
            this.buttonSaveHourly.Click += new System.EventHandler(this.buttonSaveHourly_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "hour(s)";
            // 
            // comboBoxStartsMinutesHourly
            // 
            this.comboBoxStartsMinutesHourly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartsMinutesHourly.FormattingEnabled = true;
            this.comboBoxStartsMinutesHourly.Location = new System.Drawing.Point(138, 46);
            this.comboBoxStartsMinutesHourly.Name = "comboBoxStartsMinutesHourly";
            this.comboBoxStartsMinutesHourly.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartsMinutesHourly.TabIndex = 9;
            // 
            // comboBoxStartsHourHourly
            // 
            this.comboBoxStartsHourHourly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartsHourHourly.FormattingEnabled = true;
            this.comboBoxStartsHourHourly.Location = new System.Drawing.Point(90, 46);
            this.comboBoxStartsHourHourly.Name = "comboBoxStartsHourHourly";
            this.comboBoxStartsHourHourly.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartsHourHourly.TabIndex = 8;
            // 
            // radioButtonEveryHours
            // 
            this.radioButtonEveryHours.AutoSize = true;
            this.radioButtonEveryHours.Checked = true;
            this.radioButtonEveryHours.Location = new System.Drawing.Point(20, 20);
            this.radioButtonEveryHours.Name = "radioButtonEveryHours";
            this.radioButtonEveryHours.Size = new System.Drawing.Size(52, 17);
            this.radioButtonEveryHours.TabIndex = 7;
            this.radioButtonEveryHours.TabStop = true;
            this.radioButtonEveryHours.Text = "Every";
            this.radioButtonEveryHours.UseVisualStyleBackColor = true;
            // 
            // radioButtonStartsAtHours
            // 
            this.radioButtonStartsAtHours.AutoSize = true;
            this.radioButtonStartsAtHours.Location = new System.Drawing.Point(20, 47);
            this.radioButtonStartsAtHours.Name = "radioButtonStartsAtHours";
            this.radioButtonStartsAtHours.Size = new System.Drawing.Size(64, 17);
            this.radioButtonStartsAtHours.TabIndex = 6;
            this.radioButtonStartsAtHours.Text = "Starts at";
            this.radioButtonStartsAtHours.UseVisualStyleBackColor = true;
            // 
            // tabPageDaily
            // 
            this.tabPageDaily.Controls.Add(this.comboBoxEveryDays);
            this.tabPageDaily.Controls.Add(this.buttonSaveDaily);
            this.tabPageDaily.Controls.Add(this.label6);
            this.tabPageDaily.Controls.Add(this.label4);
            this.tabPageDaily.Controls.Add(this.comboBoxStartMinutesDaily);
            this.tabPageDaily.Controls.Add(this.comboBoxStartHourDaily);
            this.tabPageDaily.Controls.Add(this.radioButtonEveryDays);
            this.tabPageDaily.Controls.Add(this.radioButtonEveryWeekDay);
            this.tabPageDaily.Location = new System.Drawing.Point(4, 22);
            this.tabPageDaily.Name = "tabPageDaily";
            this.tabPageDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDaily.Size = new System.Drawing.Size(509, 183);
            this.tabPageDaily.TabIndex = 2;
            this.tabPageDaily.Text = "Daily";
            this.tabPageDaily.UseVisualStyleBackColor = true;
            // 
            // buttonSaveDaily
            // 
            this.buttonSaveDaily.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveDaily.Name = "buttonSaveDaily";
            this.buttonSaveDaily.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveDaily.TabIndex = 18;
            this.buttonSaveDaily.Text = "Save";
            this.buttonSaveDaily.UseVisualStyleBackColor = true;
            this.buttonSaveDaily.Click += new System.EventHandler(this.buttonSaveDaily_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "day(s)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Start time";
            // 
            // comboBoxStartMinutesDaily
            // 
            this.comboBoxStartMinutesDaily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartMinutesDaily.FormattingEnabled = true;
            this.comboBoxStartMinutesDaily.Location = new System.Drawing.Point(122, 66);
            this.comboBoxStartMinutesDaily.Name = "comboBoxStartMinutesDaily";
            this.comboBoxStartMinutesDaily.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartMinutesDaily.TabIndex = 15;
            // 
            // comboBoxStartHourDaily
            // 
            this.comboBoxStartHourDaily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartHourDaily.FormattingEnabled = true;
            this.comboBoxStartHourDaily.Location = new System.Drawing.Point(74, 66);
            this.comboBoxStartHourDaily.Name = "comboBoxStartHourDaily";
            this.comboBoxStartHourDaily.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartHourDaily.TabIndex = 14;
            // 
            // radioButtonEveryDays
            // 
            this.radioButtonEveryDays.AutoSize = true;
            this.radioButtonEveryDays.Checked = true;
            this.radioButtonEveryDays.Location = new System.Drawing.Point(20, 20);
            this.radioButtonEveryDays.Name = "radioButtonEveryDays";
            this.radioButtonEveryDays.Size = new System.Drawing.Size(52, 17);
            this.radioButtonEveryDays.TabIndex = 13;
            this.radioButtonEveryDays.TabStop = true;
            this.radioButtonEveryDays.Text = "Every";
            this.radioButtonEveryDays.UseVisualStyleBackColor = true;
            // 
            // radioButtonEveryWeekDay
            // 
            this.radioButtonEveryWeekDay.AutoSize = true;
            this.radioButtonEveryWeekDay.Location = new System.Drawing.Point(20, 43);
            this.radioButtonEveryWeekDay.Name = "radioButtonEveryWeekDay";
            this.radioButtonEveryWeekDay.Size = new System.Drawing.Size(101, 17);
            this.radioButtonEveryWeekDay.TabIndex = 12;
            this.radioButtonEveryWeekDay.Text = "Every week day";
            this.radioButtonEveryWeekDay.UseVisualStyleBackColor = true;
            // 
            // tabPageWeekly
            // 
            this.tabPageWeekly.Controls.Add(this.buttonSaveWeekly);
            this.tabPageWeekly.Controls.Add(this.label2);
            this.tabPageWeekly.Controls.Add(this.comboBoxStartMinutesWeekly);
            this.tabPageWeekly.Controls.Add(this.comboBoxStartHourWeekly);
            this.tabPageWeekly.Controls.Add(this.checkBox_Sunday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Saturday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Friday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Thursday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Wednesday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Tuesday);
            this.tabPageWeekly.Controls.Add(this.checkBox_Monday);
            this.tabPageWeekly.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeekly.Name = "tabPageWeekly";
            this.tabPageWeekly.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeekly.Size = new System.Drawing.Size(509, 183);
            this.tabPageWeekly.TabIndex = 3;
            this.tabPageWeekly.Text = "Weekly";
            this.tabPageWeekly.UseVisualStyleBackColor = true;
            // 
            // buttonSaveWeekly
            // 
            this.buttonSaveWeekly.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveWeekly.Name = "buttonSaveWeekly";
            this.buttonSaveWeekly.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveWeekly.TabIndex = 20;
            this.buttonSaveWeekly.Text = "Save";
            this.buttonSaveWeekly.UseVisualStyleBackColor = true;
            this.buttonSaveWeekly.Click += new System.EventHandler(this.buttonSaveWeekly_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Start time";
            // 
            // comboBoxStartMinutesWeekly
            // 
            this.comboBoxStartMinutesWeekly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartMinutesWeekly.FormattingEnabled = true;
            this.comboBoxStartMinutesWeekly.Location = new System.Drawing.Point(122, 64);
            this.comboBoxStartMinutesWeekly.Name = "comboBoxStartMinutesWeekly";
            this.comboBoxStartMinutesWeekly.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartMinutesWeekly.TabIndex = 18;
            // 
            // comboBoxStartHourWeekly
            // 
            this.comboBoxStartHourWeekly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartHourWeekly.FormattingEnabled = true;
            this.comboBoxStartHourWeekly.Location = new System.Drawing.Point(74, 64);
            this.comboBoxStartHourWeekly.Name = "comboBoxStartHourWeekly";
            this.comboBoxStartHourWeekly.Size = new System.Drawing.Size(42, 21);
            this.comboBoxStartHourWeekly.TabIndex = 17;
            // 
            // checkBox_Sunday
            // 
            this.checkBox_Sunday.AutoSize = true;
            this.checkBox_Sunday.Location = new System.Drawing.Point(163, 43);
            this.checkBox_Sunday.Name = "checkBox_Sunday";
            this.checkBox_Sunday.Size = new System.Drawing.Size(62, 17);
            this.checkBox_Sunday.TabIndex = 6;
            this.checkBox_Sunday.Text = "Sunday";
            this.checkBox_Sunday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Saturday
            // 
            this.checkBox_Saturday.AutoSize = true;
            this.checkBox_Saturday.Location = new System.Drawing.Point(90, 43);
            this.checkBox_Saturday.Name = "checkBox_Saturday";
            this.checkBox_Saturday.Size = new System.Drawing.Size(68, 17);
            this.checkBox_Saturday.TabIndex = 5;
            this.checkBox_Saturday.Text = "Saturday";
            this.checkBox_Saturday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Friday
            // 
            this.checkBox_Friday.AutoSize = true;
            this.checkBox_Friday.Location = new System.Drawing.Point(20, 43);
            this.checkBox_Friday.Name = "checkBox_Friday";
            this.checkBox_Friday.Size = new System.Drawing.Size(54, 17);
            this.checkBox_Friday.TabIndex = 4;
            this.checkBox_Friday.Text = "Friday";
            this.checkBox_Friday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Thursday
            // 
            this.checkBox_Thursday.AutoSize = true;
            this.checkBox_Thursday.Location = new System.Drawing.Point(252, 20);
            this.checkBox_Thursday.Name = "checkBox_Thursday";
            this.checkBox_Thursday.Size = new System.Drawing.Size(70, 17);
            this.checkBox_Thursday.TabIndex = 3;
            this.checkBox_Thursday.Text = "Thursday";
            this.checkBox_Thursday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Wednesday
            // 
            this.checkBox_Wednesday.AutoSize = true;
            this.checkBox_Wednesday.Location = new System.Drawing.Point(163, 20);
            this.checkBox_Wednesday.Name = "checkBox_Wednesday";
            this.checkBox_Wednesday.Size = new System.Drawing.Size(83, 17);
            this.checkBox_Wednesday.TabIndex = 2;
            this.checkBox_Wednesday.Text = "Wednesday";
            this.checkBox_Wednesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Tuesday
            // 
            this.checkBox_Tuesday.AutoSize = true;
            this.checkBox_Tuesday.Location = new System.Drawing.Point(90, 20);
            this.checkBox_Tuesday.Name = "checkBox_Tuesday";
            this.checkBox_Tuesday.Size = new System.Drawing.Size(67, 17);
            this.checkBox_Tuesday.TabIndex = 1;
            this.checkBox_Tuesday.Text = "Tuesday";
            this.checkBox_Tuesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Monday
            // 
            this.checkBox_Monday.AutoSize = true;
            this.checkBox_Monday.Location = new System.Drawing.Point(20, 20);
            this.checkBox_Monday.Name = "checkBox_Monday";
            this.checkBox_Monday.Size = new System.Drawing.Size(64, 17);
            this.checkBox_Monday.TabIndex = 0;
            this.checkBox_Monday.Text = "Monday";
            this.checkBox_Monday.UseVisualStyleBackColor = true;
            // 
            // tabPageMonthly
            // 
            this.tabPageMonthly.Controls.Add(this.buttonSaveMonthly);
            this.tabPageMonthly.Location = new System.Drawing.Point(4, 22);
            this.tabPageMonthly.Name = "tabPageMonthly";
            this.tabPageMonthly.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMonthly.Size = new System.Drawing.Size(509, 183);
            this.tabPageMonthly.TabIndex = 4;
            this.tabPageMonthly.Text = "Monthly";
            this.tabPageMonthly.UseVisualStyleBackColor = true;
            // 
            // buttonSaveMonthly
            // 
            this.buttonSaveMonthly.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveMonthly.Name = "buttonSaveMonthly";
            this.buttonSaveMonthly.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveMonthly.TabIndex = 12;
            this.buttonSaveMonthly.Text = "Save";
            this.buttonSaveMonthly.UseVisualStyleBackColor = true;
            this.buttonSaveMonthly.Click += new System.EventHandler(this.buttonSaveMonthly_Click);
            // 
            // tabPageYearly
            // 
            this.tabPageYearly.Controls.Add(this.buttonSaveYearly);
            this.tabPageYearly.Location = new System.Drawing.Point(4, 22);
            this.tabPageYearly.Name = "tabPageYearly";
            this.tabPageYearly.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageYearly.Size = new System.Drawing.Size(509, 183);
            this.tabPageYearly.TabIndex = 5;
            this.tabPageYearly.Text = "Yearly";
            this.tabPageYearly.UseVisualStyleBackColor = true;
            // 
            // buttonSaveYearly
            // 
            this.buttonSaveYearly.Location = new System.Drawing.Point(428, 154);
            this.buttonSaveYearly.Name = "buttonSaveYearly";
            this.buttonSaveYearly.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveYearly.TabIndex = 12;
            this.buttonSaveYearly.Text = "Save";
            this.buttonSaveYearly.UseVisualStyleBackColor = true;
            this.buttonSaveYearly.Click += new System.EventHandler(this.buttonSaveYearly_Click);
            // 
            // comboBoxEveryMinutes
            // 
            this.comboBoxEveryMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEveryMinutes.FormattingEnabled = true;
            this.comboBoxEveryMinutes.Location = new System.Drawing.Point(60, 17);
            this.comboBoxEveryMinutes.Name = "comboBoxEveryMinutes";
            this.comboBoxEveryMinutes.Size = new System.Drawing.Size(49, 21);
            this.comboBoxEveryMinutes.TabIndex = 4;
            // 
            // comboBoxEveryHours
            // 
            this.comboBoxEveryHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEveryHours.FormattingEnabled = true;
            this.comboBoxEveryHours.Location = new System.Drawing.Point(78, 19);
            this.comboBoxEveryHours.Name = "comboBoxEveryHours";
            this.comboBoxEveryHours.Size = new System.Drawing.Size(49, 21);
            this.comboBoxEveryHours.TabIndex = 12;
            // 
            // comboBoxEveryDays
            // 
            this.comboBoxEveryDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEveryDays.FormattingEnabled = true;
            this.comboBoxEveryDays.Location = new System.Drawing.Point(78, 19);
            this.comboBoxEveryDays.Name = "comboBoxEveryDays";
            this.comboBoxEveryDays.Size = new System.Drawing.Size(49, 21);
            this.comboBoxEveryDays.TabIndex = 19;
            // 
            // RepeatPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 242);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RepeatPicker";
            this.Text = "RepeatPicker";
            this.tabControl1.ResumeLayout(false);
            this.tabPageMinutes.ResumeLayout(false);
            this.tabPageMinutes.PerformLayout();
            this.tabPageHourly.ResumeLayout(false);
            this.tabPageHourly.PerformLayout();
            this.tabPageDaily.ResumeLayout(false);
            this.tabPageDaily.PerformLayout();
            this.tabPageWeekly.ResumeLayout(false);
            this.tabPageWeekly.PerformLayout();
            this.tabPageMonthly.ResumeLayout(false);
            this.tabPageYearly.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageHourly;
        private System.Windows.Forms.ComboBox comboBoxStartsMinutesHourly;
        private System.Windows.Forms.ComboBox comboBoxStartsHourHourly;
        private System.Windows.Forms.RadioButton radioButtonEveryHours;
        private System.Windows.Forms.RadioButton radioButtonStartsAtHours;
        private System.Windows.Forms.TabPage tabPageDaily;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxStartMinutesDaily;
        private System.Windows.Forms.ComboBox comboBoxStartHourDaily;
        private System.Windows.Forms.RadioButton radioButtonEveryDays;
        private System.Windows.Forms.RadioButton radioButtonEveryWeekDay;
        private System.Windows.Forms.TabPage tabPageWeekly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxStartMinutesWeekly;
        private System.Windows.Forms.ComboBox comboBoxStartHourWeekly;
        private System.Windows.Forms.CheckBox checkBox_Sunday;
        private System.Windows.Forms.CheckBox checkBox_Saturday;
        private System.Windows.Forms.CheckBox checkBox_Friday;
        private System.Windows.Forms.CheckBox checkBox_Thursday;
        private System.Windows.Forms.CheckBox checkBox_Wednesday;
        private System.Windows.Forms.CheckBox checkBox_Tuesday;
        private System.Windows.Forms.CheckBox checkBox_Monday;
        private System.Windows.Forms.TabPage tabPageMonthly;
        private System.Windows.Forms.TabPage tabPageYearly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSaveMinutes;
        private System.Windows.Forms.Button buttonSaveHourly;
        private System.Windows.Forms.Button buttonSaveDaily;
        private System.Windows.Forms.Button buttonSaveWeekly;
        private System.Windows.Forms.Button buttonSaveMonthly;
        private System.Windows.Forms.Button buttonSaveYearly;
        private System.Windows.Forms.ComboBox comboBoxEveryMinutes;
        private System.Windows.Forms.ComboBox comboBoxEveryHours;
        private System.Windows.Forms.ComboBox comboBoxEveryDays;
    }
}
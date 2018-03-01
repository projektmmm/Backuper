using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox_BackupType.Items.Add("Full");
            comboBox_BackupType.Items.Add("Differential");
            comboBox_BackupType.Items.Add("Incremental");
            comboBox_DaemonId.Items.Add("1");

            comboBox_DaemonId.SelectedIndex = 0;
            comboBox_BackupType.SelectedIndex = 0;
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            List<string> toPost = new List<string>();
            if (ValidateAll())
            {
                CommandInformation commandInformation = new CommandInformation()
                {
                    DaemonId = Convert.ToInt32(this.comboBox_DaemonId.SelectedItem),
                    RunAt = Convert.ToDateTime(this.dateTimePicker_RunAt.Value),
                    BackupType = this.comboBox_BackupType.SelectedItem.ToString(),
                    SourcePath = this.textBox_SourcePath.Text.Replace("\\","\\\\"),
                    DestinationPath = this.textBox_DestinationPath.Text.Replace("\\", "\\\\")
                };

                toPost.Add(JsonConvert.SerializeObject(commandInformation));
                ApiCommunication.PostBackupReport(toPost, "api/admin");
            }
        }

        private bool ValidateAll()
        {
            bool SourceBool = Regex.IsMatch(textBox_SourcePath.Text, @"^(?:[A-Za-z]{1}\:\\)");
            bool DestinationBool = Regex.IsMatch(textBox_DestinationPath.Text, @"^(?:[A-Za-z]{1}\:\\)");
            if (dateTimePicker_RunAt.Value > DateTime.Now && SourceBool && DestinationBool)
            {
                //datum není v minulosti && cesty začínaj na [A-Z]:\
                return true;
            }
            else
            {
                MessageBox.Show("NOT PROPERLY FILLED");
                return false;
            }
        }

        private void comboBox_BackupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_BackupType.SelectedIndex == 1 || comboBox_BackupType.SelectedIndex == 2)
            {
                textBox_DestinationPath.ReadOnly = true;
                textBox_SourcePath.ReadOnly = true;
            }
            else
            {
                textBox_DestinationPath.ReadOnly = false;
                textBox_SourcePath.ReadOnly = false;
            }
        }
    }
}
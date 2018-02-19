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

        private void button_Odeslat_Click(object sender, EventArgs e)
        {
            if (ValidateAll())
            {
                MessageBox.Show("OKboi");
            }
        }

        private void button_ProvestHned_Click(object sender, EventArgs e)
        {
            if (ValidateAll())
            {
                MessageBox.Show("OKboi");
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
                return false;
            }
        }
    }
}
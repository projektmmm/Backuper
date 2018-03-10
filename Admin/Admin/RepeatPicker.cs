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
    /// <summary>
    /// Vrací cron ve formátu * * * * * = minuta,hodina,den v měsíci, měsíc, den v týdnu
    /// lomeno před číslem znamená že to je každý časový údaj
    /// čárky oddělují např více dní v týdnu
    /// příklady: 
    /// 15 13 /1 * * = 13:15 každý den
    /// /1 * * * * = každou minutu
    /// 15 13 * * 1,2 = 13:15 každé pondělí a úterý
    /// </summary>
    public partial class RepeatPicker : Form
    {
        public string Cron { get; set; } = "* * * * *";

        public RepeatPicker()
        {
            InitializeComponent();
            AddDefaults();
        }

        private void buttonSaveMinutes_Click(object sender, EventArgs e)
        {
            this.Cron = "/" + comboBoxEveryMinutes.Text + " * * * *";
            this.Hide();
        }

        private void buttonSaveHourly_Click(object sender, EventArgs e)
        {
            if (radioButtonEveryHours.Checked)
            {
                this.Cron = "* /" + comboBoxEveryHours.Text + " * * *";
                this.Hide();
            }
            else if (radioButtonStartsAtHours.Checked)
            {
                this.Cron = comboBoxStartsMinutesHourly.Text + " " + comboBoxStartsHourHourly.Text + " /1 * *";
                this.Hide();
            }
        }

        private void buttonSaveDaily_Click(object sender, EventArgs e)
        {
            if (radioButtonEveryDays.Checked)
            {               
                this.Cron = comboBoxStartMinutesDaily.Text + " " + comboBoxStartHourDaily.Text + " /" + comboBoxEveryDays.Text + " * *";
                this.Hide();
            }
            else if (radioButtonEveryWeekDay.Checked)
            {
                this.Cron = comboBoxStartMinutesDaily.Text + " " + comboBoxStartHourDaily.Text + " /1 * *";
                this.Hide();
            }
        }

        private void buttonSaveWeekly_Click(object sender, EventArgs e)
        {
            List<int> days = new List<int>();

            if (checkBox_Monday.Checked)
            {
                days.Add(1);
            }

            if (checkBox_Tuesday.Checked)
            {
                days.Add(2);
            }

            if (checkBox_Wednesday.Checked)
            {
                days.Add(3);
            }

            if (checkBox_Thursday.Checked)
            {
                days.Add(4);
            }

            if (checkBox_Friday.Checked)
            {
                days.Add(5);
            }

            if (checkBox_Saturday.Checked)
            {
                days.Add(6);
            }

            if (checkBox_Sunday.Checked)
            {
                days.Add(7);
            }

            string Days = "";

            foreach (int item in days)
            {
                Days = Days + item + ",";
            }

            Days = Days.Substring(0, Days.Length - 1);

            this.Cron = comboBoxStartMinutesWeekly.Text + " " + comboBoxStartHourWeekly.Text + " * * " + Days;
            this.Hide();
        }

        private void buttonSaveMonthly_Click(object sender, EventArgs e)
        {
            //todo
        }

        private void buttonSaveYearly_Click(object sender, EventArgs e)
        {
            //todo
        }

        private void AddDefaults()
        {
            for (int i = 0; i <= 23; i++)
            {
                comboBoxStartsHourHourly.Items.Add(i); 
                comboBoxStartHourDaily.Items.Add(i);
                comboBoxStartHourWeekly.Items.Add(i);

                if (i >= 1)
                {
                    comboBoxEveryHours.Items.Add(i);
                }
            }

            comboBoxStartsHourHourly.SelectedIndex = 0;
            comboBoxStartHourDaily.SelectedIndex = 0;
            comboBoxStartHourWeekly.SelectedIndex = 0;

            comboBoxEveryHours.SelectedIndex = 0;

            for (int i = 0; i <= 59; i++)
            {
                comboBoxStartsMinutesHourly.Items.Add(i);
                comboBoxStartMinutesDaily.Items.Add(i);
                comboBoxStartMinutesWeekly.Items.Add(i);

                if (i >= 1)
                {
                    comboBoxEveryMinutes.Items.Add(i);
                }                  
            }

            comboBoxStartsMinutesHourly.SelectedIndex = 0;
            comboBoxStartMinutesDaily.SelectedIndex = 0;
            comboBoxStartMinutesWeekly.SelectedIndex = 0;

            comboBoxEveryMinutes.SelectedIndex = 0;

            for (int i = 1; i <= 31; i++)
            {
                comboBoxEveryDays.Items.Add(i);
            }

            comboBoxEveryDays.SelectedIndex = 0;
        }
    }
}

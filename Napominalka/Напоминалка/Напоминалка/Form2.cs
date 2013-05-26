using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Напоминалка
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((Form1)this.Owner).managerOfEvent.count > 9)
            {
                DialogResult result1 = MessageBox.Show("Ви ствроили більше 10 нагадувань. Якщо кількість в полі ''Активні нагадування'' < 10, можливо деякі події вже минули.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                int Iyear, Imonth, Idate, Ihour, Iminute;
                Iyear = Convert.ToInt32(numericUpDown6.Value);
                Imonth = Convert.ToInt32(numericUpDown5.Value);
                Idate = Convert.ToInt32(numericUpDown4.Value);
                Ihour = Convert.ToInt32(numericUpDown1.Value);
                Iminute = Convert.ToInt32(numericUpDown2.Value);

                DateTime date1 = new DateTime(Iyear, Imonth, Idate, Ihour, Iminute, 0);
                for (int i = 0; i < ((Form1)this.Owner).managerOfEvent.count; i++)
                {
                    if (date1 ==((Form1)this.Owner).managerOfEvent.Events[i].GetDateAndTime())
                    {
                        DialogResult result2 = MessageBox.Show("Подія на даний час уже зареєстрована.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (result2 == DialogResult.OK)
                        {
                            this.Close();
                            return;
                        }
                    }
                }
                    if (DateTime.Compare(date1, DateTime.Now) < 0)
                    {
                        DialogResult result2 = MessageBox.Show("Дане нагадування знаходиться в минулому часі, дана подія не буде додана", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (result2 == DialogResult.OK)
                        {
                            this.Close();
                            return;
                        }
                    }
                // Adding new event
                ((Form1)this.Owner).managerOfEvent.Events[((Form1)this.Owner).managerOfEvent.count++] = new Form1.MyEvent(date1, this.textBox1.Text);

                // Sorting events
                ((Form1)this.Owner).managerOfEvent.sort();

                MessageBox.Show("Нагадування успішно збережене", "AddEvent", MessageBoxButtons.OK, MessageBoxIcon.Information); //повідомлення після натискання кнопки "зберегти"
                this.Close(); //закрили форму


            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToInt32(DateTime.Now.Hour);
            numericUpDown2.Value = Convert.ToInt32(DateTime.Now.Minute);
            numericUpDown4.Value = Convert.ToInt32(DateTime.Now.Day);
            numericUpDown5.Value = Convert.ToInt32(DateTime.Now.Month);
            numericUpDown6.Value = Convert.ToInt32(DateTime.Now.Year);
        }
    }
}

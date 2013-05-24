using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;//for gitHub

namespace Напоминалка
{
    public partial class Form1 : Form
    {

        public class MyEvent
        {
            private DateTime DateAndTime;
            private string Message;
            private bool Active;

            public MyEvent(DateTime DateAndTime, string Message)
            {
                this.DateAndTime = DateAndTime;
                this.Message = Message;
                this.Active = true;
            }

            public DateTime GetDateAndTime() { return this.DateAndTime; }
            public string GetMessage() { return this.Message; }
            public bool IsActive() { return this.Active; }

            public void SetDateAndTime(DateTime DateAndTime) { this.DateAndTime = DateAndTime; }
            public void SetMessage(string Message) { this.Message = Message; }
            public void SetActive(bool State) { this.Active = State; }
        }

        public class Manager
        {
            public System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            public MyEvent[] Events = new MyEvent[10];
            public short count = 0;

            public int getActiveCount()
            {
                int con = 0;
                for (int i = 0; i < count; i++)
                    if (Events[i].IsActive())
                        con = con + 1;
                return con;
            }
            public void playsound()
            {
                sp.SoundLocation = (System.IO.Directory.GetCurrentDirectory()+@"\alarm_clock.wav");
                sp.Play();
            }
            public void sort()
            {
                for (int i = 0; i < count - 1; i++)
                    for (int j = 1; j < count; j++)
                    {
                        if (Events[i].GetDateAndTime() > Events[j].GetDateAndTime() || !Events[i].IsActive())
                        {
                            if (!Events[j].IsActive())
                            {
                               int q=2;//важлива змінна, яка нічого не робить, і тим же робить все
                            }
                            else
                            {
                                MyEvent tmp = Events[i];
                                Events[i] = Events[j];
                                Events[j] = tmp;
                            }
                        }
                    }
                for (int i = 0; i < count - 1; i++)
                    for (int j = 1; j < count; j++)
                    {
                        if (DateTime.Compare(Events[i].GetDateAndTime(), Events[j].GetDateAndTime()) > 0 || DateTime.Compare(Events[i].GetDateAndTime(), Events[j].GetDateAndTime()) < 0)
                            return;
                        if (DateTime.Compare(Events[i].GetDateAndTime(),Events[j].GetDateAndTime())==0)
                        {
                            DateTime d1 = new DateTime(Events[j].GetDateAndTime().Year, Events[j].GetDateAndTime().Month, Events[j].GetDateAndTime().Day, Events[j].GetDateAndTime().Hour, Events[j].GetDateAndTime().Minute, count+1);
                            Events[j].SetDateAndTime(d1);
                        }
                    }
            }
        }
        public Manager managerOfEvent = new Manager();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = managerOfEvent.getActiveCount().ToString();
            label2.Text = DateTime.Now.ToString("HH:mm:ss");
            for (int i = 0; i < managerOfEvent.count; i++)
            {
                dataGridView1.Rows[i + 1].Cells[1].Value = Convert.ToString(managerOfEvent.Events[i].GetDateAndTime());
                dataGridView1.Rows[i + 1].Cells[2].Value = Convert.ToString(managerOfEvent.Events[i].GetMessage());
            }
            if (this.managerOfEvent.count == 0) return;
            if (this.managerOfEvent.Events[0].GetDateAndTime().Hour == DateTime.Now.Hour && this.managerOfEvent.Events[0].GetDateAndTime().Minute == DateTime.Now.Minute
                && this.managerOfEvent.Events[0].GetDateAndTime().Date == DateTime.Now.Date && this.managerOfEvent.Events[0].IsActive())
            {
                this.managerOfEvent.Events[0].SetActive(false);
                this.managerOfEvent.playsound();
                DialogResult result = MessageBox.Show(this.managerOfEvent.Events[0].GetMessage(), "Event", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    managerOfEvent.sp.Stop();
                }
                this.managerOfEvent.sort();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.ShowInTaskbar = false;               //ховаємо форму з панелі задач
            secondForm.StartPosition = FormStartPosition.CenterScreen; //встановлюємо форму в центрі екрану
            secondForm.ShowDialog(this);                //указуємо хазяїна для форми
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Height = 170;
            dataGridView1.RowCount = 11;
            dataGridView1.ColumnCount = 3;
            for (int i = 1; i < 11; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i);
            }
            dataGridView1.Rows[0].Cells[0].Value = "№";
            dataGridView1.Rows[0].Cells[1].Value = "             Час             ";
            dataGridView1.Rows[0].Cells[2].Value = "      Текст    Повідомлення     ";
            dataGridView1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            Height = 480;
            if (managerOfEvent.count == 0)
            {
                dataGridView1.Visible = true;
                return;
            }
            else
            {
                dataGridView1.Visible = true;
                for (int i = 0; i < managerOfEvent.count; i++)
                {
                    dataGridView1.Rows[i + 1].Cells[1].Value = Convert.ToString(managerOfEvent.Events[i].GetDateAndTime());
                    dataGridView1.Rows[i + 1].Cells[2].Value = Convert.ToString(managerOfEvent.Events[i].GetMessage());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            button4.Visible = false;
            Height = 170;
        }
    }
}

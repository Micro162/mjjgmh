using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace mjjgmh
{
    public class Form1 : Form
    {
        private ListView listViewProcesses = new ListView();
        private Button btnRefresh = new Button();

        public Form1()
        {
            this.Text = "Список запущених процесів";
            this.Width = 700;
            this.Height = 500;

            listViewProcesses.Dock = DockStyle.Fill;
            listViewProcesses.View = View.Details;
            listViewProcesses.FullRowSelect = true;
            listViewProcesses.GridLines = true;
            listViewProcesses.Columns.Add("PID", 70);
            listViewProcesses.Columns.Add("Назва процесу", 250);
            listViewProcesses.Columns.Add("Пам'ять (МБ)", 120);
            listViewProcesses.Columns.Add("Час запуску", 200);

            btnRefresh.Text = "Оновити";
            btnRefresh.Dock = DockStyle.Bottom;
            btnRefresh.Height = 35;
            btnRefresh.Click += (s, e) => LoadProcesses();

            this.Controls.Add(listViewProcesses);
            this.Controls.Add(btnRefresh);

            LoadProcesses();
        }

        private void LoadProcesses()
        {
            listViewProcesses.Items.Clear();

            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    var item = new ListViewItem(proc.Id.ToString());
                    item.SubItems.Add(proc.ProcessName);
                    item.SubItems.Add((proc.WorkingSet64 / 1024 / 1024).ToString());

                    string startTime;
                    try
                    {
                        startTime = proc.StartTime.ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch
                    {
                        startTime = "н/д";
                    }
                    item.SubItems.Add(startTime);

                    listViewProcesses.Items.Add(item);
                }
                catch
                {
                    // Пропускаємо процеси без доступу
                }
            }
        }
    }
}
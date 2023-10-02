using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveConnectionTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetProgressBar(int percent)
        {
            progressBar1.Value = percent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            label_result.Text = string.Empty;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label_result_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start Test")
            {
                progressBar1.Visible = true;
                Task.Run(() =>
                {
                    while (progressBar1.Value < 100)
                    {
                        progressBar1.Value += 20;
                        Task.Delay(1000).Wait();
                    }
                });

                DriveTest driveTest = new DriveTest();
                bool _driveTestResult = driveTest.TestConnection(textBox1.Text);
                progressBar1.Value = 100;
                startButton.Text = "Reset";
                textBox1.Enabled = false;
                if (_driveTestResult)
                {
                    label_result.Text = "Successfully connected to drive system";
                    label_result.ForeColor = Color.Green;
                }
                else
                {
                    label_result.Text = "Failed to connect to drive system";
                    label_result.ForeColor = Color.Red;
                }
            } else
            {
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                startButton.Text = "Start Test";
                label_result.Text = string.Empty;
                textBox1.Text = string.Empty;
                textBox1.Enabled = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppGray
{
    public partial class SplashScreen : Form
    {
        int progress = 0;
        public SplashScreen()
        {
            InitializeComponent();
            timer1.Interval = 5;
            timer1.Start();
           
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            progress++;
            if (progress >= 100)
            {
                timer1.Stop();
                this.Close();

                //New Code
            }

            progressBar1.Value = progress;
            label1 .Text = progress.ToString() + "%"; 
        }
    }
}

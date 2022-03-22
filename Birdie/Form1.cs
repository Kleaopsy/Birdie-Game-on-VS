using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Birdie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("BackGround.png");
            label2.Visible = false;
        }

        Timer Start = new Timer();
        private void PressToSpace(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Space)
            {
                Start.Interval =1000;
                Start.Tick += Start_Tick;
                Start.Enabled = true;
            }
        }

        
        
        int UcIkiBir = 3;
        Birdie birdie = new Birdie();
        private void Start_Tick(object sender, EventArgs e)
        {
            label2.Visible = true;
            label2.Text=UcIkiBir.ToString();

            if (UcIkiBir==0)
            {
                label2.Visible = false;
                UcIkiBir = 3;
                Start.Enabled = false;

                birdie.ShowDialog();
            }
            else
            {
                UcIkiBir--;
            }
        }
    }
}

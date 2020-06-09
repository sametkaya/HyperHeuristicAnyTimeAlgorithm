using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperHeuristicAnyTimeAlgorithm
{
    public partial class Form1 : Form
    {
        PWP_HyperHeuristic pwp_hyper;
        int circleSize = 20;
        int radius = 10;

        public Form1()
        {
            InitializeComponent();

        }



        private void btn_set_Click(object sender, EventArgs e)
        {

            pwp_hyper = new PWP_HyperHeuristic(this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height, (int)this.nupd_pointCount.Value, 10);
            this.btn_run.Enabled = true;
            this.btn_anyTime.Enabled = true;
            this.nupd_times.Enabled = true;
            this.btn_resume.Enabled = true;
            //pwp_hyper.distanceUpdatedEvent += Pwp_hyper_distanceUpdatedEvent;
            //pwp_hyper.DrawBestSolution();
            splitContainer1.Panel2.Refresh();
        }
        /*
        private void Pwp_hyper_distanceUpdatedEvent()
        {
            foreach (double item in this.pwp_hyper.distances)
            {
                chrt_distance.Series["Distance"].Points.AddY(item);
            }
        }*/

        public void ShowDistance(List<double> distances)
        {

            chrt_distance.Series["Distance"].Points.Clear();

            for (int i = 0; i < distances.Count; i++)
            {
                chrt_distance.Series["Distance"].Points.AddY(distances[i]);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (pwp_hyper == null)
            {
                return;
            }

            this.pwp_hyper.DrawBestSolution(e.Graphics);
        }


        private void btn_run_Click(object sender, EventArgs e)
        {

            //splitContainer2.Panel1.Refresh();
            int times = (int)this.nupd_times.Value;

            this.pwp_hyper.StartAlgorithm(times);

            //splitContainer2.Panel1.Refresh();

        }

        private void btn_anyTime_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Refresh();
            chrt_distance.Series["Distance"].Points.Clear();
            for (int i = 0; i < this.pwp_hyper.distances.Count; i++)
            {
                chrt_distance.Series["Distance"].Points.AddY(this.pwp_hyper.distances[i]);
            }
            /*foreach (double item in this.pwp_hyper.distances)
            {
                chrt_distance.Series["Distance"].Points.AddY(item);
            }*/



        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
            pwp_hyper.isPaused = !pwp_hyper.isPaused;
            if (pwp_hyper.isPaused)
            {
                btn_resume.Text = "Paused >";
            }
            else
            {
                btn_resume.Text = "Pause ||";
            }
        }
    }
}

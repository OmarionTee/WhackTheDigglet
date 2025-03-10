using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WackAJack
{
    public partial class Form1 : Form
    {
        static Image Front = WackAJack.Properties.Resources.Diglett_Front;
        static Image Hole = WackAJack.Properties.Resources.Diglett_Hole;
        int score = 0;
        int missed = 0;
        Random random = new Random();
        PictureBox[] pictureBoxes;
        Timer Digglet;
        Image[] image = new Image[] {Front, Hole};

        public Form1()
        {
            InitializeComponent();
            
            pictureBoxes = new PictureBox[] {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9};
            
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Click += Digglet_Click;
            }

            Digglet = new Timer();
            Digglet.Interval = 2500;
            Digglet.Tick += Timer_Tick;
            Digglet.Start();

            pictureBox1.Image = Hole;
            pictureBox2.Image = Hole;
            pictureBox3.Image = Hole;
            pictureBox4.Image = Hole;
            pictureBox5.Image = Hole;
            pictureBox6.Image = Hole;
            pictureBox7.Image = Hole;
            pictureBox8.Image = Hole;
            pictureBox9.Image = Hole;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                if (pictureBox.Image == Front)
                {
                    missed++;
                    Misser();
                }
                pictureBox.Image = Hole;
            }

            if (missed >= 10)
            {
                Digglet.Stop();
                MessageBox.Show($"Game Over! You missed 10 digglets! Your score is: {score}");
                return;
            }

            int randomIndex = random.Next(pictureBoxes.Length);
            pictureBoxes[randomIndex].Image = Front;
        }

        private void Digglet_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = sender as PictureBox;

            if (clickedBox.Image == Front)
            {
                score++;
                clickedBox.Image = Hole;
                ScorePotator();
                AdjustTimer();
            }
        }

        private void AdjustTimer()
        {
            if (score % 10 == 0 && Digglet.Interval > 500)
            {
                Digglet.Interval -= 500;
            }
        }

        private void ScorePotator()
        {
            label1.Text = "Score: " + score;
        }
        private void Misser()
        {
            label2.Text = "Missed: " + missed;
        }

    }
}

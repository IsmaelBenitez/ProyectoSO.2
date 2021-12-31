using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace cliente
{
    public partial class Acusacion : Form
    {
        List<Carta> Lista = new List<Carta>();
        List<PictureBox> lpic = new List<PictureBox>();
        int num;
        public Acusacion()
        {
            InitializeComponent();
        }

        private void Acusacion_Load(object sender, EventArgs e)
        {
            if (Lista.Count == 1)
            {
                pictureBox2.Image = Lista[0].GetImage();
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox2);
                pictureBox1.Visible = false;
                pictureBox3.Visible = false;

            }
            else if (Lista.Count == 2)
            {
                pictureBox1.Image = Lista[0].GetImage();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox1);

                pictureBox3.Image = Lista[1].GetImage();
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox3);

                pictureBox2.Visible = false;
            }
            else if (Lista.Count == 3)
            {
                pictureBox1.Image = Lista[0].GetImage();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox1);

                pictureBox2.Image = Lista[1].GetImage();
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox2);

                pictureBox3.Image = Lista[2].GetImage();
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                lpic.Add(pictureBox3);
            }
        }
        public void GetLista(List<Carta> Lista)
        {
            this.Lista = Lista;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            num = Lista[0].GetNum();
            Close();
        }
        public int GiveNum()
        {
            return num;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Lista.Count == 1)
            {
                num = Lista[0].GetNum();
                Close();
            }
            else if (Lista.Count == 3)
            {
                num = Lista[1].GetNum();
                Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Lista.Count == 2)
            {
                num = Lista[1].GetNum();
                Close();
            }
            else if (Lista.Count == 3)
            {
                num = Lista[2].GetNum();
                Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cliente
{
    public partial class Solucionar : Form
    {
        int[] solucion = new int[3];
        public Solucionar()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void Solucionar_Load(object sender, EventArgs e)
        {

        }
        private void ActivarArmas()
        {
            label4.Visible = true;

            pictureBox14.Visible = true;
            pictureBox14.Enabled = true;

            pictureBox13.Visible = true;
            pictureBox13.Enabled = true;

            pictureBox12.Visible = true;
            pictureBox12.Enabled = true;

            pictureBox11.Visible = true;
            pictureBox11.Enabled = true;

            pictureBox10.Visible = true;
            pictureBox10.Enabled = true;

            pictureBox9.Visible = true;
            pictureBox9.Enabled = true;

            pictureBox22.Visible = true;
            pictureBox22.Enabled = true;

            pictureBox24.Visible = true;
            pictureBox24.Enabled = true;
        }
        private void ActivarLugares()
        {
            label2.Visible = true;

            pictureBox21.Visible = true;
            pictureBox21.Enabled = true;

            pictureBox20.Visible = true;
            pictureBox20.Enabled = true;

            pictureBox19.Visible = true;
            pictureBox19.Enabled = true;

            pictureBox18.Visible = true;
            pictureBox18.Enabled = true;

            pictureBox17.Visible = true;
            pictureBox17.Enabled = true;

            pictureBox16.Visible = true;
            pictureBox16.Enabled = true;

            pictureBox23.Visible = true;
            pictureBox23.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            solucion[0] = 0;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("azahara.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            solucion[0] = 1;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("guillem.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            solucion[0] = 2;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("ismael.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            solucion[0] = 3;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("itziar.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            solucion[0] = 4;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("pedro.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            solucion[0] = 5;
            ActivarArmas();
            label3.Visible = true;
            pictureBox25.BackgroundImage = new Bitmap("victor.png");
            pictureBox25.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            solucion[1] = 6;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("barra.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            solucion[1] = 7;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("cables.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            solucion[1] = 8;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("cuchillo.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            solucion[1] = 9;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("extintor.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            solucion[1] = 10;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("osciloscopio.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            solucion[1] = 11;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("termo.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            solucion[1] = 12;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("tijeras.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            solucion[1] = 13;
            ActivarLugares();
            label5.Visible = true;
            pictureBox26.BackgroundImage = new Bitmap("veneno.png");
            pictureBox26.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public int[] DarDatos()
        {
            return solucion;
        }

        private void enviar_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            solucion[2] = 18;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("residencia.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            solucion[2] = 17;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("hall.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            solucion[2] = 16;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("estacion.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            solucion[2] = 15;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("clase.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            solucion[2] = 14;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("cafeteria.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            solucion[2] = 19;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("biblioteca.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            solucion[2] = 20;
            label6.Visible = true;
            pictureBox27.BackgroundImage = new Bitmap("actos.png");
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            enviar_btn.Enabled = true;
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }
    }
}

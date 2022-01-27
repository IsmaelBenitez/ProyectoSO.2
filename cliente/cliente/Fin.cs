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
    public partial class Fin : Form
    {
        List<Carta> cartas = new List<Carta>();
        string usuario;
        Boolean Tu;
        public Fin(List<Carta> cartas, string usuario,Boolean Tu)
        {
            InitializeComponent();
            this.cartas = cartas;
            this.usuario = usuario;
            this.Tu = Tu;
        }

        private void Fin_Load(object sender, EventArgs e)
        {
            if (Tu)
                label3.Text = "Enhorabuena, tu combinación es correcta ! ";
            else
                label3.Text = usuario + " ha acertado la combinación correcta !";
            pictureBox1.BackgroundImage = cartas[0].GetImage();
            pictureBox2.BackgroundImage = cartas[1].GetImage();
            pictureBox27.BackgroundImage = cartas[2].GetImage();
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox27.BackgroundImageLayout = ImageLayout.Stretch;
            label3.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

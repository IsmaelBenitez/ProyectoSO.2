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
    public partial class video : Form
    {
        // Este es el form donde esta el video tutorial.
        //Se muestra mediante un reproductor windowmedia player
        public video()
        {
            InitializeComponent();
        }

        private void video_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"Tutorial.mp4";
        }

        private void video_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
    }
}

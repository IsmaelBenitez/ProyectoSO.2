﻿using System;
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
        public video()
        {
            InitializeComponent();
        }

        private void video_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = @"C:\Users\ismae\Desktop\video\Video_V2.mp4";
        }

        private void video_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.close();
        }
    }
}

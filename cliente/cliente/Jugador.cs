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
    public class Jugador
    {
        string nombre;
        string avatar;
        int x;
        int y;
        PictureBox pic;
        
        public void RefreshLocation()
        {
            
            pic.Width = 20;
            pic.Height = 20;
            pic.ClientSize = new Size(20, 20);

            pic.Location = new Point(x,y);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Jugador(string nombre,string avatar)
        {
            this.nombre = nombre;
            this.avatar = avatar;
            pic = new PictureBox();
            pic.Width = 30;
            pic.Height = 30;
            pic.ClientSize = new Size(30, 30);
            
            switch (avatar)
            {
                case "aza":
                    setX(185);
                    setY(35);
                    pic.BackColor = Color.Red;
                    pic.Location = new Point(185,35);
                    
                    break;
                case "guillem":
                    setX(35);
                    setY(185);
                    pic.BackColor = Color.LightGoldenrodYellow;
                    pic.Location = new Point(35, 185);

                    break;
                case "ismael":
                    setX(335);
                    setY(635);
                    pic.BackColor = Color.Green;
                    pic.Location = new Point(335, 635);

                    break;
                case "itziar":
                    setX(35);
                    setY(485);
                    pic.BackColor = Color.DarkViolet;
                    pic.Location = new Point(35,485);

                    break;
                case "pedro":
                    setX(485);
                    setY(185);
                    pic.BackColor = Color.HotPink;
                    pic.Location = new Point(485, 185);

                    break;
                case "victor":
                    setX(485);
                    setY(485);
                    pic.BackColor = Color.Blue;
                    pic.Location = new Point(485, 485);
                    break;
            }

        }
        public string GetNombre()
        {
            return this.nombre;
        }
        public string GetAvatar()
        {
            return this.avatar;
        }
        public int GetX()
        {
            return x;
        }
        public  int GetY()
        {
            return y;
        }
        public void setX(int x)
        {
            this.x = x;
        }
        public void setY(int y)
        {
            this.y = y;
        }
        public PictureBox GetPictureBox()
        {
            return pic;
        }
    }
}

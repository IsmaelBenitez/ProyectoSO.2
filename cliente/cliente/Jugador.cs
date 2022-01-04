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

        PictureBox pic;
        Point Posicion;
        
        public void RefreshLocation(Point Nueva)
        {

            pic.Location =new Point (Nueva.X+25/2,Nueva.Y+25/2);
            Posicion = Nueva;
            pic.Refresh();
            
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
                    Posicion= new Point(175, 25);
                    pic.Image =new Bitmap("fichaaza.png");
                    pic.BackColor = Color.IndianRed;
                    pic.Location = new Point(Posicion.X+25/2,Posicion.Y+25/2);
                    
                    break;
                case "guillem":
                    Posicion = new Point(25, 175);
                    pic.Image = new Bitmap("fichaguillem.png");
                    pic.BackColor = Color.Aquamarine;
                    pic.Location = new Point(Posicion.X + 25 / 2, Posicion.Y + 25 / 2); 

                    break;
                case "ismael":
                    Posicion = new Point(325, 625);
                    pic.Image = new Bitmap("fichaisma.png");
                    pic.BackColor = Color.ForestGreen;
                    pic.Location = new Point(Posicion.X + 25 / 2, Posicion.Y + 25 / 2); 

                    break;
                case "itziar":
                    Posicion= new Point(25, 475);
                    pic.Image = new Bitmap("fichaitzi.png");
                    pic.BackColor = Color.DarkMagenta;
                    pic.Location = new Point(Posicion.X + 25 / 2, Posicion.Y + 25 / 2); 

                    break;
                case "pedro":
                    Posicion = new Point(475, 175);
                    pic.Image = new Bitmap("fichapedro.png");
                    pic.BackColor = Color.DeepPink;
                    pic.Location = new Point(Posicion.X + 25 / 2, Posicion.Y + 25 / 2); 

                    break;
                case "victor":
                    Posicion= new Point(475, 475);
                    pic.Image = new Bitmap("fichavictor.png");
                    pic.BackColor = Color.Gold;
                    pic.Location = new Point(Posicion.X + 25 / 2, Posicion.Y + 25 / 2); ;
                    break;
            }
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            

        }
        public string GetNombre()
        {
            return this.nombre;
        }
        public void SetNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public string GetAvatar()
        {
            return this.avatar;
        }
        public void SetAvatar(string avatar)
        {
            this.avatar = avatar;
        }
        
        public PictureBox GetPictureBox()
        {
            return pic;
        }
        public void SetPictureBox(Point Punto)
        {
            pic.Location = new Point(Punto.X + 25 / 2, Punto.Y + 25 / 2);
        }
        public Point GetPoint()
        {
            return Posicion;
        }
        public void SetPosicion(Point Punto)
        {
            Posicion = Punto;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cliente
{
    public class Carta
    {
        int num;
        string nombre;
        PictureBox Pic=new PictureBox();
        Bitmap image;

        public Carta(int num)
        {
            this.num = num;
            switch (num)
            {
                case 0:
                    nombre = "Azahara";
                    this.image = new Bitmap("azahara.png");
                    break;
                case 1:
                    nombre = "Guillem";
                    this.image = new Bitmap("guillem.png");
                    break;
                case 2:
                    nombre = "Ismael";
                    this.image = new Bitmap("ismael.png");
                    break;
                case 3:
                    nombre = "Itziar";
                    this.image = new Bitmap("itziar.png");
                    break;
                case 4:
                    nombre = "Pedro";
                    this.image = new Bitmap("pedro.png");
                    break;
                case 5:
                    nombre = "Victor";
                    this.image = new Bitmap("victor.png");
                    break;
                case 6:
                    nombre = "Barra Metálica";
                    this.image = new Bitmap("barra.png");
                    break;
                case 7:
                    nombre = "Cables";
                    this.image = new Bitmap("cables.png");
                    break;
                case 8:
                    nombre = "Cuchillo";
                    this.image = new Bitmap("cuchillo.png");
                    break;
                case 9:
                    nombre = "Extintor";
                    this.image = new Bitmap("extintor.png");
                    break;
                case 10:
                    nombre = "Osciloscopio";
                    this.image = new Bitmap("osciloscopio.png");
                    break;
                case 11:
                    nombre = "Termo";
                    this.image = new Bitmap("termo.png");
                    break;
                case 12:
                    nombre = "Tijeras";
                    this.image = new Bitmap("tijeras.png");
                    break;
                case 13:
                    nombre = "Veneno";
                    this.image = new Bitmap("veneno.png");
                    break;
                case 14:
                    nombre = "Cafetería";
                    this.image = new Bitmap("cafeteria.png");
                    break;
                case 15:
                    nombre = "Clase";
                    this.image = new Bitmap("clase.png");
                    break;
                case 16:
                    nombre = "Estación del Bus";
                    this.image = new Bitmap("estacion.png");
                    break;
                case 17:
                    nombre = "Hall";
                    this.image = new Bitmap("hall.png");
                    break;
                case 18:
                    nombre = "Residencia";
                    this.image = new Bitmap("residencia.png");
                    break;
                case 19:
                    nombre = "Biblioteca";
                    this.image = new Bitmap("biblioteca.png");
                    break;
                case 20:
                    nombre = "Sala de actos";
                    this.image = new Bitmap("actos.png");
                    break;
            }
            Pic.Image = image;
        }
        public string GetNombre()
        {
            return nombre;
        }
        public int GetNum()
        {
            return num;
        } 
        public PictureBox DamePic()
        {
            return Pic;
        }
        public Bitmap GetImage()
        {
            return image;
        }
    }
}

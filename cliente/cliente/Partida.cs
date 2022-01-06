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
using System.Media;

namespace cliente
{
    public partial class Partida : Form
    {
        Socket server;
        string[] Datos;
        string sesion;
        int IDp;
        int nMensajes = 0;
        int dado = 0;
        int tiempo = 0;

        delegate void DelegadoParaEscribirMensaje(string mensaje);
        delegate void DelegadoparaJugador(Jugador Jug);
        delegate void DelegadoParaCartas(Carta carta);
        delegate void Delegado(string[] trozos);


        Queue<string> cola = new Queue<string>();
        List<Jugador> Jugadores = new List<Jugador>();
        List<Carta> Ganadoras = new List<Carta>();
        List<Carta> MisCartas = new List<Carta>();

        string turno;
        int Num;
        string[] chatmensajes = new string[7];
        Boolean room = false;
        Boolean escogido = false;
        public Partida(Socket server, string[] trozos, string sesion)
        {
            InitializeComponent();

            this.server = server;
            Datos = trozos;
            this.sesion = sesion;

            IDp = Convert.ToInt32(trozos[Convert.ToInt32(trozos[1]) + 2]);
            int num = Convert.ToInt32(trozos[1]);
            for (int i = 0; i < num; i++)
            {
                cola.Enqueue(trozos[i + 2]);
            }
        }
        private void btn_chat_Click(object sender, EventArgs e)
        {
            if (MensajeBox.Text != string.Empty)
            {
                string Mensaje = "8/" + IDp + "/" + sesion + "/" + MensajeBox.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                server.Send(msg);
                MensajeBox.Clear();
            }
        }
        public void RecibirMensaje(string mensaje)
        {
            DelegadoParaEscribirMensaje delegado = new DelegadoParaEscribirMensaje(EscribeMensaje);
            Invoke(delegado, new object[] { mensaje });
        }
        public void RecibirAvatar(string nombre, string avatar)
        {
            Jugador Jug = new Jugador(nombre, avatar);
            DelegadoparaJugador delegado = new DelegadoparaJugador(PonJugador);
            Invoke(delegado, new object[] { Jug });


        }
        private void EscribeMensaje(string mensaje)
        {
            if (nMensajes < 6)
            {
                chatmensajes[nMensajes] = mensaje;
                Chat.Text = Chat.Text + Environment.NewLine + mensaje;
                nMensajes++;
            }
            else
            {
                Chat.Text = string.Empty;
                chatmensajes[0] = null;
                int i = 1;
                while (i < 6)
                {
                    chatmensajes[i - 1] = chatmensajes[i];
                    i++;
                }
                chatmensajes[5] = mensaje;
                int j = 1;
                Chat.Text = chatmensajes[0];
                while (j < 6)
                {
                    Chat.Text = Chat.Text + Environment.NewLine + chatmensajes[j];
                    j++;
                }

            }
        }
        private void PonJugador(Jugador Jug)
        {
            Jugadores.Add(Jug);
            cola.Enqueue(turno);
            turno = cola.Dequeue();
            if (turno == sesion)
            {
                turno_lbl.Text = "Tu turno!";
                turno_lbl.ForeColor = Color.Green;
                SoundPlayer simpleSound = new SoundPlayer(@"sonido.wav");
                simpleSound.Play();
            }
            else
            {
                turno_lbl.Text = "Turno de: "+ turno;
                turno_lbl.ForeColor = Color.Black;
            }
            string avatar = Jug.GetAvatar();
            switch (avatar)
            {
                case "aza":
                    aza_button.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();

                    break;
                case "guillem":
                    guillem_btn.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();
                    break;
                case "ismael":
                    ismael_btn.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();
                    break;
                case "itziar":
                    itziar_btn.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();
                    break;
                case "pedro":
                    pedro_btn.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();
                    break;
                case "victor":
                    victor_btn.Visible = false;
                    Controls.Add(Jugadores[Jugadores.Count() - 1].GetPictureBox());
                    Jugadores[Jugadores.Count() - 1].GetPictureBox().BringToFront();
                    break;
            }
            Num++;
            if (Num >= cola.Count() + 1)
            {
                aza_button.Visible = false;
                guillem_btn.Visible = false;
                ismael_btn.Visible = false;
                itziar_btn.Visible = false;
                pedro_btn.Visible = false;
                victor_btn.Visible = false;
            }


        }
        private void MensajeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (MensajeBox.Text != string.Empty)
                {
                    string Mensaje = "8/" + IDp + "/" + sesion + "/" + MensajeBox.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                    server.Send(msg);
                    MensajeBox.Clear();
                }
            }
        }
        private void Partida_Load(object sender, EventArgs e)
        {
            timer1.Interval = 60000;
            timer1.Start();

            int x = 175;
            int y = 25;
            string strButtonName = "btn_Tablero";  //Nombre que van a tener los botones del tablero

            int nBoton = 0; //Almaceno el numero de boton
            for (int i = 1; i <= 3; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 4; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);


                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 175;

            }

            x = 25;
            y = 175;

            for (int i = 1; i <= 2; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 10; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);


                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 25;
            }
            x = 175;
            y = 275;

            for (int i = 1; i <= 3; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 1; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);


                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 175;
            }
            x = 325;
            y = 275;

            for (int i = 1; i <= 3; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 1; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);

                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 325;
            }
            x = 25;
            y = 425;

            for (int i = 1; i <= 2; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 10; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);


                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 25;
            }
            x = 175;
            y = 525;

            for (int i = 1; i <= 3; i++)
            {
                nBoton++; //Para dessincronizarlos, le sumo 1 al cambiar de fila
                for (int j = 1; j <= 4; j++)
                {
                    var btn = new Button();
                    btn.Text = "";
                    btn.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
                    btn.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
                    btn.Location = new Point(x, y);
                    btn.Size = new Size(50, 50);
                    btn.BackgroundImage = Properties.Resources._1;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Empty;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(btn_click);


                    this.Controls.Add(btn);
                    x += 50;
                    nBoton++;
                }

                y += 50;
                x = 175;
            }
            var room1 = new Button();
            room1.Text = "";
            room1.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room1.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room1.Location = new Point(25, 25);
            room1.Size = new Size(150, 150);
            room1.BackgroundImage = new Bitmap("classelloc.png");
            room1.BackgroundImageLayout = ImageLayout.Stretch;
            room1.FlatStyle = FlatStyle.Flat;
            room1.FlatAppearance.BorderColor = Color.Empty;
            room1.FlatAppearance.BorderSize = 0;
            room1.Click += new EventHandler(room1_Click);


            this.Controls.Add(room1);

            nBoton++;

            var room2 = new Button();
            room2.Text = "";
            room2.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room2.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room2.Location = new Point(375, 25);
            room2.Size = new Size(150, 150);
            room2.BackgroundImage = new Bitmap("estaciolloc.png");
            room2.BackgroundImageLayout = ImageLayout.Stretch;
            room2.FlatStyle = FlatStyle.Flat;
            room2.FlatAppearance.BorderColor = Color.Empty;
            room2.FlatAppearance.BorderSize = 0;
            room2.Click += new EventHandler(room2_Click);

            this.Controls.Add(room2);

            nBoton++;

            var room3 = new Button();
            room3.Text = "";
            room3.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room3.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room3.Location = new Point(25, 275);
            room3.Size = new Size(150, 150);
            room3.BackgroundImage = new Bitmap("salalloc.png");
            room3.BackgroundImageLayout = ImageLayout.Stretch;
            room3.FlatStyle = FlatStyle.Flat;
            room3.FlatAppearance.BorderColor = Color.Empty;
            room3.FlatAppearance.BorderSize = 0;
            room3.Click += new EventHandler(room3_Click);


            this.Controls.Add(room3);

            nBoton++;

            var room4 = new Button();
            room4.Text = "";
            room4.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room4.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room4.Location = new Point(25, 525);
            room4.Size = new Size(150, 150);
            room4.BackgroundImage = new Bitmap("bibliolloc.png") ;
            room4.BackgroundImageLayout = ImageLayout.Stretch;
            room4.FlatStyle = FlatStyle.Flat;
            room4.FlatAppearance.BorderColor = Color.Empty;
            room4.FlatAppearance.BorderSize = 0;
            room4.Click += new EventHandler(room4_Click);


            this.Controls.Add(room4);

            nBoton++;

            var room5 = new Button();
            room5.Text = "";
            room5.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room5.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room5.Location = new Point(375, 525);
            room5.Size = new Size(150, 150);
            room5.BackgroundImage = new Bitmap("cafelloc.png");
            room5.BackgroundImageLayout = ImageLayout.Stretch;
            room5.FlatStyle = FlatStyle.Flat;
            room5.FlatAppearance.BorderColor = Color.Empty;
            room5.FlatAppearance.BorderSize = 0;
            room5.Click += new EventHandler(room5_Click);


            this.Controls.Add(room5);

            nBoton++;

            var room6 = new Button();
            room6.Text = "";
            room6.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room6.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room6.Location = new Point(375, 275);
            room6.Size = new Size(150, 150);
            room6.BackgroundImage = new Bitmap("resilloc.png");
            room6.BackgroundImageLayout = ImageLayout.Stretch;
            room6.FlatStyle = FlatStyle.Flat;
            room6.FlatAppearance.BorderColor = Color.Empty;
            room6.FlatAppearance.BorderSize = 0;
            room6.Click += new EventHandler(room6_Click);


            this.Controls.Add(room6);

            nBoton++;

            var room7 = new Button();
            room7.Text = "";
            room7.Name = string.Format("{0}_{1}", strButtonName, nBoton); // Le doy un name que me permita identificar el boton
            room7.Tag = nBoton; // Le añado un tag, para permitirme ordenarlos
            room7.Location = new Point(225, 275);
            room7.Size = new Size(100, 150);
            room7.BackgroundImage =new Bitmap("halllloc.png") ;
            room7.BackgroundImageLayout = ImageLayout.Stretch;
            room7.FlatStyle = FlatStyle.Flat;
            room7.FlatAppearance.BorderColor = Color.Empty;
            room7.FlatAppearance.BorderSize = 0;
            room7.Click += new EventHandler(room7_Click);


            this.Controls.Add(room7);

            nBoton++;
            turno_lbl.AutoSize = true;
            turno = cola.Dequeue();
            if (turno == sesion)
            {
                turno_lbl.Text = "Tu turno!";
                turno_lbl.ForeColor = Color.Green;
                SoundPlayer simpleSound = new SoundPlayer(@"sonido.wav");
                simpleSound.Play();
            }
            else
            {
                turno_lbl.Text = "Turno de: "+turno;
                turno_lbl.ForeColor = Color.Black;
            }



        }
        private void btn_click(object sender, EventArgs e)
        {
            if (sesion == turno)
            {
                Button cb = (sender as Button);
                int i = DameJugador(sesion);
                Boolean Puedes;
                if (room && dado == 1)
                {
                    Jugadores[i].RefreshLocation(Jugadores[i].GetPoint());
             
                    string Mensaje = "10/" + IDp + "/" + sesion + "/" + Jugadores[i].GetPoint().X + "/" + Jugadores[i].GetPoint().Y;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                    server.Send(msg);
                    cola.Enqueue(turno);
                    turno = cola.Dequeue();
                    turno_lbl.Text = "Turno de: " + turno;
                    turno_lbl.ForeColor = Color.Black;
                    dado = 0;
                    Dado_btn.Enabled = false;
                    room = false;

                }
                else
                {
                    if (!room)
                        Puedes = Distancia(cb.Location, Jugadores[i].GetPoint(), dado);
                    else
                    {
                        Puedes = Distancia(cb.Location, Jugadores[i].GetPoint(), dado - 1);
                    }

                    if (Puedes)
                    {
                        Point Nuevo = new Point(cb.Location.X, cb.Location.Y);
                        Boolean Libre = CasillaLibre(Nuevo);
                        if (Libre)
                        {
                            Jugadores[i].RefreshLocation(Nuevo);
                            string Mensaje = "10/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y;
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                            server.Send(msg);
                            cola.Enqueue(turno);
                            turno = cola.Dequeue();
                            turno_lbl.Text = "Turno de: " + turno;
                            turno_lbl.ForeColor = Color.Black;
                            dado = 0;
                            Dado_btn.Enabled = false;
                            room = false;
                        }
                    }
                }
            }

        }
        private Boolean Distancia(Point Casilla, Point Localizacion, int dado)
        {

            float distY = Math.Abs(Casilla.Y - Localizacion.Y);
            float distX = Math.Abs(Casilla.X - Localizacion.X);

            float dist = distY + distX;

            if (dist == dado * 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private Boolean DistanciaRoom(Point Casilla, Point Localizacion, int dado)
        {

            int distY = Math.Abs(Casilla.Y - Localizacion.Y);
            int distX = Math.Abs(Casilla.X - Localizacion.X);

            int dist = distY + distX + 50;

            if (dist <= dado * 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void room1_Click(object sender, EventArgs e)
        {

            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(175, 125), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(125, 125);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(75, 125);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(25, 125);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(125, 75);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(75, 75);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(25, 75);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 15;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(175, 125));
                        room = true;
                    }
                }
            }

        }
        private void room2_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(325, 125), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(375, 125);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(425, 125);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(475, 125);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(375, 75);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(425, 75);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(475, 75);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 16;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(325, 125));
                        room = true;
                    }
                }
            }
        }
        private void room7_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(225, 225), Jugadores[i].GetPoint(), dado);
                Boolean TambienPuedes = DistanciaRoom(new Point(275, 225), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(225, 275);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(225, 325);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(225, 375);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(275, 275);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(275, 325);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(275, 375);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 17;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(225, 225));
                        room = true;
                    }
                }
                else if (TambienPuedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(275, 275);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(225, 325);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(225, 375);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(275, 275);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(275, 325);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(275, 375);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }
                            
                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 17;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(275, 275));
                        room = true;
                    }
                }
            }
        }
        private void room6_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(375, 425), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(375, 375);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(425, 375);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(475, 375);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(375, 325);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(425, 325);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(475, 325);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 18;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(375, 425));
                        room = true;
                    }
                }
            }
        }
        private void room5_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(325, 525), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(375, 525);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(425, 525);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(475, 525);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(375, 475);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(425, 475);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(475, 475);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 14;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(325, 525));
                        room = true;
                    }
                }
            }
        }
        private void room4_Click(object sender, EventArgs e)
        {

            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(175, 525), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(125, 525);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(75, 525);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(25, 525);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(125, 475);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(75, 475);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(25, 475);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 19;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(175, 525));
                        room = true;
                    }
                }
            }
        }
        private void room3_Click(object sender, EventArgs e)
        {

            if (turno == sesion)
            {
                int i = DameJugador(sesion);
                Boolean Puedes = DistanciaRoom(new Point(125, 425), Jugadores[i].GetPoint(), dado);
                if (Puedes)
                {
                    if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                    {
                        MessageBox.Show("Por favor, selecciona  tu acusación antes de entrar a la sala.");
                    }
                    else
                    {
                        Point Nuevo = new Point(125, 375);
                        Boolean Libre = CasillaLibre(Nuevo);
                        Jugadores[i].RefreshLocation(Nuevo);

                        if (!Libre)
                        {
                            Nuevo = new Point(75, 375);
                            Libre = CasillaLibre(Nuevo);
                            if (Libre)
                                Jugadores[i].SetPictureBox(Nuevo);
                            else
                            {
                                Nuevo = new Point(25, 375);
                                Libre = CasillaLibre(Nuevo);
                                if (Libre)
                                    Jugadores[i].SetPictureBox(Nuevo);
                                else
                                {
                                    Nuevo = new Point(125, 325);
                                    Libre = CasillaLibre(Nuevo);
                                    if (Libre)
                                        Jugadores[i].SetPictureBox(Nuevo);
                                    else
                                    {
                                        Nuevo = new Point(75, 325);
                                        Libre = CasillaLibre(Nuevo);
                                        if (Libre)
                                            Jugadores[i].SetPictureBox(Nuevo);
                                        else
                                        {
                                            Nuevo = new Point(25, 325);
                                            Libre = CasillaLibre(Nuevo);
                                            if (Libre)
                                                Jugadores[i].SetPictureBox(Nuevo);
                                        }
                                    }
                                }
                            }

                        }
                        int asesino = listBox1.SelectedIndex;
                        int arma = listBox2.SelectedIndex + 6;
                        int lugar = 20;
                        string Mensaje = "11/" + IDp + "/" + sesion + "/" + Nuevo.X + "/" + Nuevo.Y + "/" + asesino + "/" + arma + "/" + lugar;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                        server.Send(msg);
                        cola.Enqueue(turno);
                        turno = cola.Dequeue();
                        turno_lbl.Text = "Turno de: " + turno;
                        turno_lbl.ForeColor = Color.Black;
                        dado = 0;
                        Dado_btn.Enabled = false;
                        Jugadores[i].SetPosicion(new Point(125, 425));
                        room = true;
                    }
                }
            }
        }
        private void aza_button_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/aza/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        private void guillem_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/guillem/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        private void ismael_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/ismael/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        private void itziar_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/itziar/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        private void pedro_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/pedro/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        private void victor_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                string mensaje = "9/victor/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                escogido = true;

            }
        }
        public void CartasGanadoras(string[] cartas)
        {
            int i = 2;
            while (i <= 4)
            {
                Carta Carta = new Carta(Convert.ToInt32(cartas[i]));
                DelegadoParaCartas delegado = new DelegadoParaCartas(PonCartaGanadora);
                Invoke(delegado, new object[] { Carta });
                i++;
            }
        }
        private void PonCartaGanadora(Carta carta)
        {
            Ganadoras.Add(carta);
        }
        public void CartasPersonales(string[] cartas)
        {
            int i = 0;
            while (i < Convert.ToInt32(cartas[2]))
            {
                Carta Carta = new Carta(Convert.ToInt32(cartas[i + 3]));
                DelegadoParaCartas delegado = new DelegadoParaCartas(PonMiCarta);
                Invoke(delegado, new object[] { Carta });
                i++;
            }
        }
        private void PonMiCarta(Carta carta)
        {
            MisCartas.Add(carta);
        }
        public void CartasSobrantes(string[] cartas)
        {
            int i = 0;
            while (i < Convert.ToInt32(cartas[2]))
            {
                Carta Carta = new Carta(Convert.ToInt32(cartas[i + 3]));
                DelegadoParaCartas delegado = new DelegadoParaCartas(MuestraCarta);
                Invoke(delegado, new object[] { Carta });
                i++;
            }
            Carta carta = new Carta(Convert.ToInt32(cartas[i + 2]));
            DelegadoParaCartas Delegado = new DelegadoParaCartas(MuestraMisCartas);
            Invoke(Delegado, new object[] { carta });
            Delegado = new DelegadoParaCartas(AñadeCheckBoxes);
            Invoke(Delegado, new object[] { carta });
        }
        private void MuestraCarta(Carta carta)
        {

            PictureBox pic = carta.DamePic();
            pic.Width = 300;
            pic.Height = 500;
            pic.ClientSize = new Size(250, 350);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Location = new Point(Width / 3 + 100, Height / 4 - 50);
            Controls.Add(pic);
            pic.BringToFront();
            wait(3000);
            Controls.Remove(pic);
        }
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void MuestraMisCartas(Carta carta)
        {
            int i = 0;
            foreach (Carta p in MisCartas)
            {
                PictureBox pic = p.DamePic();
                pic.Width = 175;
                pic.Height = 200;
                pic.Tag = p.GetNum();
                pic.ClientSize = new Size(175, 215);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                if (i < 3)
                    pic.Location = new Point(575 + i * 185, 40);
                else
                    pic.Location = new Point(575 + (i - 3) * 185, 270);
                //pic.BringToFront();
                Controls.Add(pic);
                i++;
            }


            Cartas_lbl.Text = "Mis Cartas:";
            Cartas_lbl.AutoSize = true;
            Cartas_lbl.Font = new Font("Mongolian Baiti", 20);
            Cartas_lbl.Visible = true;
            Controls.Add(Cartas_lbl);
            Cartas_lbl.BringToFront();
            Sol_btn.Visible = true;
            label1.Visible = true;
        }
        private void AñadeCheckBoxes(Carta Carta)
        {
            Label Asesinos = new Label();
            Asesinos.Location = new Point(1185, 40);
            Asesinos.Text = "Asesinos";
            Asesinos.Font = new Font("Mongolian Baiti", 12,FontStyle.Bold);
            Asesinos.ForeColor = Color.Black;
            Asesinos.BackColor = Color.Transparent;
            Controls.Add(Asesinos);
            for (int i = 0; i < 6; i++)
            {
                Carta carta = new Carta(i);
                CheckBox dynamicCheckBox = new CheckBox();
                dynamicCheckBox.Location = new Point(1185, 65 + 25 * i);


                dynamicCheckBox.Width = 300;
                dynamicCheckBox.Height = 30;

                // Set background and foreground  

                dynamicCheckBox.ForeColor = Color.Black;
                dynamicCheckBox.BackColor = Color.Transparent;
                dynamicCheckBox.Text = carta.GetNombre();
                dynamicCheckBox.Name = carta.GetNombre();
                dynamicCheckBox.Font = new Font("Mongolian Baiti", 12, FontStyle.Bold);

                Controls.Add(dynamicCheckBox);
            }
            Label Armas = new Label();
            Armas.Location = new Point(1185, 240);
            Armas.Text = "Armas";
            Armas.Font = new Font("Mongolian Baiti", 12, FontStyle.Bold);
            Armas.ForeColor = Color.Black;
            Armas.BackColor = Color.Transparent;
            Controls.Add(Armas);

            for (int i = 6; i < 14; i++)
            {
                Carta carta = new Carta(i);
                CheckBox dynamicCheckBox = new CheckBox();
                dynamicCheckBox.Location = new Point(1185, 65 + 25 * (i + 2));

                dynamicCheckBox.Width = 300;
                dynamicCheckBox.Height = 30;

                // Set background and foreground  

                dynamicCheckBox.ForeColor = Color.Black;
                dynamicCheckBox.BackColor = Color.Transparent;
                dynamicCheckBox.Text = carta.GetNombre();
                dynamicCheckBox.Name = carta.GetNombre();
                dynamicCheckBox.Font = new Font("Mongolian Baiti", 12, FontStyle.Bold);

                Controls.Add(dynamicCheckBox);
            }
            Label Lugares = new Label();
            Lugares.Location = new Point(1185, 490);
            Lugares.Text = "Lugares";
            Lugares.Font = new Font("Mongolian Baiti", 12, FontStyle.Bold);
            Lugares.ForeColor = Color.Black;
            Lugares.BackColor = Color.Transparent;
            Controls.Add(Lugares);
            for (int i = 14; i < 21; i++)
            {
                Carta carta = new Carta(i);
                CheckBox dynamicCheckBox = new CheckBox();
                dynamicCheckBox.Location = new Point(1185, 65 + 25 * (i + 4));

                dynamicCheckBox.Width = 300;
                dynamicCheckBox.Height = 30;

                // Set background and foreground  

                dynamicCheckBox.ForeColor = Color.Black;
                dynamicCheckBox.BackColor = Color.Transparent;
                dynamicCheckBox.Text = carta.GetNombre();
                dynamicCheckBox.Name = carta.GetNombre();
                dynamicCheckBox.Font = new Font("Mongolian Baiti", 12, FontStyle.Bold);

                Controls.Add(dynamicCheckBox);
            }
            listBox1.Visible = true;
            listBox2.Visible = true;
            Chat.Visible = true;
            MensajeBox.Visible = true;
            btn_chat.Visible = true;
            Dado_btn.Visible = true;
        }
        private void Dado_btn_Click(object sender, EventArgs e)
        {
            if (sesion == turno)
            {

                Random random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    dado = random.Next(1, 7);
                    switch (dado)
                    {
                        case 1:
                            Dado_btn.BackgroundImage = new Bitmap("d1.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                        case 2:
                            Dado_btn.BackgroundImage = new Bitmap("d2.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                        case 3:
                            Dado_btn.BackgroundImage = new Bitmap("d3.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                        case 4:
                            Dado_btn.BackgroundImage = new Bitmap("d4.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                        case 5:
                            Dado_btn.BackgroundImage = new Bitmap("d5.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                        case 6:
                            Dado_btn.BackgroundImage = new Bitmap("d6.png");
                            Dado_btn.BackgroundImageLayout = ImageLayout.Stretch;
                            break;
                    }
                    wait(300);
                }
                Dado_btn.Enabled = false;
            }
        }
        private int DameJugador(string nombre)
        {
            int i = 0;
            Boolean Encontrado = false;
            while (i < Jugadores.Count && !Encontrado)
            {
                if (Jugadores[i].GetNombre() == nombre)
                {
                    Encontrado = true;
                }
                else
                    i++;
            }
            if (Encontrado)
                return i;
            else
                return -1;
        }
        public void DameMovimiento(string[] trozos)
        {

            Delegado delegado = new Delegado(MoverFicha);
            Invoke(delegado, new object[] { trozos });
        }
        public void MoverFicha(string[] trozos)
        {
            Point Nuevo = new Point(Convert.ToInt32(trozos[3]), Convert.ToInt32(trozos[4]));
            int i = DameJugador(trozos[2]);
            Jugadores[i].RefreshLocation(Nuevo);
            cola.Enqueue(turno);
            turno = cola.Dequeue();
            if (turno == sesion)
            {
                dado = 0;
                Dado_btn.Enabled = true;
                turno_lbl.Text = "Tu turno!";
                turno_lbl.ForeColor = Color.Green;
                SoundPlayer simpleSound = new SoundPlayer(@"sonido.wav");
                simpleSound.Play();
            }
            else
            {
                turno_lbl.ForeColor = Color.Black;
                turno_lbl.Text = "Turno de: " + turno;
            }

        }
        private Boolean CasillaLibre(Point Punto)
        {
            Boolean Libre = true;
            int i = 0;
            while (i < Jugadores.Count && Libre)
            {
                if (Jugadores[i].GetPoint() == Punto)
                    Libre = false;
                else
                    i++;
            }
            return Libre;
        }
        public void DameAcusacion(string[] trozos)
        {
            Delegado delegado = new Delegado(Acusar);
            Invoke(delegado, new object[] { trozos });
        }
        public void Acusar(string[] trozos)
        {
            string nombre = trozos[2];
            Carta asesino = new Carta(Convert.ToInt32(trozos[5]));
            Carta arma = new Carta(Convert.ToInt32(trozos[6]));
            Carta lugar = new Carta(Convert.ToInt32(trozos[7]));
            string mensaje = nombre + " ha hecho la siguiente acusación: Asesino: " + asesino.GetNombre() + " Arma: " + arma.GetNombre() + " Lugar: " + lugar.GetNombre();
            EscribeMensaje(mensaje);
            Chat.BackColor = Color.Red;
            Boolean Hay = HayCartas(asesino, arma, lugar);
            if (!Hay)
            {
                string Mensaje = "12/" + IDp + "/" + sesion;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                server.Send(msg);
            }
            else
            {
                Acusacion Form = new Acusacion();
                List<Carta> cartas1 = DameLista(asesino, arma, lugar);
                Form.GetLista(cartas1);
                Form.ShowDialog();
                int numero = Form.GiveNum();
                string Mensaje = "13/" + IDp + "/" + sesion + "/" + numero;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                server.Send(msg);
            }
        }
        private Boolean HayCartas(Carta asesino, Carta arma, Carta lugar)
        {
            int i = 0;
            Boolean encontrado = false;
            while (i < MisCartas.Count && !encontrado)
            {
                if (MisCartas[i].GetNombre() == asesino.GetNombre() || MisCartas[i].GetNombre() == arma.GetNombre() || MisCartas[i].GetNombre() == lugar.GetNombre())
                    encontrado = true;
                else
                    i++;
            }
            return encontrado;
        }
        private List<Carta> DameLista(Carta asesino, Carta arma, Carta lugar)
        {
            List<Carta> cartas1 = new List<Carta>();
            int i = 0;
            while (i < MisCartas.Count)
            {
                if (MisCartas[i].GetNombre() == asesino.GetNombre() || MisCartas[i].GetNombre() == arma.GetNombre() || MisCartas[i].GetNombre() == lugar.GetNombre())
                {
                    cartas1.Add(MisCartas[i]);
                }

                i++;
            }
            return cartas1;
        }
        public void acabaAcusacion(string letra)
        {
            DelegadoParaEscribirMensaje delegado = new DelegadoParaEscribirMensaje(AcabaAcuascion);
            Invoke(delegado, new object[] { letra });
        }
        public void AcabaAcuascion(string letra)
        {
            Chat.BackColor = Color.Snow;
        }
        public void Partida_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!escogido)
                e.Cancel = true;
            else
            {
                string mensaje = "14/" + IDp + "/" + sesion;
                foreach (Carta p in MisCartas)
                {
                    mensaje = mensaje + "/" + p.GetNum();
                }
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                if (cola.Count > 1)
                {
                    if (turno == sesion)
                    {
                        turno = cola.Dequeue();
                    }
                    else
                    {
                        int i = DameJugador(sesion);
                        Jugadores.RemoveAt(i);
                    }
                }
            }
        }
        public void MuestraAcusacion(string[] trozos)
        {
            int j = 1;
            while (2 * j < trozos.Count())
            {
                if (Convert.ToInt32(trozos[2 * j + 1]) == -1)
                {
                    j++;
                }
                else
                {
                    Carta carta = new Carta(Convert.ToInt32(trozos[2 * j + 1]));
                    Label label = new Label();
                    string texto = trozos[2*j] + " te ha mostrado esta carta:";
                    label.Text = texto;
                    label.Location = new Point(Width / 3-25, Height / 4 - 100);
                    label.Font = new Font("Mongolian Baiti", 20, FontStyle.Bold);
                    label.ForeColor = Color.Black;
                    label.BackColor = Color.Transparent;
                    label.AutoSize = true;
           
                 
                    Controls.Add(label);
                    label.BringToFront();
                    MuestraCarta(carta);
                    Controls.Remove(label);
                    break;
                }
            }
        }
        public void muestraAcusacion(string[] trozos)
        {
            Delegado delegado = new Delegado(MuestraAcusacion);
            Invoke(delegado, new object[] { trozos });
        }
        private void Sol_btn_Click(object sender, EventArgs e)
        {
            if (turno == sesion)
            {
                DialogResult dialogResult = MessageBox.Show("Advertencia: Seguro que quieres solucionar? No habrá vuelta atrás", "Aviso", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Solucionar Form = new Solucionar();
                    Form.ShowDialog();
                    int[] solucion = Form.DarDatos();
                    if(solucion[0]==Ganadoras[0].GetNum() && solucion[1]== Ganadoras[1].GetNum() && solucion[2] == Ganadoras[2].GetNum())
                    {
                        timer1.Stop();
                        //Tu ganas
                        string mensaje = "15/" + IDp + "/" + sesion+"/"+tiempo;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                        MessageBox.Show("Enhorabuena, tu combinación es correcta!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Lo siento, tu cominación no es correcta, buena suerte la próxima vez");
                        Close();
                    }
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
  
        }
        public void SalirPartida(string[] trozos)
        {
            Delegado delegado = new Delegado(SacarDePartida);
            Invoke(delegado, new object[] { trozos });
        }
        public void SacarDePartida(string[] trozos)
        {
            string nombre = trozos[2];
            if (nombre == turno)
            {
                turno = cola.Dequeue();
                turno_lbl.Text="Turno de: " + turno;
                turno_lbl.ForeColor = Color.Black;
            }
            else
            {
                int i = DameJugador(nombre);
                if (i != -1)
                {
                    Jugadores.RemoveAt(i);
                    int j = cola.Count;
                    for (int p = 0; p < j; p++)
                    {
                        string Jugador = cola.Dequeue();
                        if (Jugador != nombre)
                            cola.Enqueue(Jugador);
                    }
                }
            }
            if (cola.Count < 1)
            {
                //Este tio gana la partida porque es el ultimo.
                timer1.Stop();
                string mensaje = "15/" + IDp + "/" + sesion + "/" + tiempo;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("Vaya, te has quedado solo, parece que eres el ganador ");
                Close();
            }
            else
            {
                Label label = new Label();
                string texto = nombre + " se ha marchado, estas son sus cartas:";
                label.Text =texto;
                label.Location =  new Point(Width / 3-25  , Height / 4 - 100);
                label.Font = new Font("Mongolian Baiti", 20,FontStyle.Bold);
                label.ForeColor = Color.Black;
                label.AutoSize = true;
                label.BackColor = Color.Transparent;
                Controls.Add(label);
                label.BringToFront();
                for (int i=0; i < Convert.ToInt32(trozos[3]); i++)
                {
                    Carta carta = new Carta(Convert.ToInt32(trozos[i + 4]));
                    MuestraCarta(carta);
                }
                Controls.Remove(label);
            }
        }
        public void LlegaGanador(string[] trozos)
        {
            MessageBox.Show(trozos[2] + " ha acertado la combinación ganadora:\nAsesin@: " + Ganadoras[0].GetNombre() + "\nArma:  " + Ganadoras[1].GetNombre() + " \nLugar: " + Ganadoras[2].GetNombre());
            DelegadoParaEscribirMensaje delegado = new DelegadoParaEscribirMensaje(Cerrar);
            Invoke(delegado, new object[] { "a" });
        }
        public void Cerrar(string letra)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempo++;
        }
    }
}

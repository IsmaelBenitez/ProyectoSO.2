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
using System.Threading;
using System.Security.Cryptography;

namespace cliente
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread Atender;
        List<Partida> formularios = new List<Partida>();
        List<int> IDs = new List<int>();

        delegate void DelegadoParaEscribir();
        delegate void DelegadoParaDataGridView(string[] texto);
        delegate void DelegadoParaDarMensaje(string mensaje, int id);
        string[] Invitados=new string[6];
        int invitados;
        string sesion;
        string[] orden = new string[6];
        string key = "mykey";
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Conectar_Click(object sender, EventArgs e)
        {
            /*
             Boton conectar:   Se iniciará la conexión con el servidor y  apareceran los objetos para iniciar sesión 
            y se iniciará el thread de atender servidor
             */
            //Establecemos conexión con el servidor
            IPAddress direc = IPAddress.Parse("147.83.117.22");
            IPEndPoint ipep = new IPEndPoint(direc, 50064);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket

                label1.Visible = true;
                label2.Visible = true;
                nombre1Text.Visible = true;
                Contra1Text.Visible = true;
                btn_Iniciar_Sesion.Visible = true;
                btn_Iniciar_Sesion.Enabled = false;
                btn_Registrarse.Visible = true;
                btn_Conectar.Enabled = false;
                btn_Desconectar.Enabled = true;
            }
            catch (SocketException ex)
            {
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }

            ThreadStart ts = delegate { AtenderServidor(); };
            Atender = new Thread(ts);
            Atender.Start();

        }

        private void btn_Desconectar_Click(object sender, EventArgs e)
        {
            /*
             Boton desconectar:Se envia al servidor el mensaje de desconexión, Desaparecen todos los objetos de inicio de sesion, registrarse o consultas.
            Se elimina la conexion a socket y se aborta el thread del servidor
            Se reinicia la lista de invitados
             Mensaje a enviar : 0/
             */
            try
            {
                errorProvider1.SetError(btn_Desconectar, string.Empty);
                //Enviamos mensaje de desconexión
                string Mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                server.Send(msg);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                Atender.Abort();
                btn_Conectar.Enabled = true;
                //Desaparecen los datos de iniciar sesión
                label1.Visible = false;
                label2.Visible = false;
                nombre1Text.Visible = false;
                Contra1Text.Visible = false;
                btn_Iniciar_Sesion.Visible = false;
                btn_Registrarse.Visible = false;
                btn_Desconectar.Enabled = false;
                nombre1Text.Text = string.Empty;
                Contra1Text.Text = string.Empty;

                //desaparecen los datos de hacer consultas
                label6.Visible = false;
                porcentaje.Visible = false;
                Favorito.Visible = false;
                ganador.Visible = false;
                btn_Enviar.Visible = false;
                textBox1.Visible = false;
                textBox1.Text = string.Empty;
                btn_Enviar.Enabled = false;
                Grid.Visible = false;
                btn_Invitar.Visible = false;
                label7.Visible = false;
                button1.Visible = false;
                button2.Visible = false;




                //Desaparece el registrarse
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                Nombre2Text.Visible = false;
                Contra2Text.Visible = false;
                Contra3Text.Visible = false;
                btn_Registrarse2.Visible = false;
                Nombre2Text.Text = string.Empty;
                Contra2Text.Text = string.Empty;
                Contra3Text.Text = string.Empty;

                VaciarInvitados();

            }
            catch
            {
                errorProvider1.SetError(btn_Desconectar, "No hay ninguna conexión establecida");
            }
        }

        private void nombre1Text_TextChanged(object sender, EventArgs e)
        {
            /*
             * Funcion para deshabilitar el botón en caso de que alguno de los datos no este lleno
             */
            if (Text != string.Empty && Contra1Text.Text != string.Empty)
                btn_Iniciar_Sesion.Enabled = true;
            else
                btn_Iniciar_Sesion.Enabled = false;

        }

        private void Contra1Text_TextChanged(object sender, EventArgs e)
        {
            /*
             * Funcion para deshabilitar el botón en caso de que alguno de los datos no este lleno
             */
            if (Text != string.Empty && nombre1Text.Text != string.Empty)
                btn_Iniciar_Sesion.Enabled = true;
            else
                btn_Iniciar_Sesion.Enabled = false;
        }

        private void btn_Iniciar_Sesion_Click(object sender, EventArgs e)
        {
            /*
             * Boton Iniciar sesion: Se comprueba que todos los datos que esten en orden y se envia la petición de iniciar sesión al servidor
             * Mensaje: 1/NombreUsuario/Contraseña
             */
            if (nombre1Text.Text == string.Empty || Contra1Text.Text == string.Empty)
            {
                if (nombre1Text.Text == string.Empty)
                    errorProvider1.SetError(nombre1Text, "Por favor ingrese todos los datos");
                if (Contra1Text.Text == string.Empty)
                    errorProvider1.SetError(Contra1Text, "Por favor ingrese todos los datos");
            }
            else
            {
                //Limpiamos el simbolo de error
                errorProvider1.SetError(nombre1Text, string.Empty);
                errorProvider1.SetError(Contra1Text, string.Empty);
                try
                {

                    string contraseña = Encriptar(Contra1Text.Text);
                    //Generamos el mensaje de petición de iniciar sesión
                    string Mensaje = "1/" + nombre1Text.Text + "/" + contraseña;

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                    server.Send(msg);                    
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Error de rebasamiento");
                    return;
                }
            }
        }

        private void btn_Registrarse_Click(object sender, EventArgs e)
        {
            /*
             * Se habilitan los objetas necesarios para registrar una nueva cuenta
             */
            //Aparecen los datos para registrarse

            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            Nombre2Text.Visible = true;
            Contra2Text.Visible = true;
            Contra3Text.Visible = true;
            btn_Registrarse2.Visible = true;

            //Desaparecen los datos de iniciar sesión
            label1.Visible = false;
            label2.Visible = false;
            nombre1Text.Visible = false;
            Contra1Text.Visible = false;
            btn_Iniciar_Sesion.Visible = false;
            btn_Registrarse.Visible = false;
            nombre1Text.Text = string.Empty;
            Contra1Text.Text = string.Empty;


        }

        private void btn_Registrarse2_Click(object sender, EventArgs e)
        {
            /* Boton Registrarse2: Comprueba que los datos esten en orden y envia al servidor una petición de crear un nuevo  ususario
             * Mensaje: 2/NombreUsuario/Cotraseña
             */
            if (Nombre2Text.Text == string.Empty || Contra2Text.Text == string.Empty || Contra3Text.Text == string.Empty)
            {
                if (Nombre2Text.Text == string.Empty)
                    errorProvider1.SetError(Nombre2Text, "Por favor introduce todos los datos");
                if (Contra2Text.Text == string.Empty)
                    errorProvider1.SetError(Contra2Text, "Por favor introduce todos los datos");
                if (Contra3Text.Text == string.Empty)
                    errorProvider1.SetError(Contra3Text, "Por favor introduce todos los datos");
            }
            else if (Contra2Text.Text != Contra3Text.Text)
            {
                errorProvider1.SetError(Contra2Text, "Por favor, las contraseñas deben ser iguales");
                errorProvider1.SetError(Contra3Text, "Por favor, las contraseñas deben ser iguales");
            }
            else
            {
                errorProvider1.SetError(Nombre2Text, string.Empty);
                errorProvider1.SetError(Contra2Text, string.Empty);
                errorProvider1.SetError(Contra3Text, string.Empty);
                try
                {
                    string contraseña = Encriptar(Contra2Text.Text);

                    //Generamos el mensaje de petición de iniciar sesión
                    string Mensaje = "2/" + Nombre2Text.Text + "/" + contraseña;

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                    server.Send(msg);

                   
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Error de rebasamiento");
                    return;
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
            }
        }

        private void btn_Enviar_Click(object sender, EventArgs e)
        {
            /* Boton Enviar:  Envia al servidor la consulta que el usuario haya decidido hacer
             * Mensaje : 3/Palabraclave  // 4/PalabraClave // 5/IdPartida
             */
            if (textBox1.Text != string.Empty)
            {
                try
                {
                    if (porcentaje.Checked)
                    {
                        string mensaje = "3/" + textBox1.Text;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }
                    else if (Favorito.Checked)
                    {
                        string mensaje = "4/" + textBox1.Text;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }
                    else if (ganador.Checked)
                    {
                        string mensaje = "5/" + textBox1.Text;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                    }
                    else
                        errorProvider1.SetError(btn_Enviar, "Debes seleccionar una de las opciones");
                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Problema de rebasamiento");
                }
            }
            else
                errorProvider1.SetError(textBox1, "Por favor, introduce los datos");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /* deshabilita el boton envar en el caso de estar vacio el text box correspondiente
             */
            if (Text != string.Empty)
                btn_Enviar.Enabled = true;
            else
                btn_Enviar.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Grid.ColumnCount = 1;
            Grid.RowHeadersVisible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Al cerrar el Form, se inicia el proceso de desconexión al servidor
             * Mensaje: 0/
             */
            string Mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
            if (server != null)
            {
                try
                {
                    server.Send(msg);
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    Atender.Abort();
                }
                catch (System.ObjectDisposedException)
                {
                
                }
                catch (System.Net.Sockets.SocketException)
                {
                    MessageBox.Show("Lo sentimos, el servidor ha fallado");
                    
                }

            }
        }
        private void AtenderServidor()
        {
            /* 
             * Función atender servidor
             * Esta función es la que se encargará de recibir los mensajes que el servidor envie al cliente.
             * Deberá interpretar y dstribuir los mensajes al subproceso al que esten dirigidos.
             */
            while (true)
            {
                try
                {
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    string mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    string[] trozos = mensaje.Split('/');
                
                    int codigo = Convert.ToInt32(trozos[0]);
                    switch (codigo)
                    {
                        case 1:
                            // Llega la respuesta a lapetición de iniciar sesión
                            if (trozos[1] == "OK")
                            {
                                
                                DelegadoParaEscribir delegado = new DelegadoParaEscribir(DesaparecerIniciarSesion);
                                this.Invoke(delegado);
                            }
                            else
                            {
                                MessageBox.Show("Usuario no encontrado");

                            }
                            break;
                        case 2:
                            //Llega la respuesta a la peticion de registrar unnuevo usuario
                            if (trozos[1] == "OK")
                            {
                                
                                DelegadoParaEscribir delegado = new DelegadoParaEscribir(DesaparecerRegistarse);
                                this.Invoke(delegado);
                            }
                            else
                            {
                                MessageBox.Show("Error: no se ha podido registrar al usuario");

                            }
                            break;
                        case 3:
                            //Llega la respuesta a la consulta sobre el porcentaje de victorias
                            if (trozos[1] == "E")
                                MessageBox.Show("Error en la búsqueda");
                            else
                                MessageBox.Show("El porcentaje de victorias de " + textBox1.Text + " es: " + trozos[1]);
                            break;
                        case 4:
                            //Llega la respuesta a la consulta sobre el avatar favorito
                            if (trozos[1] == "E")
                                MessageBox.Show("Error en la búsqueda");
                            else if (trozos[1] == "X")
                                MessageBox.Show("Este usuario no tiene registrada ninguna partida");
                            else
                                MessageBox.Show("El personaje favorito de " + textBox1.Text + " es: " + trozos[1]);
                            break;
                        case 5:
                            //Llega la respuesta a la consulta sobre el ganador de la partida
                            if (trozos[1] == "E")
                                MessageBox.Show("Error en la búsqueda");
                            else if (trozos[1] == "X")
                            {
                                MessageBox.Show("No se han obtenido datos en la consulta");
                            }
                            else
                                MessageBox.Show("El gandor de la partida con id: " + textBox1.Text + " fue: " + trozos[1]);
                            break;
                        case 6:
                            //Se actualiza la lista de conectados
                            DelegadoParaDataGridView delegado2 = new DelegadoParaDataGridView(PonerLista);
                            this.Invoke(delegado2, new object[] { trozos });


                            break;
                        case 7:
                            //Llega la invitacion a la partida
                            string host = trozos[1];
                            int Id = Convert.ToInt32(trozos[2]);
                            DialogResult dialogResult = MessageBox.Show(host + " te ha invitado a unirte a su partida. Aceptas?", "Invitación", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string Mensaje = "7/SI" + "/" + Id.ToString();

                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                                server.Send(msg);
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                string Mensaje = "7/NO" + "/" + Id.ToString();
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                                server.Send(msg);

                            }
                            break;
                        case 8:
                            //Llega el mensaje de iniciar la partida
                            int j = 0;
                            while (j < Convert.ToInt32(trozos[1]))
                            {
                                orden[j] = trozos[j + 2];
                                j++;
                            }

                            ThreadStart ts = delegate { IniciarPartida(trozos, sesion); };
                            Thread T = new Thread(ts);
                            T.Start();

                            break;
                        case 9:
                            //Llega un mensaje para el chat
                            int ID = Convert.ToInt32(trozos[1]);
                            int i = 0;
                            Boolean encontrado = false;
                            while (i < IDs.Count && !encontrado)
                            {
                                if (IDs[i] == ID)
                                    encontrado = true;
                                else
                                    i++;
                            }
                            if (encontrado)
                            {
                                formularios[i].RecibirMensaje(trozos[2]);

                            }
                            
                            break;
                        case 10:
                            //Llega un avatar
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].RecibirAvatar(trozos[2], trozos[3]);
                            }
                            break;
                        case 11:
                            //Llega la combinacion ganadora Asesino/Arma/Lugar
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].CartasGanadoras(trozos);
                            }

                            break;
                        case 12:
                            //Llegan tus cartas.
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].CartasPersonales(trozos);
                            }

                            break;
                        case 13:
                            //Llegan las cartas que sobran.
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].CartasSobrantes(trozos);
                            }
                            break;
                        case 14:
                            //Llega un movimimiento
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].DameMovimiento(trozos);
                            }
                            break;
                        case 15:
                            //Llega un movimiento + un acusasción
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].DameMovimiento(trozos);
                                formularios[i].DameAcusacion(trozos);
                            }
                            break;
                        case 16:
                            //Llega la respuesta a la acusación a los que no son el acusador
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                j = 1;  
                                while (2*j < trozos.Count())
                                {
                                    string texto;
                                    if (Convert.ToInt32(trozos[2 * j + 1]) == -1)
                                    {
                                        texto = trozos[2 * j] + " no enseña ninguna carta";
                                        formularios[i].RecibirMensaje(texto);
                                    }
                                    else
                                    {
                                        texto = trozos[2 * j] + " enseña una carta";
                                        formularios[i].RecibirMensaje(texto);
                                        break;
                                    }
                                    j++;
                                }
                                formularios[i].acabaAcusacion("a");
                            }
                            break;
                        case 17:
                            //Llega la respuesta de la acusación a quien hizo la acusación
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                j = 1;


                                while (2 * j < trozos.Count())
                                {
                                    string texto;
                                    if (Convert.ToInt32(trozos[2 * j + 1]) == -1)
                                    {
                                        texto = trozos[2 * j] + " no enseña ninguna carta";
                                        formularios[i].RecibirMensaje(texto);

                                    }
                                    else
                                    {
                                        Carta carta = new Carta(Convert.ToInt32(trozos[2 * j + 1]));
                                        texto = trozos[2 * j] + " enseña la carta: "+carta.GetNombre();
                                        formularios[i].RecibirMensaje(texto);
                                        formularios[i].muestraAcusacion(trozos);
                                        break;
                                    }

                                    j++;
                                }
                                formularios[i].acabaAcusacion("a");
                            }
                            break;
                        case 18:
                            // Llega la notificación de que alguien ha salido de la partida
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].SalirPartida(trozos);
                            }
                            break;
                        case 19:
                            // Llega la notificacion de que alguien  ha ganado
                            ID = Convert.ToInt32(trozos[1]);
                            i = EncontrarForm(ID);
                            if (i != -1)
                            {
                                formularios[i].LlegaGanador(trozos);
                            }
                            break;
                        case 20:
                            DelegadoParaEscribir delegado1 = new DelegadoParaEscribir(DesaparecerConsultas);
                            this.Invoke(delegado1);
                            break;


                    }
                }
                catch (System.FormatException)
                {
                    continue;
                }
   
            }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /* Al hacer doble clic en la lista de conectados, se añade/ se elimina, el usuario a la lista de invitados
             */
            try
            {


                if (invitados < 6)
                {
                    int j = e.RowIndex;
                    if (Grid.Rows[j].Cells[0].Style.BackColor == Color.Green)
                    {
                        Grid.Rows[j].Cells[0].Style.BackColor = Color.White;
                        EliminarInvitado(Grid[0, j].Value.ToString());
                    }
                    else
                    {
                        if(Grid[0, j].Value != null)
                        { 
                            if (Grid[0, j].Value.ToString() != sesion)
                            {
                                Invitados[invitados] = Grid[0, j].Value.ToString();
                                Grid.Rows[j].Cells[0].Style.BackColor = Color.Green;
                                invitados++;
                            }
                        }

                    }
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                
            }
           



        }
        private void EliminarInvitado(string nombre)
        {
            /*
             * Función que se encarga de eliminar a un usuario de la lista de invitados.
             * Parámetros: nombre (nombre del usuario a eliminar de la lista
             */
            int i = 0;
            while (i < invitados)
            {
                if (nombre == Invitados[i])
                {
                    Invitados[i] = null;
                    int j = i;
                    while (j < invitados - 1)
                    {
                        Invitados[j] = Invitados[j+1];
                        j = j + 1;
                    }
                    invitados--;
                }
                i = i + 1;
            }
        }

        private void btn_Invitar_Click(object sender, EventArgs e)
        {
            /*
             * Botón Invitar : Envia  al servidor los jugadores que han sido invitados
             * Mensaje 6/Jugadoresinvitado/...
             */
            if (invitados < 2)
                MessageBox.Show("Se necesitan mínimo tres participantes");
            else
            {
                string mensaje = "6/"+(invitados+1);

                int i = 0;
                while (i < invitados)
                {
                    mensaje = mensaje + "/" + Invitados[i];
                    i++;
                }
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                VaciarInvitados();
            }
            
        }
        private void VaciarInvitados()
        {
            /* Función para vaciar por completo la lista de invitados
             */
            for(int i = 0; i < invitados; i++)
            {
                Invitados[i] = null;
            }
            invitados = 0;
            int j = 0;
            int R = Grid.Rows.Count;
            while (j< R)
            {
                Grid.Rows[j].Cells[0].Style.BackColor = Color.White;
                j++;
            }
        }
        private void DesaparecerIniciarSesion()
        {
            /*
             * funión para asignarsela a un delegado y hacer que desaparezcan los objetod de iniciar sesión y aparezcan los de hacer conultas
             */
            sesion = nombre1Text.Text;
            //Desaparecen los datos de iniciar sesión
            label1.Visible = false;
            label2.Visible = false;
            nombre1Text.Visible = false;
            Contra1Text.Visible = false;
            btn_Iniciar_Sesion.Visible = false;
            btn_Registrarse.Visible = false;
            nombre1Text.Text = string.Empty;
            Contra1Text.Text = string.Empty;
            //Aparecen los datos de hacer consultas
            label6.Visible = true;
            porcentaje.Visible = true;
            Favorito.Visible = true;
            ganador.Visible = true;
            btn_Enviar.Visible = true;
            textBox1.Visible = true;
            Grid.Visible = true;
            btn_Invitar.Visible = true;
            label7.Visible = true;
            button1.Visible = true;
            button2.Visible = true;



        }
        private void DesaparecerRegistarse()
        {
            /* Función DesaparecerRegistrarse, para ser usado por un delegado
             * Esta función hará que desaparezcan los objetos para registrare y aparezcan los de haer consultas
             */
            sesion = Nombre2Text.Text;
            //Desaparece el registrarse
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            Nombre2Text.Visible = false;
            Contra2Text.Visible = false;
            Contra3Text.Visible = false;
            btn_Registrarse2.Visible = false;
            Nombre2Text.Text = string.Empty;
            Contra2Text.Text = string.Empty;
            Contra3Text.Text = string.Empty;

            //Aparecen los datos de hacer consultas
            label6.Visible = true;
            porcentaje.Visible = true;
            Favorito.Visible = true;
            ganador.Visible = true;
            btn_Enviar.Visible = true;
            textBox1.Visible = true;
            Grid.Visible = true;
            btn_Invitar.Visible = true;
            label7.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }
        public void DesaparecerConsultas()
        {
            /* Funcion DesaparecerConsultas para ser usado por un delegado
             * Hara  las funciones de desconectarse.
             */
             string Mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
            server.Send(msg);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            Atender.Abort();
            btn_Conectar.Enabled = true;
            //Desaparecen los datos de iniciar sesión
            label1.Visible = false;
            label2.Visible = false;
            nombre1Text.Visible = false;
            Contra1Text.Visible = false;
            btn_Iniciar_Sesion.Visible = false;
            btn_Registrarse.Visible = false;
            btn_Desconectar.Enabled = false;
            nombre1Text.Text = string.Empty;
            Contra1Text.Text = string.Empty;

            //desaparecen los datos de hacer consultas
            label6.Visible = false;
            porcentaje.Visible = false;
            Favorito.Visible = false;
            ganador.Visible = false;
            btn_Enviar.Visible = false;
            textBox1.Visible = false;
            textBox1.Text = string.Empty;
            btn_Enviar.Enabled = false;
            Grid.Visible = false;
            btn_Invitar.Visible = false;
            label7.Visible = false;
            button1.Visible = false;
            button2.Visible = false;




            //Desaparece el registrarse
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            Nombre2Text.Visible = false;
            Contra2Text.Visible = false;
            Contra3Text.Visible = false;
            btn_Registrarse2.Visible = false;
            Nombre2Text.Text = string.Empty;
            Contra2Text.Text = string.Empty;
            Contra3Text.Text = string.Empty;

            VaciarInvitados();
        }
        private void PonerLista(string[] trozos)
        {
            /* 
             * Función PonerFilas
             * función que se usa para llenar las filas del dataGridView cuando llega una actualización de la lista de conectados
             * Parámetros: string[] trozos conteniendo los nombres de los usuarios conectados
             */
            int nfilas = Convert.ToInt32(trozos[1]);

            Grid.Rows.Clear();

            for (int i = 1; i <= nfilas; i++)
            {
                string usuario = trozos[i + 1];
                Grid.Rows.Add(usuario);
            }
        }
       
        private void IniciarPartida(string[] trozos,string sesion)
        {
            /* función Iniciarpartida:
             * Función que se ejecuta cuadno llega el mensaje de iniciar una partida
             * Parámetros: string [] trozos conteniendo los nombre que participarán en la partida
             *             string sesion: indica el nombre del jugador conecto a dicho cliente
             */
            int Id = Convert.ToInt32(trozos[Convert.ToInt32(trozos[1]) + 2]);
            Partida f = new Partida(server,trozos,sesion);
            formularios.Add(f);
            IDs.Add(Id);
            f.ShowDialog();
        }
        public int EncontrarForm(int ID)
        {
            /* Función EncontrarForm:
             * Se usa para encontrar el Form al cual se deben redirigir los mensajes que llegan
             * parámetro: int ID, inicando el identificador de partida del form a buscar
             * return : la posición en la listade formularios del Form buscado.
             */
            int i = 0;
            Boolean encontrado = false;
            while (i < IDs.Count && !encontrado)
            {
                if (IDs[i] == ID)
                    encontrado = true;
                else
                    i++;
            }
            if (encontrado)
            {
                return i;

            }
            else
            {
                return -1;
            }
        }
        public string Encriptar(string texto)
        {
            /*Función encriptar:
             * Usado para encriptar contraseñas.
             * Parámetro: string texto :contraseña sin encriptar
             * Return: contraseña ecriptada
             */
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado,
            0, ArrayResultado.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Boton 1 :
             * Nos llevará al tutorial de partida
             */
            video Form = new video();
            Form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* Boton 2 : 
             * Boton para darse de baja en la base de datos
             * Mensaje: 16/nombre
             */
            DialogResult dialogResult = MessageBox.Show(" Tu cuenta se eliminará de la base de datos y perderas el acceso, ¿Quieres continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                string Mensaje = "16/"+sesion;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(Mensaje);
                server.Send(msg);
            }
        }
    }
}

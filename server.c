#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include<time.h>
#include<stdio.h>
#include<stdlib.h>

//Estructuras para la lista de conectados
typedef struct{
	char nombre[60];
	int socket;
	char avatar[10];
}Conectado;

typedef struct{
	Conectado conectados[100];
	int num;
	int avatares;
}ListaConectados;
typedef struct{
	Conectado Conectado;
	int carta;
}Acusacion;
typedef struct{
	Acusacion Acusados[100];
	int num;
	int socket;
}ListaAcusaciones;
typedef struct{
	int ocupado;
	int invitaciones;
	int BBDD;
	ListaConectados Jugadores;
	ListaAcusaciones Acusaciones;
}Partida;
typedef Partida TPartidas[100];
//Variables Globales.
MYSQL *conn; // Connector con el serivdor de MYSQL
ListaConectados Lista; // Lista de conectados
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;//Estructura para la implementación de exclusin mutua
TPartidas tabla;
int Baraja[20];
//Funciones para inicializar
void inicializar(TPartidas tabla){
	//Inicializa la tabla de partidas
	int i;
	for(i=0; i<100; i++)
		tabla[i].ocupado = 0;
}
void InciarAcusacion(TPartidas tabla,int Id, int socket){
	//Inicia la lista de una acusacion
	tabla[Id].Acusaciones.num=0;
	tabla[Id].Acusaciones.socket=socket;
}
void InicializarBaraja(int baraja[20]){
	//Inicializa la baraja 
	int i=0;
	while (i<21){
		baraja[i]=i;
		i++;
	}
}
//Estructuras y funciones para la baraja
int RepartirCartasGanadoras(Partida *partida, int cont,int Id){
	/*Elige aleatoriamente la cmbinacin ganadora de dicha partida
	Parametros: Partida partida,
	int cont: contador ded las cartas que quedan en la baraja
	Int Id: Id de la partida en cuestion
	*/
	srand(time(0));
	int i = rand() %6; // Personas
	int j = rand() %8; //Armas
	int k = rand() %7; // Lugar
	j=j+6;
	k=k+14;
	printf("i=%d,j=%d,k=%d",i,j,k);
	char Combinacion[100];
	sprintf(Combinacion,"11/%d/%d/%d/%d",Id,i,j,k);
	printf("Combinacion ganadora : %s\n",Combinacion);
	for (int p=0; p<partida->Jugadores.num;p++){
		write(partida->Jugadores.conectados[p].socket,Combinacion,strlen(Combinacion));
	}
	cont = SacarCartas(Baraja,i,cont);
	cont = SacarCartas(Baraja,j-1,cont);
	cont = SacarCartas(Baraja,k-2,cont);

}
int RepartirCartasPersonales(Partida *partida, int cont, int Id){
	/* Reparte las cartas que le tocan a cada uno en base a un calculo  de cuantas deben ser
	Parametros:  Partida partida
	int cont: Numeros de cartas restantes en la Baraja
	int Id: Id de la partida en cuestion
	*/
	int jug = partida->Jugadores.num;
	div_t resultadoDeLaDivision=div(18,jug);
	int cartas = resultadoDeLaDivision.quot; 
	printf("Numero de cartas:%d \n",cartas);
	
	for(int p=0;p<jug;p++){
		char mano[100];
		bzero(mano, 100);
		int i;
		for (int n=0;n<cartas;n++){
			i = rand() %(cont);
			printf("i=%d\n",i);
			sprintf(mano,"%s/%d",mano,Baraja[i]);
			printf("mensaje: %s\n",mano);
			printf("Carta: %d\n",Baraja[i]);
			cont = SacarCartas(Baraja,i,cont);
		}
		char mensaje[200];
		bzero(mensaje,200);
		sprintf(mensaje,"12/%d/%d%s",Id,cartas,mano);
		printf("Cartas de un jugador: %s\n",mensaje);
		write(partida->Jugadores.conectados[p].socket,mensaje,strlen(mensaje));
	}
}
int RepartirCartasSobrantes(Partida *partida, int cont, int Id){
	/* Envia las cartas que han sobado tras repartir todo
	Parametros : Partida partida
	int cont: cartas que quedan en la baraja
	*/
	int n=0;
	int sum=0;
	char sobrantes[200];
	bzero(sobrantes,200);
	while (Baraja[n]!=-1){
		sprintf(sobrantes,"%s/%d",sobrantes,Baraja[n]);
		sum++;
		n++;
	}
	char sobran[100];
	
	sprintf(sobran,"13/%d/%d%s",Id,sum,sobrantes);
	printf("sobrantes: %s\n",sobran);
	for (int f=0;f< partida->Jugadores.num;f++){
		write(partida->Jugadores.conectados[f].socket,sobran,strlen(sobran));
	}
	
}
int SacarCartas(int baraja[20],int i,int cont){
	/*Esta funcion se encarga de sacar la carta que ya ha sido repartida de la baraja, con tal de no volverla a RepartirCartasGanadoras
	Parametros:
	int baraja: la baraja de la que debe sacr la carta
	int i : posicion de la carta que debe Sacar
	int cont: contador de las cartas que quedan
	*/
	while (i<20){
		baraja[i]=baraja[i+1];
		i++;
	}
	baraja[20]=-1;
	
	cont -- ;
	return cont;
	
}

//Funciones para actuar sobre la lista de conectados
int AnadirConectado (ListaConectados *lista, char nombre[60], int socket){
	/* Añade un usuario a la lista de conectados
	Parametros
	ListaConectados lista: a donde se debe añadir
	char nombre: de la  persona a añadir
	init socket: de la persona a añadir
	*/
	if (lista->num == 100)
	{
		
		return -1;
		
	}
	else{
		
		strcpy(lista->conectados[lista->num].nombre,nombre);

		lista->conectados[lista->num].socket = socket;

		lista->num++;


		return 0;
	}

}
int DameSocket ( char nombre[20]){
	// Devuelve el socket o -1 si no estÃ¡ en la lista
	int i=0;
	int encontrado =0;
	while ((i< Lista.num) && !encontrado ){
		if (strcmp(Lista.conectados[i].nombre,nombre)==0)
			encontrado =1;
		if (!encontrado)
			i=i+1;
	}
	if (encontrado)
		return Lista.conectados[i].socket;
	else
		return -1;
	
}
int DamePos (ListaConectados *lista, char nombre[20]){
	// Devuelve la posicion en la lista o -1 si no estÃ¡ en la lista
	int i=0;
	int encontrado =0;
	while ((i< lista->num) && !encontrado ){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado =1;
		if (!encontrado)
			i=i+1;
	}
	if (encontrado)
		return i;
	else
		return -1;
}
int Elimina(ListaConectados *lista, char nombre[20]){
	//Devuelve el socket o -1 si no estÃ¡ en la lista
	// intenta eliminar de lista a usuario nombre
	int pos = DamePos (lista,nombre);
	if (pos==-1)
		return -1;
	else
	{
		int i;
		for (i=pos; i< lista->num-1;i++)
		{
			lista->conectados[i] = lista->conectados[i+1];
		}
		lista->num--;
		return 0;
		
	}
	
}
void DameConectados ( ListaConectados *lista, char *conectados[300]){
	//Pone en conectados los nombres de todos los conectados separados por /. Primero pone el num de conectados
	sprintf(conectados,"%d", lista->num);
	int i;
	for (i=0; i< lista->num; i++)
		sprintf(conectados, "%s/%s", conectados, lista->conectados[i].nombre);
}
void DameTodosSockets ( ListaConectados *lista, char nombres[80], char *sockets[200]){
	//Escribe una funciÃƒÂ³n que recibe un vector de caracteres con los nombres de jugadores
	//separados por comas y revuelve una cadena de caracteres con los sockets de cada uno
	//de estos jugadores, tambiÃƒÂ©n separados por comas.
	printf("vector nombres %s\n", nombres);
	char *p=strtok(nombres,",");
	int i=0;
	int n=lista->num;
	printf("%d\n",n);
	while (p!=NULL){
		while (i<n){
			if (strcmp(lista->conectados[i].nombre,p)==0)
			{
				printf("He entrado %s\n",lista->conectados[i].nombre);
				int valor=lista->conectados[i].socket;
				sprintf(sockets,"%s, %d", sockets, valor);
			}
			i=i+1;
		}
		i=0;
		p=strtok(NULL,",");
		
	}
}
void DameConectado(int socket,char *nombre[20]){
	/* Devuelve el nombre del conectado que tenga el socket que se da como parametro
	*/
	int i=0;
	int encontrado=0;
	while (i<Lista.num && !encontrado){
		printf("Estoy en el bucle\n");
		if(Lista.conectados[i].socket==socket){
			strcpy(nombre,Lista.conectados[i].nombre);
			printf("Este es el nombre que devuelvo para la lista %s\n",nombre);
			encontrado=1;
		}
		else
		   i=i+1;
	}
}
void AnadirAvatar(ListaConectados *lista, char sesion[20],char avatar[20]){
	/* Añade a la lista el avatar seleccionado (avatar) y por quien ha sido elegido(sesion)
	*/
	int i = DamePos (lista, sesion);
	printf("i=%d\n",i);
	strcpy(lista->conectados[i].avatar,avatar);
	lista->avatares++;
}
void EliminarTodos(ListaConectados *lista){
	//Elimina a todo el mundo de la lista
	while (lista->num>0){
		Elimina(lista,lista->conectados[0].nombre);
	}
}
//Funciones para enviar notificaciones
void EnviarLista (){
	//Envia en modo notificacion la lista de conectados
	char notificacion[300];
	char conectados[300];
	DameConectados (&Lista,conectados);
	printf("Este es el print de la lista de conectados %s\n",conectados);
	int j;
	sprintf(notificacion,"6/%s",conectados);
	printf("%s",notificacion);
	for (j=0; j<Lista.num; j++)
		write (Lista.conectados[j].socket,notificacion, strlen(notificacion));
}
void EnviarInvitacion(char nombre[20],int socket,int Id){
	//Envia una una invitacion a la persona con el nombre indicado 
	int i = DameSocket (nombre);
	char notificacion[200];
	int j=0;
	int encontrado1=0;
	while(j<Lista.num && !encontrado1){
		
		if (Lista.conectados[j].socket==socket)
			encontrado1=1;
		else
			j=j+1;
		
	}
	if (i!=-1 && encontrado1==1){
		sprintf(notificacion,"7/%s/%d",Lista.conectados[j].nombre,Id);
		write (i,notificacion, strlen(notificacion));
	}
	
}
void EmpezarPartida(int Id){
	/* Envia en modo notificacion que la partida ya esta lista para EmpezarPartida
	Parametro: int Id de la partida a EmpezarPartida
	*/
	int i=0;
	char notificacion[200];
	sprintf(notificacion,"8/%d",tabla[Id].Jugadores.num);
	while (i<tabla[Id].Jugadores.num){
		sprintf(notificacion,"%s/%s",notificacion,tabla[Id].Jugadores.conectados[i].nombre);
		printf("%s\n",tabla[Id].Jugadores.conectados[i].nombre);
		i=i+1;
	}
	sprintf(notificacion,"%s/%d",notificacion,Id);
	printf("%s\n",notificacion);
	i=0;
	while(i<tabla[Id].Jugadores.num){
		write (tabla[Id].Jugadores.conectados[i].socket,notificacion, strlen(notificacion));
		i=i+1;
	}
}
void EnviarMensaje(int Id,char nombre[20],char mensaje[100]){
	/* Envia en modo notificacion de la partida Id el mensaje proveniente del chat */
	char notificacion[200];
	sprintf(notificacion,"9/%d/%s: %s",Id,nombre,mensaje);
	int i=0;
	while(i<tabla[Id].Jugadores.num){
		write (tabla[Id].Jugadores.conectados[i].socket,notificacion, strlen(notificacion));
		i=i+1;
	}
}
void EnviarAvatar(int Id,char sesion[20],char avatar[20]){
	/* Envia en modo notificacion de la partida Id, el avatar seleccionado y por quien*/
	char notificacion[200];
	sprintf(notificacion,"10/%d/%s/%s",Id,sesion,avatar);
	printf("%s\n",notificacion);
	int i=0;
	while(i<tabla[Id].Jugadores.num){
		write (tabla[Id].Jugadores.conectados[i].socket,notificacion, strlen(notificacion));
		i=i+1;
	}
}
void EnviarMovimiento(int Id,char sesion[20],int x,int y,int socket){
	/* Envia en modo notificacio en la partida Id, las nuevas posiciones y i x y quien se ha movido*/
	char notificacion[200];
	sprintf(notificacion,"14/%d/%s/%d/%d",Id,sesion,x,y);
	printf("%s\n",notificacion);
	int i=0;
	while(i<tabla[Id].Jugadores.num){
		if(tabla[Id].Jugadores.conectados[i].socket!=socket){
			write (tabla[Id].Jugadores.conectados[i].socket,notificacion, strlen(notificacion));
		}
		i=i+1;
	}
	
}
void EnviarAcusacion(int Id,char sesion[20],int x,int y,int socket,int asesino,int arma,int lugar){
	// Envia en modo de notificación en la partida Id el movimiento realizado (x,y) y la acusacion realizada (asesino,arma,lugar)
	char notificacion[200];
	sprintf(notificacion,"15/%d/%s/%d/%d/%d/%d/%d",Id,sesion,x,y,asesino,arma,lugar);
	printf("%s\n",notificacion);
	int i=0;
	while(i<tabla[Id].Jugadores.num){
		if(tabla[Id].Jugadores.conectados[i].socket!=socket){
			write (tabla[Id].Jugadores.conectados[i].socket,notificacion, strlen(notificacion));
		}
		i=i+1;
	}
}
void ResponderAcusacion(ListaAcusaciones *lista,int Id){
	//Envia unicamente a quien habia hecho la acusacion la resolucin de la misma 
	char Notificacion[200];
	char Notificacion2[200];
	sprintf(Notificacion,"16/%d",Id);
	sprintf(Notificacion2,"17/%d",Id);
	int i=0;
	while (i<lista->num){
		sprintf(Notificacion,"%s/%s/%d",Notificacion,lista->Acusados[i].Conectado.nombre,lista->Acusados[i].carta);
		sprintf(Notificacion2,"%s/%s/%d",Notificacion2,lista->Acusados[i].Conectado.nombre,lista->Acusados[i].carta);
		i=i+1;
	}
	printf("Respuesta Acusacion:  %s\n",Notificacion);
	i=0;
	while(i<tabla[Id].Jugadores.num){
		if(lista->socket!=tabla[Id].Jugadores.conectados[i].socket){
			write (tabla[Id].Jugadores.conectados[i].socket,Notificacion, strlen(Notificacion));
		}
		else
		   write (tabla[Id].Jugadores.conectados[i].socket,Notificacion2, strlen(Notificacion2));
		i=i+1;
	}
}
void EnviarSalirPartida(int Id,char sesion[20],int socket,char cartas[200],int cont){
	//Envia en modo de notificación quien ha abandonado la partida y cuales eran sus cartas
	char Notificacion[200];
	sprintf(Notificacion,"18/%d/%s/%d%s",Id,sesion,cont,cartas);
	for(int i=0;i<tabla[Id].Jugadores.num;i++){
		if(tabla[Id].Jugadores.conectados[i].socket!=socket){
			write (tabla[Id].Jugadores.conectados[i].socket,Notificacion, strlen(Notificacion));
		}
	}
}
void EnviarGanador(int Id,char sesion[20],int socket){
	//Evia en modo notificacion de la partida quien ha sido el ganador de la misma
	char Notificacion[200];
	sprintf(Notificacion,"19/%d/%s",Id,sesion);
	for(int i=0;i<tabla[Id].Jugadores.num;i++){
		if(tabla[Id].Jugadores.conectados[i].socket!=socket){
			write (tabla[Id].Jugadores.conectados[i].socket,Notificacion, strlen(Notificacion));
		}
	}
}

//Funciones para trabajr sobre las listas de Acusaciones
void AnadirRespuestaAcusacion( ListaAcusaciones *lista, char nombre[60], int socket,int carta){
	//Acumula las respuesta que se van sucediendo tras iniciar una acusacion
	if (lista->num == 100)
	{		
		return -1;
	}
	else{
		strcpy(lista->Acusados[lista->num].Conectado.nombre,nombre);
		lista->Acusados[lista->num].Conectado.socket = socket;
		lista->Acusados[lista->num].carta=carta;
		lista->num++;
		return 0;
	}
	
}


//Thread para atender peticiones de los clientes
void *AtenderCliente(void *socket) {
	//Es el hilo que se encarga de recibir los mensajes provinientes de los clientes

	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	int sock_conn, ret;
	int *s;
	s=(int *) socket;
	sock_conn=*s;
	struct sockaddr_in serv_adr;
	//Inicializamos el Socket
	char peticion[512];
	char respuesta[512];

	int i =0;
	int terminar=0;
	char nombre[60];
	
	while(terminar==0){
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		peticion[ret]='\0';
		
		printf ("Peticion: %s\n",peticion);
		
		
		
		//Partimos el mensaje para saber que piden.
		char *p =strtok(peticion,"/");
		int codigo =atoi(p);
		printf("COdigo: %d\n",codigo);
		
		
		if (codigo==0){
			terminar=1;
		}
		else if (codigo==1){
			
			char contra[60];
			
			p=strtok(NULL,"/");
			strcpy(nombre,p);
			
			
			p=strtok(NULL,"/");
			strcpy(contra,p);
			
			
			i =EstaRegistrado( nombre,contra);
			
			
			
			if (i==1)
			{
				
				//Bloqueamos la parte en la que la lista debe ser modificada
				pthread_mutex_lock(&mutex);
				i = AnadirConectado(&Lista,nombre,sock_conn);
				pthread_mutex_unlock(&mutex);
				
				
				
				if(i==0){
					sprintf(respuesta,"1/%s","OK");
					write (sock_conn,respuesta, strlen(respuesta));
					EnviarLista ();
				}
				else{
					sprintf(respuesta,"1/%s","NO");
					
					write (sock_conn,respuesta, strlen(respuesta));
				}
			}
			else
			{
				sprintf(respuesta,"1/%s","NO");
				
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
			
		}
		else if (codigo==2){
			
			char contra[60];
			
			p=strtok(NULL,"/");
			strcpy(nombre,p);
			
			
			p=strtok(NULL,"/");
			strcpy(contra,p);
			
			i=Registrar(nombre, contra);
			
			if(i==1)
			{
				
				//Bloquemaos la parte deonde la lista está siendo modificada
				pthread_mutex_lock(&mutex);
				i = AnadirConectado(&Lista,nombre,sock_conn);
				pthread_mutex_unlock(&mutex);
				
				
				if(i==0)
				{
					sprintf(respuesta,"2/%s","OK");
					write (sock_conn,respuesta, strlen(respuesta));
					EnviarLista();
				}
				else
				{
					sprintf(respuesta,"2/%s","NO");
					
					write (sock_conn,respuesta, strlen(respuesta));
				}
			}
			else
			{
				sprintf(respuesta,"2/%s","NO");
				
				write (sock_conn,respuesta, strlen(respuesta));
			}
			
		}
		else if (codigo==3){
			char nombre[60];
			
			p=strtok(NULL,"/");
			strcpy(nombre,p);
			PorcentajeVictorias(nombre,respuesta);
			printf("%s\n",respuesta);
			if (strcmp(respuesta,"-1.00")==0)
			{
				sprintf(respuesta,"%s","E");
				printf("%s\n",respuesta);
				write (sock_conn,respuesta, strlen(respuesta));
			}
			else
				write (sock_conn,respuesta, strlen(respuesta));
			
		}
		else if (codigo==4){
			char nombre[60];
			
			p=strtok(NULL,"/");
			strcpy(nombre,p);
			JugadorFavorito(nombre,respuesta);
			write (sock_conn,respuesta, strlen(respuesta));
			
		}
		else if (codigo==5){
			char identificador[60];
			char nombre[60];
			p=strtok(NULL,"/");
			strcpy(identificador,p);
			GanadorPartida(identificador,nombre);
			write (sock_conn,nombre, strlen(nombre));
			
		}
		else if (codigo==6){
			int invitaciones=0;
			int puesta=0;
			int i=0;
			while (i<100 && !puesta){
				if(tabla[i].ocupado==0){
					puesta=1;
					tabla[i].ocupado=1;
				}
				else
				   i=i+1;
			}
			char  usuario[20];
			DameConectado(sock_conn,&usuario);
			pthread_mutex_lock(&mutex);
			AnadirConectado(&(tabla[i].Jugadores),usuario,sock_conn);
			pthread_mutex_unlock(&mutex);
			p=strtok(NULL,"/");
			//invitaciones=p
			tabla[i].invitaciones=atoi(p);
			p=strtok(NULL,"/");
			
			while (p!=NULL)
			{
				//EnviarInvitacion
				EnviarInvitacion(p,sock_conn,i);

				p=strtok(NULL,"/");
			}
			
		}
		else if (codigo==7){
			p=strtok(NULL,"/");
			char respuesta[20];
			strcpy(respuesta,p);
			p=strtok(NULL,"/");
			int Id=atoi(p);
			if (strcmp(respuesta,"SI")==0){
				
				char nombre[20];
				DameConectado(sock_conn,&nombre);
				
				pthread_mutex_lock(&mutex);
				AnadirConectado(&(tabla[Id].Jugadores),nombre,sock_conn);
				pthread_mutex_unlock(&mutex);
				
			}
			else{
				tabla[Id].invitaciones=tabla[Id].invitaciones-1;
			}
			//Comprobar si la partida esta lista para empezar
			if (tabla[Id].invitaciones==tabla[Id].Jugadores.num){
				//Partida lista para empezar
				printf("Partida lista para empezar\n");
				EmpezarPartida(Id);
				//Se le asigna un identificador en la base de datos.
				int i = DameID();
				pthread_mutex_lock(&mutex);
				tabla[Id].BBDD=i;
				PonPartida(Id);
				pthread_mutex_unlock(&mutex);
			}
		}
		else if (codigo==8){

			p=strtok(NULL,"/");
			int ID=atoi(p);
			p=strtok(NULL,"/");
			char nombre[20];
			strcpy(nombre,p);
			p=strtok(NULL,"/");
			char mensaje[100];
			strcpy(mensaje,p);
		
			EnviarMensaje(ID,nombre,mensaje);
		}
		else if(codigo==9){
			p=strtok(NULL,"/");
			char avatar[20];
			strcpy(avatar,p);
			p=strtok(NULL,"/");
			int Id=atoi(p);
			p=strtok(NULL,"/");
			char sesion[20];
			strcpy(sesion,p);
			pthread_mutex_lock(&mutex);
			AnadirAvatar(&tabla[Id].Jugadores,sesion,avatar);
			PonParticipacion(Id,sesion,avatar);
			pthread_mutex_unlock(&mutex);
			EnviarAvatar(Id,sesion,avatar);
			
			if(tabla[Id].Jugadores.avatares==tabla[Id].Jugadores.num){
				int cont = 21;
				pthread_mutex_lock(&mutex);
				InicializarBaraja(Baraja);
				cont = RepartirCartasGanadoras(&tabla[Id],cont,Id);
				pthread_mutex_unlock(&mutex);
				sleep(1);
				pthread_mutex_lock(&mutex);
				cont = RepartirCartasPersonales(&tabla[Id],cont,Id);
				pthread_mutex_unlock(&mutex);
				sleep(1);
				pthread_mutex_lock(&mutex);
				cont = RepartirCartasSobrantes(&tabla[Id],cont,Id);
				pthread_mutex_unlock(&mutex);
			}
			
		}
		else if(codigo==10){
			p=strtok(NULL,"/");
			int ID=atoi(p);
			p=strtok(NULL,"/");
			char sesion[20];
			strcpy(sesion,p);
			p=strtok(NULL,"/");
			int x=atoi(p);
			p=strtok(NULL,"/");
			int y=atoi(p);
			EnviarMovimiento(ID, sesion,x,y,sock_conn);
		}
		else if (codigo==11){
			p=strtok(NULL,"/");
			int ID=atoi(p);
			pthread_mutex_lock(&mutex);
			InciarAcusacion(tabla,ID,sock_conn);
			pthread_mutex_unlock(&mutex);
			p=strtok(NULL,"/");
			char sesion[20];
			strcpy(sesion,p);
			p=strtok(NULL,"/");
			int x=atoi(p);
			p=strtok(NULL,"/");
			int y=atoi(p);
			p=strtok(NULL,"/");
			int asesino=atoi(p);
			p=strtok(NULL,"/");
			int arma=atoi(p);
			p=strtok(NULL,"/");
			int lugar =atoi(p);
			EnviarAcusacion(ID,sesion,x,y,sock_conn,asesino,arma,lugar);
		}
		else if (codigo==12){
			p=strtok(NULL,"/");
			int Id =atoi(p);
			p=strtok(NULL,"/");
			char nombre[20];
			strcpy(nombre,p);
			int  carta = -1;
			pthread_mutex_lock(&mutex);
			AnadirRespuestaAcusacion(&tabla[Id].Acusaciones,nombre,sock_conn,carta);
			pthread_mutex_unlock(&mutex);
			if(tabla[Id].Acusaciones.num==(tabla[Id].Jugadores.num-1)){
				ResponderAcusacion(&tabla[Id].Acusaciones,Id);
			}
			
		}
		else  if (codigo==13){
			p=strtok(NULL,"/");
			int Id =atoi(p);
			p=strtok(NULL,"/");
			char nombre[20];
			strcpy(nombre,p);
			p=strtok(NULL,"/");
			int carta=atoi(p);
			pthread_mutex_lock(&mutex);
			AnadirRespuestaAcusacion(&tabla[Id].Acusaciones,nombre,sock_conn,carta);
			pthread_mutex_unlock(&mutex);
			printf("Numero Lista Acusados: %d\n",tabla[Id].Acusaciones.num);
			printf("Numero Lista Conectados:  %d\n",(tabla[Id].Jugadores.num-1));
			if(tabla[Id].Acusaciones.num==(tabla[Id].Jugadores.num-1)){
				ResponderAcusacion(&tabla[Id].Acusaciones,Id);
			}
		}
		else if(codigo==14){
			p=strtok(NULL,"/");
			int Id=atoi(p);
			p=strtok(NULL,"/");
			char sesion[20];
			strcpy(sesion,p);
			pthread_mutex_lock(&mutex);
			Elimina(&tabla[Id].Jugadores,sesion);
			pthread_mutex_unlock(&mutex);
			int cont =0 ;
			p=strtok(NULL,"/");
			char hey[200];
			bzero(hey,100);
			printf("cartas: %s\n",hey);
			while(p!=NULL){
				sprintf(hey,"%s/%s",hey,p);
				p=strtok(NULL,"/");
				cont=cont+1;
			}
			printf("cartas: %s\n",hey);
			EnviarSalirPartida(Id,sesion,sock_conn,hey,cont);
			
		}
		else if(codigo==15){
			p=strtok(NULL,"/");
			int Id=atoi(p);
			p=strtok(NULL,"/");
			char sesion[20];
			strcpy(sesion,p);
			p=strtok(NULL,"/");
			int tiempo=atoi(p);
			pthread_mutex_lock(&mutex);
			EnviarGanador(Id,sesion,sock_conn);
			EliminarTodos(&tabla[Id].Jugadores);
			PonGanador(Id,sesion);
			PonDuracion(Id,tiempo);
			pthread_mutex_unlock(&mutex);
						
		}
		else if (codigo ==16){
			char nombre[50];
			p=strtok(NULL,"/");
			strcpy(nombre,p);
			int i=EliminaBBDD(nombre);
			printf("%d\n",i);
			if(i==-1){
				write (sock_conn,"20/-1", strlen("20/-1"));
			}
			else
			   write (sock_conn,"20/1", strlen("20/1"));
			
		}
		
	
	}
	//Bloqueamos la parte donde la lista está siendo modificada
	pthread_mutex_lock(&mutex);
	i =Elimina(&Lista,nombre);
	pthread_mutex_unlock(&mutex);
	EnviarLista();
	
	if(i==0)
		close(sock_conn);
	else
		printf("No se puede desconectar a este usuario");
}
//Funciones para atender las peticiones del server a la base de datos;
int EstaRegistrado(char nombre[60],char contrasena[60]){
	//REvisa si el usuario esta registrado en la base de datos.
	//Devuelve 0 en caso  contrario
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	strcpy(consulta,"Select Nombre,Contraseña FROM Jugador WHERE Nombre='");
	strcat(consulta,nombre);
	strcat (consulta,"' AND Contraseña='");
	strcat(consulta,contrasena);
	strcat (consulta,"'");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return 0;
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL)
		return 0;
	else
		return 1;
}
int Registrar(char nombre[60],char contrasena[60]){
	//Registra si es posible al nueo usuario
	//Devuelve 0 si no es posible registrarlo
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	char consulta2[500];
	int yaesta;
	yaesta=EstaRegistrado(nombre,contrasena);
	if (yaesta==0){
		strcpy(consulta,"SELECT MAX(Identificador) FROM Jugador");
		err=mysql_query (conn, consulta);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return 0;
			exit (1);
		}
		resultado = mysql_store_result (conn);
		row = mysql_fetch_row (resultado);
		int id=atoi(row[0])+1;
		char id1[10];
		strcpy(consulta2,"INSERT INTO Jugador VALUES(");
		sprintf(id1,"%d",id);
		strcat(consulta2,id1);
		strcat(consulta2,",'");
		strcat(consulta2,nombre);
		strcat(consulta2,"','");
		strcat(consulta2,contrasena);
		strcat(consulta2,"',0,0);");
		
		err=mysql_query (conn, consulta2);
		if (err!=0) {
			printf ("Error al añadir en la base de datos %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return 0;
			exit (1);
		}
		else
			return 1;
		
	}
	else
		return 0;
		
	
}

void PorcentajeVictorias(char nombre[60],char *solucion[10]){
	//Devuelve el porcentage de victorias del usuario nombre
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta [500];
	float porcentaje;
	strcpy(consulta,"SELECT Jugador.Partidas_ganadas, Jugador.Partidas_jugadas FROM Jugador WHERE Nombre = '");
	strcat (consulta, nombre);
	strcat (consulta,"'");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		porcentaje=-1.0;
		exit (1);
	}
	//recogemos el resultado de la consulta. El resultado de la
	//consulta se devuelve en una variable del tipo puntero a
	//MYSQL_RES tal y como hemos declarado anteriormente.
	//Se trata de una tabla virtual en memoria que es la copia
	//de la tabla real en disco.
	resultado = mysql_store_result (conn);
	// El resultado es una estructura matricial en memoria
	// en la que cada fila contiene los datos de un jugador.
	// Ahora obtenemos la primera fila que se almacena en una
	// variable de tipo MYSQL_ROW
	row = mysql_fetch_row (resultado);
	// En una fila hay tantas columnas como datos tiene un
	// jugador. En nuestro caso hay dos columnas: Partidas_ganadas(row[0]) y
	// Partidas_jugadas(row[1]) .
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
		porcentaje =-1.0;
	}
	else
	{
		int Partidas_ganadas=atoi(row[0]);
		int Partidas_jugadas=atoi(row[1]);
		if (Partidas_jugadas==0)
			porcentaje=0;
		else
		{
			porcentaje=((float) Partidas_ganadas/(float)Partidas_jugadas)*100;
		}
		printf("Ganadas: %d \n",Partidas_ganadas);
		printf("Jugadas: %d \n",Partidas_jugadas);
		printf("El ratio de victorias es: %.2f \n",porcentaje);
	}
	sprintf(solucion,"3/%.2f",porcentaje);
}
void JugadorFavorito(char nombre[60],char *avatar[60]){
	//Devuelve el avatar mas jugad por usuario especificado
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	int ismael=0;
	int itziar=0;
	int guillem=0;
	int victor=0;
	int azahara=0;
	strcpy(consulta,"SELECT Participacion.Avatar,Jugador.Partidas_jugadas FROM (Jugador, Participacion) WHERE Jugador.Nombre = '");
	strcat (consulta, nombre);
	strcat (consulta,"' AND Participacion.Id_J = Jugador.Identificador");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		strcpy(avatar,"4/E");
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
		strcpy(avatar,"4/X");
	}
	
	else
	{
		while (row !=NULL) {
			if (strcmp(row[0],"Ismael")==0)
				ismael=ismael+1;
			else if (strcmp(row[0],"Itziar")==0)
				itziar=itziar+1;
			else if (strcmp(row[0],"Guillem")==0)
				guillem=guillem+1;
			else if (strcmp(row[0],"Victor")==0)
				victor=victor+1;
			else if (strcmp(row[0],"Azahara")==0)
				azahara=azahara+1;
			
			strcpy(avatar,"4/Ismael");
			int i=ismael;
			if (i<itziar){
				i=itziar;
				strcpy(avatar,"4/Itziar");
			}
			if (i<guillem){
				i=guillem;
				strcpy(avatar,"4/Guillem");
			}
			if (i<victor){
				i=victor;
				strcpy(avatar,"4/Victor");
			}
			if (i<azahara){
				i=azahara;
				strcpy(avatar,"4/Azahara");
			}
			
			// obtenemos la siguiente fila
			row = mysql_fetch_row (resultado);
		}
		
		printf("El avatar más jugado es: %s \n",avatar);
		
	}
}
void GanadorPartida(char Identificador[60],char *nombre[60]){
	//Devuelve quien fue el ganador de la partida con ID indicado
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	strcpy(consulta,"SELECT Jugador.nombre FROM (Jugador, Partida) WHERE Partida.Identificador = '");
	strcat (consulta, Identificador);
	strcat (consulta,"' AND Partida.Ganador = Jugador.Identificador");
	printf("%s\n",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		strcpy(nombre,"5/E");
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
		strcpy(nombre,"5/X");
	}
	else
	{
		printf("El ganador de la partida %s, es %s \n",Identificador,row[0]);
		sprintf(nombre,"5/%s",row[0]);
		printf("5/%s\n",nombre);
	}
}
int DameID(){
	//Devuelve cual debe ser el Id del proximo jugador en registrarse
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	strcpy(consulta,"SELECT MAX(Identificador) From Partida");
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
		return -1;
	}
	else{
		int k = atoi(row[0])+1;
		return k;
	}
}
int DameIDJ(char sesion[20]){
	//Devuelve cual es el identificador de un jugado en cuestion
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	sprintf(consulta,"SELECT Identificador From Jugador WHERE Nombre='%s'",sesion);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	if (row == NULL){
		printf ("No se han obtenido datos en la consulta\n");
	}
	else{
		printf("%d",atoi(row[0]));
		return atoi(row[0]);
	}
}
void PonPartida(int Id){
	//Añade la partido con Id indicado a la base de datos
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	
	sprintf(consulta,"INSERT INTO Partida VALUES(%d,-1,1)",tabla[Id].BBDD);
	printf("%s",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}
void PonParticipacion(int Id ,char sesion[20],char avatar[20]){
	// Añade la participacion de un jugador en una partida concreta(Id), con el avatar que ha jugado en la base de datos
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int IDJ=DameIDJ(sesion);
	
	char consulta[500];
	
	sprintf(consulta,"INSERT INTO Participacion VALUES(%d,%d,'%s')",IDJ,tabla[Id].BBDD,avatar);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}
	

void PonGanador(int Id,char sesion[20]){
	//Añade quien ha sido el  ganador de dicha partida a la base de datos
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	char consulta[500];
	int IDJ=DameIDJ(sesion);
	sprintf(consulta, "UPDATE Partida SET Partida.Ganador = %d WHERE Identificador= %d",IDJ,tabla[Id].BBDD);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}
void PonDuracion(int Id,int tiempo){
	//Añade la duracion de la partida  a la base de datos
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta2[500];
	sprintf(consulta2, "UPDATE Partida SET Partida.Duracion = %d WHERE Identificador= %d",tiempo,tabla[Id].BBDD);
	err=mysql_query (conn, consulta2);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}
int EliminaBBDD(char nombre[50]){
	//Elimina los datos de un jugador de la base de datos
	printf("Entro en la funcion\n");
	int IdentificadorJugador;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[500];
	sprintf(consulta,"SELECT Jugador.Identificador,Partida.Identificador FROM Jugador,Partida WHERE Jugador.Nombre='%s' AND Partida.Ganador=Jugador.Identificador",nombre);
	printf("Consulta1: %s\n",consulta);
	err=mysql_query (conn, consulta);
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return -1;
		exit (1);
		
	}
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
		if ( row != NULL){
		printf("Resultado del Id: %s,\n",row[0]);
		if(row!=NULL){
			printf("entro en el if\n");
			IdentificadorJugador=atoi(row[0]);
			char consulta2[200];

			printf("row[1]: %s\n ",row[1]);
			sprintf(consulta2,"DELETE FROM Participacion WHERE Id_P=%s",row[1]);
			printf("consulta2 primera parte: %s",consulta2);
			row = mysql_fetch_row (resultado);
			while (row!=NULL){
				printf("row[1]: %s\n ",row[1]);
				sprintf(consulta2,"%s OR Id_P=%s",consulta2,row[1]);
				row = mysql_fetch_row (resultado);
			}
			printf("consulta2: %s",consulta2);
			err=mysql_query (conn, consulta2);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				return -1;
					exit (1);
				
			}
		}
		else{
			char consultax[200];
			sprintf(consultax,"SELECT Identificador FROM Jugador WHERE Nombre='%s'",nombre);
			err=mysql_query (conn, consultax);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				return -1;
				exit (1);
				resultado = mysql_store_result (conn);
				row = mysql_fetch_row (resultado);
				IdentificadorJugador=atoi(row[0]);
			
		}
		char consulta3[500];
		sprintf(consulta3,"DELETE FROM Participacion WHERE Id_J=%d",IdentificadorJugador);
		err=mysql_query (conn, consulta3);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return -1;
				exit (1);
			
		}
		char consulta4[200];
		sprintf(consulta4,"DELETE FROM Partida WHERE Ganador=%d",IdentificadorJugador);
		err=mysql_query (conn, consulta4);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return -1;
				exit (1);
			
		}
		char consulta5[200];
		sprintf(consulta5,"DELETE FROM Jugador WHERE Identificador=%d",IdentificadorJugador);
		err=mysql_query (conn, consulta5);
		if (err!=0) {
			printf ("Error al consultar datos de la base %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return -1;
				exit (1);
			
		}
		return 1;
	}
		}
	else
	   return -1;
}

//MAIN DEL SERVER
int main(int argc, char *argv[]){
	inicializar(tabla);
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexiï¿ƒï¾³n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion con la base de datos
	conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "T5BBDD",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexiï¿ƒï¾³n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//Estructiras para los sockets
	int sock_conn,sock_listen;
	struct sockaddr_in serv_adr;

	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(50064);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");

	// Bucle infinito de atender las peticiones abriendo threads
	for (;;)
	{
		printf("Escuchando\n");
		pthread_t thread;
		
		sock_conn=accept(sock_listen,NULL,NULL);
		printf("He recibido conexion\n");
		
		pthread_create(&thread,NULL,AtenderCliente,&sock_conn);
		
	}
}


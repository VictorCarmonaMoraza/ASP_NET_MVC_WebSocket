using Fleck;
using System;
using System.Collections.Generic;

namespace MiPrimerServidorWebSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IP de mi maquina  // ws:// es el protocolo de un socket
            string ipServidorWebSocket = DataSocket.PROTOCOLO_SOCKET + DataSocket.IP_SISTEMA;

            //Creamos nuestro servidor de socket
            WebSocketServer servidorSocket = new WebSocketServer(ipServidorWebSocket);

            //Definimos los clientes qebSocket
            //Almacena todos los clientes conectados
            List<IWebSocketConnection> clientesSockets = new List<IWebSocketConnection>();

            //Mensaje para cuando se inicia el socket
            Console.WriteLine("Servidor web sockets iniciado");

            //Iniciamos el servidor socket
            servidorSocket.Start(clienteSocket =>
            {
                //Evento del cliente, se ejecuta cada vez que nos conectamos al servidor socket
                clienteSocket.OnOpen = () =>
                {
                    //Añado al listado de socket el cliente
                    clientesSockets.Add(clienteSocket);
                    Console.WriteLine("Cliente conectado");
                };
                //Cunaod tu envias algo al socket
                clienteSocket.OnMessage = (string texto) =>
                {
                    //Emitimos a todos los clientes conectados
                    clientesSockets.ForEach(p => p.Send(texto));

                    //Es equivalente a :
                    //for (int i = 0; i < clientesSockets.Count; i++)
                    //{
                    //    clientesSockets[i].Send(texto);
                    //}

                    //Otra forma
                    //foreach (var cliente in clientesSockets)
                    //{
                    //    cliente.Send(texto);
                    //}
                };
                //Cuando ya no estas conectado al socketo sales del socket
                clienteSocket.OnClose = () =>
                {
                    //Eliminamos el cliente que estaba conectadio cuando sale
                    clientesSockets.Remove(clienteSocket);
                    Console.WriteLine("Cliente desconectado");
                };
            });

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static Client client;
        TcpListener server;
        MessageHandler messageHandler;
        public Server()
        {
            messageHandler = new MessageHandler();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }

        public void Run()
        {
            do
            {
                if (IsRunning())
                {

                }
                else
                {
                    AsyncAccept();
                }
            } while (true);           
                string message = client.Recieve();

                Respond(message);
                
        }
        private async bool IsRunning()
        {
            bool output = true;
            await AsyncAccept();
            output = false;

        }
        private Task AsyncAccept()
        {
            return Task.Run(() =>
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();

                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                messageHandler.AddClientTolobby(client);
            });
        }
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();

            Console.WriteLine("Connected");
            NetworkStream stream = clientSocket.GetStream();
            client = new Client(stream, clientSocket);
            messageHandler.AddClientTolobby(client);
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}

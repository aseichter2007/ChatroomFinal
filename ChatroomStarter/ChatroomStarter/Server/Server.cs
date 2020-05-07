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
        Random random;
        public Server()
        {
            random = new Random();
            messageHandler = new MessageHandler();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }

        public void Run()
        {
            Parallel.Invoke(AcceptClient,IncomingMessages,messageHandler.Run);
            //Thread getClients = new Thread(IncomingConnecitons);
            //Thread incomingMessages = new Thread(IncomingMessages);
            //Thread outgoingMessages = new Thread(OutgoingMessages);
            //Thread handlemessage = new Thread(messageHandler.Run);

            //getClients.Start();
            //incomingMessages.Start();
            //handlemessage.Start();
            //outgoingMessages.Start();
        }        
        //public void IncomingConnecitons()
        //{
        //    while (true)
        //    {

        //        Task client = new Task(AcceptClient);
        //        client.Start(); 
        //        client.Wait();
        //    }
        //}
        public void IncomingMessages()
        {
            while (true)
            {
                foreach (string UID in messageHandler.allusers)
                {
                    recievable(messageHandler.allusers.TryGetUser(UID));
                }
                Thread.Sleep(100);
            }
        }
        public void recievable(Client client)
        {
            messageHandler.Add( client.Recieve(),client);
        }
        
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient); 

                clientSocket = server.AcceptTcpClient();

                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                string UID = random.Next(99999999).ToString();
                while (messageHandler.allusers.CheckUser(UID))
                {
                    UID = random.Next(99999999).ToString();
                }
                client = new Client(stream, clientSocket, UID);

                messageHandler.AddClientTolobby(client);
            }
        }        
    }
}

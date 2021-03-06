﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client:INotify
    {
        NetworkStream stream;
        TcpClient client;
        public string UserId;

        public Client(NetworkStream Stream, TcpClient Client,string UID)
        {
            stream = Stream;
            client = Client;
            Random random = new Random();
            UserId = UID;
        }
        
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
        }
        public string Recieve()
        {
            if (stream.DataAvailable)
            {
                byte[] recievedMessage = new byte[256];
                stream.Read(recievedMessage, 0, recievedMessage.Length);           
                string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
                Console.WriteLine(recievedMessageString);
                return recievedMessageString;
            }
            else
            {
                return null;
            }
            
        }

        public void Notify(string Message)
        {
                byte[] message = Encoding.ASCII.GetBytes(Message);
                stream.Write(message, 0, message.Count());           
        }
    }
}

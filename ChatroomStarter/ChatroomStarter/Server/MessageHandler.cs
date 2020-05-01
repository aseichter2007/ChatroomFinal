using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class MessageHandler
    {
        RoomList roomList;
        MessageQueue messageQueue;
        Message thismessage;
        List<string> recieved;
        char parsebreak = '█';
        public MessageHandler()
        {
            messageQueue = new MessageQueue();
            roomList = new RoomList();
            recieved = new List<string>();
        }
        public void Run()
        {
            bool run = true;
            do
            {
                if (messageQueue.MessageWaiting())
                {
                    thismessage = messageQueue.GetRemoveOldest();
                }


            } while (run);
        }
        public Task RunRecieveMessages()
        {
            return Task.Run(() =>
            {
                foreach (Room room in roomList)
                {
                    foreach (Client client in room)
                    {
                        Message message = new Message(client, client.Recieve());
                        messageQueue.AddMessage(message);
                        
                    }
                }
            });
        }
        public string[] MessageParse(string message)
        {
            //check room, /check commands

            string[] output = new string[3];
            if (true)
            {

            }
            return output;
        }
        public void AddClientTolobby(Client client)
        {
            roomList.AddClientToLobby(client);
        }
    }
}

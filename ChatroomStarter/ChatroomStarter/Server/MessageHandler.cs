using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class MessageHandler
    {
        public ConnectedUsers allusers;
        RoomList roomList;
        MessageQueue messageQueue;
     
        char parsebreak = '█';
        public MessageHandler()
        {
            allusers = new ConnectedUsers();
            messageQueue = new MessageQueue();
            roomList = new RoomList();
        }
        public void Run()
        {
            while (true)
            {
                MessageParse();
                
            }
        }
       
        public void MessageParse()
        {
            //check room, /check commands
            if (messageQueue.MessageWaiting())
            {
                Message message = messageQueue.GetOldest();
                string[] output = message.Body.Split(parsebreak);
                MessageAction(output, message.sender);
            }
            else
            {
                Thread.Sleep(100);
            }
           
        }
        public void Broadcast(string[] data, string senderID, Room room)
        {
            if (data[1] != "")
            {
                foreach (Client client in room)
                {
                    client.Send(0 + parsebreak + senderID +":"+ parsebreak + data[3]);
                }
            }
            else
            {
                if (allusers.CheckUser(data[2]))
                {
                    Client whisper = allusers.TryGetUser(data[2]);
                    whisper.Send(0 + parsebreak + senderID + ":" + parsebreak + data[3]);                        
                }
            }
        }
        private void MessageAction(string[] data,Client client)
        {
            //[0] = commands
            //[1] = room
            //[2] = whisper
            //[3] = message
            List<string> rooms = roomList.GetAvailableRooms();
            Room working = roomList.GetRoom(rooms.IndexOf(data[1]));

            switch (data[0])
            {
                case "Message":
                    Broadcast(data, client.UserId, working);
                    break;
                case "NewUsername":
                    string old = client.UserId;
                    if (!allusers.CheckUser(data[3]))
                    {
                        allusers.ChangeUID(old, data[3]);
                        working.connectedUsers.ChangeUID(old, data[3]);
                        client.Send(1 + parsebreak + "your new username is " +parsebreak+ data[3]);
                    }
                    else
                    {
                        client.Send(0 + parsebreak + client.UserId + parsebreak + "this name is taken");
                    }
                    
                    break;
                case "JoinRoom":
                    if (rooms.Contains(data[1]))
                    {                        
                        working.AddUser(client);
                        client.Send(2+parsebreak+data[1]+parsebreak+"joined "+working.name);
                    }

                    else
                    {
                        client.Send(0+parsebreak+client.UserId+parsebreak+"Room does not exist");
                    }
                    break;
                case "CreateRoom":
                    roomList.CreateRoom(data[1]);
                    client.Send(2+parsebreak+data[1]+parsebreak+data[1]);
                    break;
                case "LeaveRoom":
                    working.connectedUsers.DisconnectUser(client.UserId);
                    roomList.AddClientToLobby(client);
                    client.Send(2 + parsebreak + "Lobby" + parsebreak + "you rejoined the Lobby");
                    break;
                case "Disconnect":
                    allusers.DisconnectUser(client.UserId);
                    working.connectedUsers.DisconnectUser(client.UserId);
                    client.Send(9 + parsebreak + client.UserId + parsebreak + "Goodbye. Please come again.");
                    break;
                case "Roomlist":
                    foreach (string room in rooms)
                    {
                        client.Send(0 + parsebreak + "Available room:" + parsebreak + room);
                    }  
                    break;
                    
            }
        }
        private int MessageCommands(string data)
        {
            int output = 0;
            
            return output;
        }
        public void AddClientTolobby(Client client)
        {
            allusers.AddUser(client);
            roomList.AddClientToLobby(client);
        }
        public void Add(string message,Client client)
        {
            if (message==null)
            {
                return;
            }
            Message packedmessage = new Message(client,message);
            messageQueue.AddMessage(packedmessage);
        }
    }
}

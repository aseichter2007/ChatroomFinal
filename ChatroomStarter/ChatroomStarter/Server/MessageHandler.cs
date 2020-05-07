using System;
using System.Collections.Generic;
using System.ComponentModel;
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
     
        char parsebreak = '\t';
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
            if (data[2] == "")
            {
                foreach (Client client in room)
                {
                    client.Send("M"+ parsebreak + senderID +":"+ parsebreak + data[3] + parsebreak);
                }
            }
            else
            {
                if (allusers.CheckUser(data[2]))
                {
                    Client whisper = allusers.TryGetUser(data[2]);
                    whisper.Send("M" + parsebreak + senderID + ":" + parsebreak +"Whisper: "+ data[3] + parsebreak);                        
                }
                else if (allusers.CheckUser(" "+data[2]))
                {
                    Client whisper = allusers.TryGetUser(" "+data[2]);
                    //whisper.Send("M" + parsebreak + senderID + ":" + parsebreak + "Whisper: " + data[3] + parsebreak);
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
            int thisroom = rooms.IndexOf(data[1]);
            Room working = roomList.rooms[thisroom];

            switch (data[0])
            {
                case "Message":
                    client.Send("8"+parsebreak+parsebreak+parsebreak);
                    Broadcast(data, client.UserId, working);

                    break;
                case "NewUsername":
                    string old = client.UserId;
                    if (!allusers.CheckUser(data[3]))
                    {
                        allusers.ChangeUID(old, data[3]);
                        working.connectedUsers.ChangeUID(old, data[3]);
                        client.Send("M" + parsebreak + "your new username is " +parsebreak+ data[3] + parsebreak);
                    }
                    else
                    {
                        client.Send("M" + parsebreak + client.UserId + parsebreak + "this name is taken" + parsebreak);
                    }
                    
                    break;
                case "JoinRoom":
                    if (rooms.Contains(data[2]))
                    {
                        working.DisconnectUser(client.UserId);
                        int roomnumber = rooms.IndexOf(data[2]);
                        working = roomList.GetRoom(roomnumber);
                        working.AddUser(client);
                        NotifyJoin(roomnumber, client);
                        client.Send("2"+parsebreak+data[2]+parsebreak+ "left "+data[1] + " joined "+working.name + parsebreak);
                    }

                    else
                    {
                        client.Send("0"+parsebreak+client.UserId+parsebreak+"Room does not exist" + parsebreak);
                    }
                    break;
                case "CreateRoom":
                    if (rooms.Contains(data[2]))
                    {
                        client.Send("M" + parsebreak + client.UserId + parsebreak + "this room already exists" + parsebreak);
                        break;
                    }
                    roomList.CreateRoom(data[2]);
                    client.Send("M"+parsebreak+client.UserId+parsebreak+"created "+ data[2] + parsebreak);
                    break;
                case "LeaveRoom":
                    working.connectedUsers.DisconnectUser(client.UserId);
                    roomList.AddClientToLobby(client);
                    client.Send("2" + parsebreak + "Lobby" + parsebreak + "you rejoined the Lobby" + parsebreak);
                    break;
                case "Disconnect":
                    allusers.DisconnectUser(client.UserId);
                    working.connectedUsers.DisconnectUser(client.UserId);
                    client.Send("9" + parsebreak + client.UserId + parsebreak + "Goodbye. Please come again." + parsebreak);
                    break;
                case "Roomlist":
                    foreach (string room in rooms)
                    {
                        client.Send("0" + parsebreak + "Available room:" + parsebreak + room + parsebreak);
                    }  
                    break;
                    
            }
        }
       
        public void AddClientTolobby(Client client)
        {
            allusers.AddUser(client);
            roomList.AddClientToLobby(client);
            NotifyJoin(0,client);
        }
        private void NotifyJoin(int roomnumber,Client client)
        {
            Room working = roomList.GetRoom(roomnumber);
            foreach (Client user in working)
            {
                user.Notify("M"+parsebreak +"notice" +parsebreak+ client.UserId + " has joined the room"+parsebreak);
            }

            
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

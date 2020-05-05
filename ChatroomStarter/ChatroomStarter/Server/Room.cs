using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Room : IEnumerable
    {
        public ConnectedUsers connectedUsers;

        private int count;
        public int Count
        {
            get => count;
        }
         public string name;
        public Room(string name)
        {
            connectedUsers = new ConnectedUsers();
            this.name = name;
        }
        public void AddUser(Client client)
        {
            connectedUsers.AddUser(client);
            Console.WriteLine(client.UserId + " has joined "+ name);
        }
        public void DisconnectUser(string userID)
        {
            connectedUsers.DisconnectUser(userID);
        }

        public IEnumerator GetEnumerator()
        {
            List<string> connectedusers = connectedUsers.GetUserList();
            for (int i = 0; i < connectedUsers.GetUserList().Count; i++)
            {
                yield return connectedUsers.TryGetUser(connectedusers[i]);
            }
        }
    }
}

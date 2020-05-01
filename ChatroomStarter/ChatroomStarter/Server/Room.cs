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
        ConnectedUsers connectedUsers;
        public List<Message> MessageLog;

        private int count;
        public int Count
        {
            get => count;
        }
        string name;
        public Room(string name)
        {
            connectedUsers = new ConnectedUsers();
            this.name = name;
            MessageLog = new List<Message>();
        }
        public void AddUser(Client client)
        {
            connectedUsers.AddUser(client);
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

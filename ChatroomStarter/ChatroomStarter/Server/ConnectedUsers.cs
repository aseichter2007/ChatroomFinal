using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server
{
    class ConnectedUsers :IEnumerable
    {
        private Dictionary<string, Client> connectedUsers;

        public ConnectedUsers()
        {
            connectedUsers = new Dictionary<string, Client>();
        }
        public bool AddUser(Client newClient)
        {
            bool output = true;
            if (connectedUsers.ContainsKey(newClient.UserId))
            {
                output = false;
            }
            else
            {
                connectedUsers.Add(newClient.UserId, newClient);
            }
            return output;
        }
        public Client TryGetUser(string userID)
        {
            Client output;
            if(connectedUsers.TryGetValue(userID,out output))
            {
            }
            else
            {
                Console.WriteLine("user not found");
            }
            return output;            
        }
        public bool CheckUser(string UserID)
        {
            bool output = false;
            output = connectedUsers.ContainsKey(UserID);
            return output;
        }
        public void DisconnectUser(string userID)
        {
            bool check = CheckUser(userID);
            if (true)
            {
                connectedUsers.Remove(userID);
            }
        }
        public List<string> GetUserList()
        {
            List<string> output = new List<string>();

            foreach (string key in connectedUsers.Keys)
            {
                output.Add(key);
            }
            return output;
        }
        public void ChangeUID(string Old,string newkey)
        {
            Client Working = TryGetUser(Old);
            Working.UserId = newkey;
            AddUser(Working);
            DisconnectUser(Old);
        }


        public IEnumerator GetEnumerator()
        {
            List<string> keys = GetUserList();
            for (int i = 0; i < keys.Count; i++)
            {
                yield return keys[i];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server
{
    class ConnectedUsers
    {
        private Dictionary<string, Client> connectedUsers;

        public ConnectedUsers()
        {
            connectedUsers = new Dictionary<string, Client>();
        }
        public bool AddUser(string userID, Client newClient)
        {
            bool output = true;
            if (connectedUsers.ContainsKey(userID))
            {
                output = false;
            }
            else
            {
                connectedUsers.Add(userID, newClient);
            }
            return output;
        }
        public Client TryGetUser(string userID)
        {
            Client output;
            if(connectedUsers.TryGetValue(userID,out output))
            {
                //maybe messages in the server output
            }
            else
            {

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
    }
}

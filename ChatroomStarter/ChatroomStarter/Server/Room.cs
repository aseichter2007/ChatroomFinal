using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Room
    {
        ConnectedUsers connectedUsers;
        string name;
        public Room(string name)
        {
            connectedUsers = new ConnectedUsers();
            this.name = name;
        }
    }
}

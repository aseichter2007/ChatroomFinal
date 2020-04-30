using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    class RoomList
    {
        private List<string> roomNames;
        private List<Room> rooms;
        public RoomList()
        {
            rooms = new List<Room>();
            roomNames = new List<string>();


        }
        public List<string> GetAvailableRooms()
        {
            return roomNames;
        }
        public bool CreateRoom(string name)
        {
            bool output = true;
            if (roomNames.Contains(name))
            {
                output = false;
            }
            else
            {
                Room newroom = new Room(name);
                roomNames.Add(name);
                rooms.Add(newroom);
            }
            return output;
        }
    }
}

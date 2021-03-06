﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    class RoomList : IEnumerable
    {
        private List<string> roomNames;
        public List<Room> rooms;
        public RoomList()
        {
            rooms = new List<Room>();
            roomNames = new List<string>();
            CreateLobby();
        }
        public Room GetRoom(int index)
        {
            return rooms[index];
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
        private void CreateLobby()
        {
            Room newroom = new Room("Lobby");
            roomNames.Add("Lobby");
            rooms.Add(newroom);
        }
        public void AddClientToLobby(Client client)
        {
            rooms[0].AddUser(client);
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                yield return rooms[i];
            }
        }
    }
}

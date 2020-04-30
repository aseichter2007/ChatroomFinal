using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageHandler
    {
        RoomList roomList;
        MessageQueue messageQueue;
        Message message;
        public MessageHandler()
        {
            messageQueue = new MessageQueue();
            roomList = new RoomList();
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

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageQueue
    {
        private List<Message> messageList;

        public MessageQueue()
        {
            messageList = new List<Message>();
        }
        public void AddMessage(Message message)
        {
            messageList.Add(message);
        }
        public Message GetOldest()
        {
            return messageList[0];
        }
        public Message GetIndex(int index)
        {
            return messageList[index];
        }
        public Message GetRemoveOldest()
        {
            Message output = messageList[0];
            messageList.RemoveAt(0);
            return output;
        }
        public void QueueTrim()
        {
            if (messageList.Count<100)
            {
                messageList.RemoveRange(0, messageList.Count - 100);
            }
        }

    }
}

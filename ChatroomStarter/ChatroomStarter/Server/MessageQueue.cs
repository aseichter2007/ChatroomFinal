using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageQueue
    {
        private Queue<Message> messageList;

        public MessageQueue()
        {
            messageList = new Queue<Message>();
        }
        public void AddMessage(Message message)
        {
            messageList.Enqueue(message);
        }
        public Message GetOldest()
        {
            return messageList.Dequeue();
        }
        
        public bool MessageWaiting()
        {
            return (messageList.Count > 0);
        }       
    }
}

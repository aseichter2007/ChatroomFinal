using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MessageQueue: IEnumerable
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
            if (index>0&&index<messageList.Count)
            {
                return messageList[index];
            }
            else
            {
                throw new Exception("index outside of list");
            }
        }
        public bool MessageWaiting()
        {
            return (messageList.Count < 0);
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

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < messageList.Count; i++)
            {
               yield return messageList[i];
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class RoomMessageLog :IEnumerable
    {
        List<Message> messageLog;

        public RoomMessageLog()
        {
            messageLog = new List<Message>();
        }

        public void TrimLog()
        {
            if (messageLog.Count<100)
            {
                messageLog.RemoveRange(0, messageLog.Count - 100);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < messageLog.Count; i++)
            {
                yield return messageLog[i];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class MessageParse
    {
        static char parsebreak = '█';

        
        public static void ParseMessage(string message)
        {
            string[] working = message.Split(parsebreak);
            if (working[0]=="8")
            {
                return;
            }
            UI.DisplayMessage(working[1] + ":" + working[2]);
            switch (working[0])
            {
                case "0":
                    break;
                case "1":
                    break;
                case "2":
                    MessageBuilder.room = working[1];
                    break;
                case "3":
                    break;
                case "9":
                    break;

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class MessageBuilder
    {
        static char parsebreak = '█';
        static private List<string> commands= new List<string>() { "Message", "NewUsername", "JoinRoom", "CreateRoom", "LeaveRoom", "Roomlist", "Disconnect" };

        public static int CommandCheck(string input, string room)
        {
            int output = 0;
            return output;
        }
        public static string MessageOut(int command,string input)
        {
            string message = "";
            return message;
        }
        
    }
}

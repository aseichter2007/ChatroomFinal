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
        public static string room = "Lobby";
        static private List<string> commands= new List<string>() { "Message", "NewUsername", "JoinRoom", "CreateRoom", "LeaveRoom", "Roomlist", "Disconnect" };

        public static string CommandCheck(string input)
        {
            //[0] = commands
            //[1] = room
            //[2] = whisper
            //[3] = message
            int output = 0;
            string[] message = new string[4] { "", "", "", "" };
            if (input.Contains('@')&&input.Contains(':'))
            {
                int startWhisper = input.IndexOf('@');
                int endwhisper = input.IndexOf(':');
                message[0] = commands[0];
                message[2] = input.Substring(startWhisper + 1, endwhisper);
                message[3] = input.Substring(endwhisper + 1);
            }
            else
            {
                int split = input.IndexOf(' ');
                message[0] = ProperCommands(input);
                if (message[0] == commands[0])
                {
                    message[1] = room;
                    message[3] = input.Substring(split);
                }
                else if (message[0] == commands[1])
                {
                    message[3] = input.Substring(split);
                }
                else if (message[0] == commands[2])
                {
                    message[1] = input.Substring(split);
                }
                else if (message[0] == commands[3])
                {
                    message[1] = input.Substring(split);
                }
               
                
                

            }

            if (message[0]!="0")
            {
                return (message[0] + parsebreak + message[1] + parsebreak + message[2] + parsebreak + message[3])
            }
            return null;
        }
        private static string ProperCommands(string input)
        {
            int cut = input.IndexOf(' ');
            switch (input.ToLower().Substring(0,cut))
            {
                case "/username":
                    return commands[1];
                case "/join":
                    return commands[2];
                case "/newroom":
                    return commands[3];
                case "/lobby":
                    return commands[4];
                case "/roomlist":
                    return commands[5];
                case "/quit":
                    return commands[6];
                case "/help":
                    UI.DisplayHelp();
                    return "0";
                default:
                    return commands[0];
                    
            }
        }
        public static string MessageOut(int command,string[] input)
        {
            string message = "";
            return message;
        }
        
    }
}

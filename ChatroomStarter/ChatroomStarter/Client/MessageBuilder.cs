using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class MessageBuilder
    {
        static char parsebreak = '\t';
        public static string room = "Lobby";
        static private List<string> commands= new List<string>() { "Message", "NewUsername", "JoinRoom", "CreateRoom", "LeaveRoom", "Roomlist", "Disconnect" };

        public static string CommandCheck(string input)
        {
            //[0] = commands
            //[1] = room
            //[2] = whisper/newroom
            //[3] = message
            string[] message = new string[4] { "", room, "", "" };
            if (input.Contains('@')&&input.Contains(':'))
            {
                int startWhisper = input.IndexOf('@');
                int endwhisper = input.IndexOf(':');
                message[0] = commands[0];
                message[2] = input.Substring(startWhisper + 1, endwhisper-1);
                message[3] = input.Substring(endwhisper + 1);
            }
            else
            {
                int split = 0;
                if (input.IndexOf('/')==0)
                {
                    message[0] = ProperCommands(input);

                    if (input.Contains(' '))
                    {
                       split = input.IndexOf(' ');
                       input = input.Substring(split);
                    }
                }
                else
                {
                    message[0] = commands[0];
                }               
                if (message[0] == commands[0])
                {
                    message[3] = input;
                }
                else if (message[0] == commands[1])
                {
                    message[3] = input;
                }
                else if (message[0] == commands[2])
                {
                    message[2] = input;
                }
                else if (message[0] == commands[3])
                {
                    message[2] = input;
                }
            }

            if (message[0]!="0")
            {
                return (message[0] + parsebreak + message[1] + parsebreak + message[2] + parsebreak + message[3]+parsebreak);
            }
            return null;
        }
        private static string ProperCommands(string input)
        {
            int cut = 0;
            if (input.Contains(' '))
            {
                cut = input.IndexOf(' ');
               input = input.Substring(0, cut);
            }
            switch (input.ToLower())
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class MessageParse
    {
        char parsebreak = '█';

        
        public void ParseMessage(string message)
        {
            string[] working = message.Split(parsebreak);
            Console.WriteLine(working[1] + ":" + working[2]);


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

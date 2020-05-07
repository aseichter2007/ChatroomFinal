using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class UI
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static string GetInput()
        {
            return Console.ReadLine();
        }
        public static async Task<string> WaitInput()
        {
            string output = await AsyncInput();

            return output;
        }
        public static Task<string> AsyncInput()
        {
            return Task.Run(() => 
            Console.ReadLine()
            );
        }
        public static void DisplayHelp()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("/username 'name' sets new username");
            Console.WriteLine("/newroom 'room name' creates new room");
            Console.WriteLine("/join 'room' joins room if it exists");
            Console.WriteLine("/lobby returns you to the lobby.");
            Console.WriteLine("/roomlist gets list of available rooms");
            Console.WriteLine("@'username': whispers to user");
        }
    }
}

using System;
using System.Collections.Generic;
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
    }
}

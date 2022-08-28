using GenerationClient.Core.Interfaces;
using System;

namespace GenerationClient.Presentation
{
    public class ConsoleUserInterfaceService : IUserInterfaceService
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void PrintLine(string text)
        {
            Console.WriteLine(text);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public int ReadNumber()
        {
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}

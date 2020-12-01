using System;

namespace crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            UserCommand userCommand = new UserCommand(input);
        }
    }
}

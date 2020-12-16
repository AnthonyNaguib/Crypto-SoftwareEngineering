using System;

namespace crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            Data userData = new Data();
            UserCommand userCommand = new UserCommand(args, userData);
            HandleInput handleInput = new HandleInput(userData);
        }
    }
}
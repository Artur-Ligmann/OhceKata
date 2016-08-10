using System;

namespace Ohce
{
    public class ConsoleService : IConsole
    {

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string v)
        {
            Console.WriteLine(v);
        }
    }

    public interface IConsole
    {
        void Write(string v);
        string ReadLine();
    }
}
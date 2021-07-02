using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"New Line - {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
                Thread.Sleep(20000);
            }
        }
    }
}

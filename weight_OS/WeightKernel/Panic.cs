using System;
using Sys = Cosmos.System;

namespace WeightKernel
{
    public static class Panic
    {
        public static void Trigger(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine("PANIC!!!!!!!");
            Console.WriteLine("weightOS has encountered a critical error and has been stopped. We'll let you read this info, and then we'll restart for you.");
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            System.Threading.Thread.Sleep(10000);
            Sys.Power.Reboot();
        }
    }
}
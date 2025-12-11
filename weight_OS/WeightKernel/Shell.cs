using System;

namespace WeightKernel
{
    public class Shell
    {
        public void Run()
        {
            while (true)
            {
                Console.Write("kernel #> ");
                string input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                string[] parts = input.Split(' ', 2);
                string command = parts[0].ToLower();
                string args = parts.Length > 1 ? parts[1] : "";

                switch (command)
                {
                    case "help":
                        ShowHelp();
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                    case "exit":
                        Console.WriteLine("Shutting down...");
                        Cosmos.System.Power.Shutdown();
                        break;
                    case "echo":
                        Console.WriteLine(args);
                        break;
                    case "panic":
                        Panic.Trigger("User triggered manual panic");
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("Kernel shell available commands:");
            Console.WriteLine("  help  - Show this help message");
            Console.WriteLine("  cls   - Clear the screen");
            Console.WriteLine("  exit  - Shutdown the PC");
            Console.WriteLine("  echo  - You know what this does");
        }
    }
}
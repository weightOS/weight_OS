using System;
using System.IO;
using System.Collections.Generic;
using Cosmos.System.FileSystem.VFS;

namespace WeightKernel
{
    public class Shell
    {
        private int currentDrive = 0;

        public void Run()
        {
            while (true)
            {
                Console.Write($"kernel {currentDrive}:\\> ");
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
                    case "cd":
                        ChangeDrive(args);
                        break;
                    case "ls":
                        ListCurrentDirectory();
                        break;
                    case "cat":
                        CatFile(args);
                        break;
                    case "panic":
                        Panic.Trigger("manual panic!");
                        break;
                    case "format":
                        if (int.TryParse(args, out int driveNum))
                            VFS.FormatDrive(driveNum);
                        else
                            Console.WriteLine("Usage: format <drive number>");
                        break;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help  - Show this help message");
            Console.WriteLine("  cls   - Clear the screen");
            Console.WriteLine("  exit  - Shutdown the OS");
            Console.WriteLine("  echo  - You know what this does");
            Console.WriteLine("  cd    - Change current drive");
            Console.WriteLine("  ls    - List files and directories on current drive");
            Console.WriteLine("  cat   - Display file contents");
            Console.WriteLine("  format - Format a drive");
        }

        private void ChangeDrive(string arg)
        {
            if (int.TryParse(arg, out int driveNum))
            {
                var disks = VFSManager.GetDisks();
                if (driveNum >= 0 && driveNum < disks.Count)
                {
                    currentDrive = driveNum;
                    Console.WriteLine($"Switched to drive {currentDrive}:\\");
                }
                else
                {
                    Console.WriteLine("Drive does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Usage: cd <drive number>");
            }
        }

        private void ListCurrentDirectory()
        {
            string rootPath = $"{currentDrive}:\\";
            try
            {
                var dirs = Directory.GetDirectories(rootPath);
                var files = Directory.GetFiles(rootPath);

                foreach (var d in dirs) Console.WriteLine("[DIR]  " + d);
                foreach (var f in files) Console.WriteLine("[FILE] " + f);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error listing directory: " + e.Message);
            }
        }

        private void CatFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Usage: cat <filename>");
                return;
            }

            string path = $"{currentDrive}:\\{filename}";
            try
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("File does not exist: " + filename);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
        }
    }
}
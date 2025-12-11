using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using Sys = Cosmos.System;

namespace WeightKernel
{
    public static class VFS
    {
        public static CosmosVFS FileSystem;

        public static void Initialize()
        {
            Console.WriteLine("[INIT] Virtual File Systems");
            FileSystem = new CosmosVFS();
            VFSManager.RegisterVFS(FileSystem);
            FileSystem.Initialize(true);
            Console.WriteLine("[OK] Virtual File Systems");
        }

        public static void FormatDrive(int driveIndex)
        {
            try
            {
                List<Sys.FileSystem.Disk> disks = VFSManager.GetDisks();
                if (driveIndex < 0 || driveIndex >= disks.Count)
                {
                    Console.WriteLine("Drive does not exist.");
                    return;
                }

                var disk = disks[driveIndex];

                if (disk.Partitions == null || disk.Partitions.Count == 0)
                {
                    Console.WriteLine("No partitions found on this drive.");
                    return;
                }

                Console.WriteLine($"Formatting partition 0 on drive {driveIndex} as FAT32…");
                disk.FormatPartition(0, "FAT32", true);
                Console.WriteLine("Format complete!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error formatting drive: " + e.Message);
            }
        }
    }
}
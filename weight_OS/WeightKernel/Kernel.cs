using System;
using Sys = Cosmos.System;

namespace WeightKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();
            try
            {
                VFS.Initialize();
            }
            catch (Exception ex)
            {
                Panic.Trigger("VFS initialization failed: " + ex.Message);
            }
            Console.WriteLine("welcome to weightOS!");
            Console.WriteLine("Type 'help' for commands.");
        }

        protected override void Run()
        {
            try
            {
                Shell shell = new Shell();
                shell.Run();
            }
            catch (Exception ex)
            {
                Panic.Trigger(ex.Message);
            }
        }
    }
}
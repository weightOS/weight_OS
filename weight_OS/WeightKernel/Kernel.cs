using System;
using Sys = Cosmos.System;

namespace WeightKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();
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
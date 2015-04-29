using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2012Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = RunTests(args);

            Environment.Exit(exitCode);
        }

        private static int RunTests(string[] args)
        {
            var arguments = new Arguments(args);
            var runSettingsFile = new RunSettingsFile(arguments);
            var testRunner = new TestRunner(arguments, runSettingsFile);

            var exitCode = testRunner.Run();

            if (exitCode == 0)
                testRunner.CopyOutputFile(arguments.AssemblyToTest.Replace(".dll", string.Empty) + ".trx");

            return exitCode;
        }
    }
}

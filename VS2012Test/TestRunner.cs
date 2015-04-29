using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2012Test
{
    public class TestRunner
    {
        private readonly Arguments _arguments;
        private readonly RunSettingsFile _runSettingsFile;

        public TestRunner(Arguments arguments, RunSettingsFile runSettingsFile)
        {
            _runSettingsFile = runSettingsFile;
            _arguments = arguments;
        }

        public int Run()
        {
            _runSettingsFile.Create();

            var process = new Process();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = _arguments.VsTestPath;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = _arguments.ArgumentsAsString;

            try
            {
                process.Start();
                process.WaitForExit(120000);
            }
            finally
            {
                ExitCode = process.ExitCode;
            }

            return ExitCode;
        }

        public void CopyOutputFile(string fileName)
        {
            //Debugger.Launch();
            var trxFile = _arguments.OutputFolder.GetFiles("*.trx").OrderByDescending(f => f.LastWriteTimeUtc).Last();

            File.Copy(trxFile.FullName, Path.Combine(_arguments.OutputFolder.FullName, fileName));
        }

        public int ExitCode { get; private set; }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VS2012Test.Tests
{
    [TestClass]
    public class RunSettingsFileTests
    {
        [TestMethod]
        public void Creates_settings_file_from_arguments()
        {
            var dirName = Guid.NewGuid();

            var args = new[] { "-assembly", "assemblyVal", 
                "-outputFolder", "OutputToFolder",
                "-settingsFile", string.Format(@"{0}\SettingsFile.runsettings", dirName) };

            var arguments = new Arguments(args);

            try
            {
                var runSettingsFile = new RunSettingsFile(arguments);

                runSettingsFile.Create();

                Assert.IsTrue(arguments.SettingsFile.Exists);
            }
            finally
            {
                if (arguments.SettingsFile.Exists)
                    arguments.SettingsFile.Directory.Delete(true);
            }
        }
    }
}

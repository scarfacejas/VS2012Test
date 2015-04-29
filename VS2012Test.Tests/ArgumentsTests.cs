using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VS2012Test.Tests
{
    [TestClass]
    public class ArgumentsTests
    {
        [TestMethod]
        public void Arguments_should_fill_properties()
        {
            var args = new[] { "-assembly", "assemblyVal", "-outputFolder", "outputFolderVal", "-settingsFile", "settingsFileVal" };
            var arguments = new Arguments(args);

            Assert.AreEqual("assemblyVal", arguments.AssemblyToTest);
            Assert.AreEqual("outputFolderVal", arguments.OutputFolder.Name);
            Assert.AreEqual("settingsFileVal", arguments.SettingsFile.Name);
            Assert.AreEqual(@"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe", arguments.VsTestPath);
        }
    }
}

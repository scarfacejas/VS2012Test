using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VS2012Test
{
    public class Arguments
    {
        public Arguments(string[] args)
        {
            SetProperty(args, () => this.AssemblyToTest, "assembly");
            SetProperty(args, () => this.OutputFolder, "outputFolder");
            SetProperty(args, () => this.SettingsFile, "settingsFile", false);
            SetCommandLineValues();
        }

        private void SetCommandLineValues()
        {
            var appSettings = new AppSettingsReader();

            VsTestPath = (string)appSettings.GetValue("vcPath", typeof(string));
            ArgumentsAsString = string.Format("{0} /settings:{1} /Logger:trx", AssemblyToTest, SettingsFile);
            CommandLine = string.Format("{0} {1}", VsTestPath, ArgumentsAsString);
        }

        private void SetProperty<T>(string[] args, Expression<Func<T>> property, string arg, bool required = true)
        {
            arg = "-" + arg;

            for (int i = 0; i < args.Length; i += 2)
            {
                if (args[i] != arg) continue;

                var argValue = args[i + 1];
                var propertyInfo = ((MemberExpression)property.Body).Member as PropertyInfo;
                object o = null;
                var propertyType = propertyInfo.PropertyType;

                if (propertyType == typeof(string))
                {
                    o = argValue;
                }
                else if (propertyType == typeof(FileInfo))
                {
                    o = new FileInfo(argValue);
                }
                else if (propertyType == typeof(DirectoryInfo))
                {
                    o = new DirectoryInfo(argValue);
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Property type {0} not expected.", propertyType));
                }

                propertyInfo.SetValue(this, o);

                return;
            }

            if (required)
                throw new InvalidOperationException(string.Format("Arg {0} not found.", arg));
        }

        public string CommandLine { get; private set; }
        public string ArgumentsAsString { get; private set; }
        public string VsTestPath { get; private set; }
        public string AssemblyToTest { get; private set; }
        public DirectoryInfo OutputFolder { get; private set; }
        public FileInfo SettingsFile { get; private set; }
    }
}

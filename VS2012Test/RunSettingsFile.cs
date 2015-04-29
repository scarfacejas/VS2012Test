using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2012Test
{
    public class RunSettingsFile
    {
        private readonly Arguments _arguments;

        public RunSettingsFile(Arguments arguments)
        {
            _arguments = arguments;
        }

        public void Create()
        {
            if(!_arguments.SettingsFile.Directory.Exists)
                _arguments.SettingsFile.Directory.Create();

            if (!_arguments.SettingsFile.Exists)
                using (var stream = _arguments.SettingsFile.CreateText())
                    stream.Write(SettingsText);

            _arguments.SettingsFile.Refresh();
        }

        private string SettingsText
        {
            get
            {
                var text = Properties.Resources.RunSettingsFileContents;

                text = text.Replace("@@OutputFolder@@", _arguments.OutputFolder.FullName);

                return text;
            }
        }
    }
}

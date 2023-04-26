using Microsoft.Win32;
using System;

namespace SlamReborn.Core
{
    public class FileDialogService : IFileDialogService
    {
        private string _defaultPath = "";
        public string Browse()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = _defaultPath;
            //ofd.Filter = "*.mp3";
            ofd.DefaultExt = ".mp3";
            ofd.Multiselect = false;
            // ofd.RestoreDirectory = true;

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                string filename = ofd.FileName; // here consist of full path to file

                return filename;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

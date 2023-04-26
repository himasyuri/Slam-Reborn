using System.Linq;
using System.Management;

namespace SlamReborn.Core.SourceEngine
{
    public class Initializer
    {
        //TODO: wmi 
        //TODO: init method
        private string GetFilePathName(string processName)
        {
            string wmiQuery = "Select * from Win32_Process Where Name = \"\"\" & ProcessName & \".exe\"\"";
            using (var searcher = new ManagementObjectSearcher(wmiQuery))
            {
                using (var results = searcher.Get())
                {
                    var Process = results.Cast<ManagementObject>().FirstOrDefault();
                    if (Process != null)
                    {
                        var exePath = Process["ExecutablePath"];

                        if (exePath != null)
                        {
                            return exePath.ToString();
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}

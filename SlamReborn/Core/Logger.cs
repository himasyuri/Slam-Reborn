using SlamReborn.Properties;
using System;
using System.IO;

namespace SlamReborn.Core
{
    public class Logger
    {
        public void LogError(Exception ex)
        {
            if (Settings.Default.LogError)
            {
                using (var log = new StreamWriter("errorlog.txt", true))
                {
                    log.WriteLine("--------------------{0} UTC--------------------", DateTime.UtcNow);
                    log.WriteLine(ex.ToString());
                }
            }
        }
    }
}

using System.Collections.Generic;

namespace SlamReborn.Models
{
    public class Track
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new List<string>();

        public string HotKey { get; set; } = string.Empty;

        public int Volume { get; set; } = 100;

        public int StartPos { get; set; }

        public int EndPos { get; set; }
    }
}

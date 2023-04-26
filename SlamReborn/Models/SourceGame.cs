using System.Collections.Generic;

namespace SlamReborn.Models
{
    public class SourceGame
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Directory { get; set; } = string.Empty;

        public string LibraryName { get; set; } = string.Empty;

        public string ToCfg { get; set; } = string.Empty;

        public bool VoiceFadeOut { get; set; } = true;

        public int Channels { get; } = 1;

        public string FileExtension { get; } = ".wav";

        public int SampleRate { get; set; } = 11025;

        public int Bits { get;} = 16;

        public string Exename { get; set; } = string.Empty;

        public int PollInterval { get; } = 100;

        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<string> BlackList { get; set; } = new List<string>() { "slam", "slam_listtracks", "list",
            "tracks", "la", "slam_play", "slam_play_on",
            "slam_play_off", "slam_updatecfg", "slam_curtrack",
            "slam_saycurtrack", "slam_sayteamcurtrack" };
    }
}

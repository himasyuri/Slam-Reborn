using SlamReborn.Models;
using SlamReborn.Properties;
using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.IO;

namespace SlamReborn.Core.SourceEngine
{
    public class Cfg
    {
        private readonly Logger _logger;

        public Cfg(Logger logger)
        {
            _logger = logger;
        }

        public void CreateCfgFiles(SourceGame game)
        {
            string gameDir = Path.Combine(Settings.Default.Steamapps, game.Directory);
            string gameCfgFolder = Path.Combine(gameDir, game.ToCfg);

            //check directory
            if (!Directory.Exists(gameCfgFolder))
            {
                throw new Exception();
            }
            CreateSlamCfg(game);
            CreateTracklistCfg(game);
        }

        //create slam config 
        private void CreateSlamCfg(SourceGame game)
        {
            TracksStore tracks = new TracksStore();
            using (var slamCfg = new StreamWriter(Settings.Default.Steamapps + game.Directory
                                                + game.ToCfg + "slam.cfg", false))
            {
                slamCfg.WriteLine("alias slam_listtracks \"\"exec slam_tracklist.cfg\"\"");
                slamCfg.WriteLine("alias list slam_listtracks");
                slamCfg.WriteLine("alias tracks slam_listtracks");
                slamCfg.WriteLine("alias la slam_listtracks");
                slamCfg.WriteLine("alias slam_play slam_play_on");
                slamCfg.WriteLine("alias slam_play_on \"\"alias slam_play slam_play_off; voice_inputfromfile 1; voice_loopback 1; +voicerecord\"\"");
                slamCfg.WriteLine("alias slam_play_off \"\"-voicerecord; voice_inputfromfile 0; voice_loopback 0; alias slam_play slam_play_on\"\"");
                slamCfg.WriteLine("alias slam_updatecfg \"\"host_writeconfig slam_relay\"\"");

                if (Settings.Default.HoldToPlay)
                {
                    slamCfg.WriteLine("alias +slam_hold_play slam_play_on");
                    slamCfg.WriteLine("alias -slam_hold_play slam_play_off");
                    slamCfg.WriteLine("bind {0} +slam_hold_play", Settings.Default.PlayKey);
                }
                else
                {
                    slamCfg.WriteLine("bind {0} slam_play", Settings.Default.PlayKey);
                }
                slamCfg.WriteLine("alias slam_curtrack \"\"exec slam_curtrack.cfg\"\"");
                slamCfg.WriteLine("alias slam_saycurtrack \"\"exec slam_saycurtrack.cfg\"\"");
                slamCfg.WriteLine("alias slam_sayteamcurtrack \"\"exec slam_sayteamcurtrack.cfg\"\"");

                foreach (var track in game.Tracks)
                {
                    game.Tracks = tracks.Get(game);
                    int index = game.Tracks.IndexOf(track);
                    slamCfg.WriteLine("alias {0} \"\"bind {1} {0}; slam_updatecfg; echo Loaded: {2}\"\"", index + 1, Settings.Default.RelayKey, track.Name);

                    foreach (var trackTag in track.Tags)
                    {
                        slamCfg.WriteLine("alias {0} \"\"bind {1} {2}; slam_updatecfg; echo Loaded: {3}\"\"", trackTag, Settings.Default.RelayKey, index + 1, track.Name);
                    }

                    if (string.IsNullOrEmpty(track.HotKey))
                    {
                        slamCfg.WriteLine("bind {0} \"\"bind {1} {2}; slam_updatecfg; echo Loaded: {3}\"\"", track.HotKey, Settings.Default.RelayKey, index + 1, track.Name);
                    }
                }

                string cfgData = "voice_enable 1; voice_modenable 1; voice_forcemicrecord 0; con_enable 1";

                if (game.VoiceFadeOut)
                {
                    cfgData += "; voice_fadeouttime 0.0";
                }
                slamCfg.WriteLine(cfgData);
            }
        }

        //create tracklist config
        private void CreateTracklistCfg(SourceGame game)
        {
            TracksStore tracks = new TracksStore();
            game.Tracks = tracks.Get(game);
            
            using (var slamTracklist = new StreamWriter(Settings.Default.Steamapps + game.Directory
                                                 + game.ToCfg + "slam_tracklist.cfg", false))
            {
                slamTracklist.WriteLine("echo \"\"You can select tracks either by typing a tag, or their track number.\"\"");
                slamTracklist.WriteLine("echo \"\"--------------------Tracks--------------------\"\"");

                foreach (var track in game.Tracks)
                {
                    int index = game.Tracks.IndexOf(track);

                    if (Settings.Default.WriteTags)
                    {
                        slamTracklist.WriteLine("echo \"\"{0}. {1} [{2}]\"\"", index + 1, track.Name, "'" + string.Join("', '", track.Tags) + "'");
                    }
                    else
                    {
                        slamTracklist.WriteLine("echo \"\"{0}. {1}\"\"", index + 1, track.Name);
                    }
                }
                slamTracklist.WriteLine("echo \"\"----------------------------------------------\"\"");
            }
        }

        public void LoadTrackCfg(SourceGame game, string trackName)
        {
            string gameCfgFolder = Path.Combine(Settings.Default.Steamapps, game.Directory, game.ToCfg);
            using (var slamCurTrack = new StreamWriter(gameCfgFolder + "slam_curtrack.cfg", false))
            {
                slamCurTrack.WriteLine("echo \"\"[SLAM] Track name: {0}\"\"", trackName);
            }
            using (var slamSayCurTrack = new StreamWriter(gameCfgFolder + "slam_saycurtrack.cfg", false))
            {
                slamSayCurTrack.WriteLine("say \"\"[SLAM] Track name: {0}\"\"", trackName);
            }
            using ( var slamSayTeamCurTrack = new StreamWriter(gameCfgFolder + "slam_sayteamcurtrack.cfg", false))
            {
                slamSayTeamCurTrack.WriteLine("say_team \"\"[SLAM] Track name: {0}\"\"", trackName);
            }

        }

        public string UserDataCfg(SourceGame game, string userDataPath)
        {
            if (Directory.Exists(userDataPath))
            {
                foreach (string userDir in Directory.GetDirectories(userDataPath))
                {
                    var cfgPath = Path.Combine(userDir, game.Id.ToString() + "\\local\\cfg\\slam_relay.cfg");
                    if (File.Exists(cfgPath))
                    {
                        return cfgPath;
                    }
                }
            }

            return string.Empty;
        }

        public void TagsCfg()
        {

        }

        //delete slam configs
        public void DeleteCfgs(SourceGame game, string steamApps)
        {
            List<string> slamFiles = new List<string> { "slam.cfg", "slam_tracklist.cfg", "slam_relay.cfg",
                "slam_curtrack.cfg", "slam_saycurtrack.cfg", "slam_sayteamcurtrack.cfg" };
            string gameDir = Path.Combine(steamApps, game.Directory);
            string gameCfgFolder = Path.Combine(gameDir, game.ToCfg);
            string voiceFile = Path.Combine(steamApps, game.Directory) + "voice_input.wav";

            try
            {
                if (File.Exists(voiceFile))
                {
                    File.Delete(voiceFile);
                }

                foreach (string slamFile in slamFiles)
                {
                    if (File.Exists(gameCfgFolder + slamFile))
                    {
                        File.Delete(gameCfgFolder + slamFile);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
            }
        }
    }
}

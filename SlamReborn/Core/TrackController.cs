using SlamReborn.Core.SourceEngine;
using SlamReborn.Models;
using SlamReborn.Stores;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SlamReborn.Core
{
    public class TrackController : ITrackController
    {
        private readonly Cfg _cfgController;
        private readonly Converter _converter;
        private readonly Logger _logger;

        public TrackController(Cfg cfg, Converter converter, Logger logger)
        {
            _cfgController = cfg;
            _converter = converter;
            _logger = logger;
        }
        public void AddTag()
        {
            throw new NotImplementedException();
        }

        public async Task Load(SourceGame game, string steamappsPath, string trackName)
        {
            TracksStore tracksStore = new TracksStore();
            Track? track;
            var tracks = tracksStore.Get(game);
            if (tracks.Count <= 0)
            {
                track = null;
            }
            else
            {
                track = tracks.SingleOrDefault(p => p.Name == Path.GetFileNameWithoutExtension(trackName));
            }
            
            try
            {
                string trackFile = steamappsPath + game.Directory + game.LibraryName + Path.GetFileNameWithoutExtension(trackName) + game.FileExtension;
                string voiceFile = Path.Combine(steamappsPath, game.Directory) + "voice_input.wav";
                if (track != null)
                {
                    if (File.Exists(voiceFile))
                    {
                        File.Delete(voiceFile);
                    }

                    if (File.Exists(trackFile))
                    {
                        if (track.Volume == 100 & track.StartPos <= 0 & track.EndPos <= 0)
                        {
                            File.Copy(trackFile, voiceFile, true);
                        }
                        //edit with ffmpeg and trim

                        //config
                        _cfgController.LoadTrackCfg(game, track.Name);
                    }
                }
                else
                {
                    await _converter.ConvertToWavAsync(trackName, trackFile);

                    if (File.Exists(trackFile))
                    {
                       // _cfgController.LoadTrackCfg(game, track.Name);
                        File.Copy(trackFile, voiceFile, true);

                        //config
                        _cfgController.LoadTrackCfg(game, Path.GetFileName(trackFile));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public async Task LoadAsync(SourceGame game, string steamappsPath, int index)
        {
            Track track;
            if (game.Tracks.Count > index)
            {
                track = game.Tracks[index];
                string voiceFile = Path.Combine(steamappsPath, game.Directory) + "voice_input.wav";
                try
                {
                    if (File.Exists(voiceFile))
                    {
                        File.Delete(voiceFile);
                    }

                    string trackFile = game.LibraryName + track.Name + game.FileExtension;

                    if (File.Exists(trackFile))
                    {
                        if (track.Volume == 100 & track.StartPos <= 0 & track.EndPos <= 0)
                        {
                            File.Copy(trackFile, voiceFile);
                        }
                        //edit with ffmpeg and trim

                        //config
                        _cfgController.LoadTrackCfg(game, track.Name);
                    }
                    else
                    {
                        string trackName = game.LibraryName + track.Name;
                        await _converter.ConvertToWavAsync(trackName, trackFile);
                        if (File.Exists(trackFile))
                        {
                            File.Copy(trackFile, voiceFile);

                            //config
                            _cfgController.LoadTrackCfg(game, track.Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                }
            }
        }

        public void RefreshTrackList()
        {
            throw new NotImplementedException();
        }

        public void Reload(SourceGame game)
        {
            if (Directory.Exists(game.LibraryName))
            {
                game.Tracks.Clear();

                foreach (var file in Directory.GetFiles(game.LibraryName))
                {
                    if (game.FileExtension == Path.GetExtension(file))
                    {
                        Track track = new Track { Name = Path.GetFileNameWithoutExtension(file) };
                        game.Tracks.Add(track);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(game.LibraryName);
            }
        }

        public void RemoveTag()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

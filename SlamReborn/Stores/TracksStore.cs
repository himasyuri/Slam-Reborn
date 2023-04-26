using SlamReborn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SlamReborn.Properties;

namespace SlamReborn.Stores
{
    public class TracksStore
    {
        private readonly List<Track> _tracks = new List<Track>();
        const string searchPattern = "*.wav";

        public IEnumerable<Track> Tracks => _tracks;

        public event Action TracksLoaded;
        public event Action TrackAdded;
        public event Action TrackUpdated;
        public event Action TrackDeleted;

        public void Load(SourceGame game)
        {
            _tracks.Clear();

            string path = Settings.Default.Steamapps + game.Directory + game.LibraryName;
            var tracks = Directory.GetFiles(path, searchPattern);

            foreach (var item in tracks)
            {
                string trackName = Path.GetFileNameWithoutExtension(item);
                Track track = new Track { Name = trackName };
                _tracks.Add(track);
            }

            TracksLoaded?.Invoke();
        }

        public List<Track> Get(SourceGame game)
        {
            _tracks.Clear();

            string path = Settings.Default.Steamapps + game.Directory + game.LibraryName;
            var tracks = Directory.GetFiles(path, searchPattern);

            foreach (var item in tracks)
            {
                string trackName = Path.GetFileNameWithoutExtension(item);
                Track track = new Track { Name = trackName };
                _tracks.Add(track);
            }

            return _tracks;
        }

        public async Task Add(string game, string trackName)
        {
            string path = Path.Combine(Properties.Settings.Default.Steamapps, game);
        }

        public async Task Update()
        {

        }

        public async Task Delete(string game, string trackName)
        {

        }
    }
}

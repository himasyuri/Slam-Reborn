using SlamReborn.Core.SourceEngine;
using SlamReborn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Core
{
    public interface ITrackController
    {
        //public Task LoadAsync(SourceGame game, string steamappsPath, int index);

        public Task Load(SourceGame game, string steamappsPath, string trackName);

        public void AddTag();

        public void RemoveTag();

        public void Save();

        public void Reload(SourceGame game);

        public void RefreshTrackList();
    }
}

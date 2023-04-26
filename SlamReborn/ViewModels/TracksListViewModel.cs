using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.ViewModels
{
    public class TracksListViewModel : ViewModelBase
    {
        private readonly TracksStore _tracksStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ObservableCollection<TracksListingItemViewModel> _tracksListItemViewModels;
        
        public IEnumerable<TracksListingItemViewModel> TracksListItemViewModels => _tracksListItemViewModels;


        public TracksListViewModel(TracksStore tracksStore, ModalNavigationStore modalNavigationStore)
        {
            _tracksStore = tracksStore;
            _modalNavigationStore = modalNavigationStore;
            _tracksListItemViewModels = new ObservableCollection<TracksListingItemViewModel>();
            _tracksStore.TracksLoaded += TracksStoreTracksLoaded;
        }

        protected override void Dispose()
        {
            _tracksStore.TracksLoaded -= TracksStoreTracksLoaded;

            base.Dispose();
        }

        private void TracksStoreTracksLoaded()
        {
            _tracksListItemViewModels.Clear();

            foreach (var track in _tracksStore.Tracks)
            {
                AddTracks(track);
            }
        }

        //private void TracksStoreTrackDelted()
        //{

        //}

        private void AddTracks(Track track)
        {
            TracksListingItemViewModel itemViewModel =
                new TracksListingItemViewModel(track, _modalNavigationStore);
            _tracksListItemViewModels.Add(itemViewModel);
        }
    }
}

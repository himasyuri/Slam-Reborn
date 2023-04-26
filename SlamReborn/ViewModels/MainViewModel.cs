using SlamReborn.Commands;
using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SlamReborn.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentModalViewModal => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public StartViewModel StartViewModel { get; }

        public MainViewModel(GamesStore gamesStore, SelectedGameStore selectedGame, 
                            ModalNavigationStore modalNavigationStore,
                            IFileDialogService fileDialogService, ITrackController trackController,
                            YTDownloader downloader, StartViewModel startViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            StartViewModel = startViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStoreCurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStoreCurrentViewModelChanged;

            base.Dispose();
        }

        private void ModalNavigationStoreCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModal));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}

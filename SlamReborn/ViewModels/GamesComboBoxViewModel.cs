using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.ViewModels
{
    public class GamesComboBoxViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GamesStore _gamesStore;
        private readonly SelectedGameStore _selectedGameStore;
        private readonly ObservableCollection<GamesComboBoxItemViewModel> _gamesComboBoxItemViewModels;

        public IEnumerable<GamesComboBoxItemViewModel> GamesComboBoxItemViewModels => _gamesComboBoxItemViewModels;

        public GamesComboBoxItemViewModel SelectedGameComboBoxItemViewModel
        {
            get
            {
                return _gamesComboBoxItemViewModels.
                    FirstOrDefault(y => y.SourceGame?.Id == _selectedGameStore.SelectedGame?.Id);
            }
            set
            {
                _selectedGameStore.SelectedGame = value?.SourceGame;
            }
        }

        public GamesComboBoxViewModel(GamesStore gamesStore, SelectedGameStore selectedGameStore,
            ModalNavigationStore modalNavigationStore)
        {
            _gamesStore = gamesStore;
            _selectedGameStore = selectedGameStore;
            _modalNavigationStore = modalNavigationStore;
            _gamesComboBoxItemViewModels = new ObservableCollection<GamesComboBoxItemViewModel>();

            _selectedGameStore.SelectedGameChanged += SelectedGameStoreSelectedGameChanged;

            _gamesStore.GamesLoaded += GameStoreGameLoaded;

            _gamesComboBoxItemViewModels.CollectionChanged += ComboBoxItemViewModelsCollectionChanged;
        }

        protected override void Dispose()
        {
            _selectedGameStore.SelectedGameChanged -= SelectedGameStoreSelectedGameChanged;

            _gamesStore.GamesLoaded -= GameStoreGameLoaded;

            _gamesComboBoxItemViewModels.CollectionChanged -= ComboBoxItemViewModelsCollectionChanged;

            base.Dispose();
        }

        private void SelectedGameStoreSelectedGameChanged()
        {
            OnPropertyChanged(nameof(SelectedGameComboBoxItemViewModel));
        }

        private void GameStoreGameLoaded()
        {
            _gamesComboBoxItemViewModels.Clear();

            foreach (SourceGame game in _gamesStore.Games)
            {
                AddGame(game);
            }
        }

        private void ComboBoxItemViewModelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedGameComboBoxItemViewModel));
        }

        private void AddGame(SourceGame game)
        {
            GamesComboBoxItemViewModel itemViewModel =
                new GamesComboBoxItemViewModel(game);
            _gamesComboBoxItemViewModels.Add(itemViewModel);
        }
    }
}

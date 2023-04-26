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
    public class StartViewModel : ViewModelBase
    {
        public GamesComboBoxViewModel GamesComboBoxViewModel { get; }
        public TracksListViewModel TracksListViewModel { get; }

        public ICommand ImportMusicCommand { get; }
        public ICommand YouTubeImportCommand { get; }
        public ICommand LoadStartViewCommand { get; }
        public ICommand LoadStartViewAsyncCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand StartCommand { get; }

        public StartViewModel(GamesStore gamesStore, SelectedGameStore selectedGame, 
            ModalNavigationStore modalNavigationStore, ITrackController trackController, IFileDialogService fileDialogService,
            YTDownloader downloader, TracksStore tracksStore)
        {
            GamesComboBoxViewModel = new GamesComboBoxViewModel(gamesStore, selectedGame, modalNavigationStore);
            TracksListViewModel = new TracksListViewModel(tracksStore, modalNavigationStore);

            LoadStartViewCommand = new LoadStartViewModelCommand(this, gamesStore);
            ImportMusicCommand = new OpenImportMusicCommand(modalNavigationStore, trackController, selectedGame, fileDialogService);
            StartCommand = new StartCommand(selectedGame);
            YouTubeImportCommand = new OpenYouTubeImportCommand(modalNavigationStore, downloader);
            OpenSettingsCommand = new OpenSettingsCommand(modalNavigationStore);
            LoadStartViewAsyncCommand = new LoadStartViewAsyncCommand(gamesStore);
        }

        public static StartViewModel LoadViewModel(GamesStore gamesStore, SelectedGameStore selectedGame,
            ModalNavigationStore modalNavigationStore, ITrackController trackController, IFileDialogService fileDialogService,
            YTDownloader downloader, TracksStore tracksStore)
        {
            StartViewModel viewModel = new StartViewModel(gamesStore, selectedGame, modalNavigationStore, trackController, fileDialogService, downloader, tracksStore);

            viewModel.LoadStartViewCommand.Execute(null);
            
            return viewModel;
        }
    }
}

using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Properties;
using SlamReborn.Stores;
using SlamReborn.ViewModels;
using System;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class ImportMusicCommand : AsyncCommandBase
    {
        private readonly ImportMusicViewModel _importMusicViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ITrackController _trackController;
        private readonly SourceGame _selectedGame;

        public ImportMusicCommand(ImportMusicViewModel importMusicViewModel, SelectedGameStore selectedGameStore,
                                  ModalNavigationStore modalNavigationStore, ITrackController trackController)
        {
            _importMusicViewModel = importMusicViewModel;
            _modalNavigationStore = modalNavigationStore;
            _trackController = trackController;
            _selectedGame = selectedGameStore.SelectedGame;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _importMusicViewModel.ErrorMessage = null;
            var trackName = _importMusicViewModel.FileName;

            try
            {
                await _trackController.Load(_selectedGame, Settings.Default.Steamapps, trackName);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                //logger works in Load methods (used defualt app logger)
            }
            finally
            {

            }
        }
    }
}

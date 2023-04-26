using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Stores;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class OpenImportMusicCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedGameStore _selectedGameStore;
        private readonly ITrackController _trackController;
        private readonly IFileDialogService _dialogService;

        public OpenImportMusicCommand(ModalNavigationStore modalNavigationStore, ITrackController trackController, 
            SelectedGameStore selectedGameStore, IFileDialogService fileDialog)
        {
            _modalNavigationStore = modalNavigationStore;
            _trackController = trackController;
            _selectedGameStore = selectedGameStore;
            _dialogService = fileDialog;
        }

        public override void Execute(object? parameter)
        {
            ImportMusicViewModel viewModel = new ImportMusicViewModel(_dialogService, _trackController, _selectedGameStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}

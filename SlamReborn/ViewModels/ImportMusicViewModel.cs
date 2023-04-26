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
    public class ImportMusicViewModel : ViewModelBase
    {
        private string _pathToMusic;
        private string _errorMessage;
        private string _fileName;

        private readonly IFileDialogService _dialogService;
        private readonly SelectedGameStore _selectedGameStore;

        private SourceGame SelectedGame => _selectedGameStore.SelectedGame;

        public bool HasSelectedGame => SelectedGame != null;

        public string PathToMusic
        {
            get
            {
                return _pathToMusic;
            }
            set
            {
                _pathToMusic = value;
                OnPropertyChanged(nameof(_pathToMusic));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        //public bool CanImport => string.IsNullOrEmpty

        public ICommand ImportCommand { get; }
        public ICommand OpenFileDialogCommand { get; }
        public ICommand CancelCommand { get; }

        public ImportMusicViewModel(IFileDialogService fileDialog, ITrackController trackController,
                                    SelectedGameStore selectedGameStore,
                                    ModalNavigationStore modalNavigationStore)
        {
            _selectedGameStore = selectedGameStore;

            OpenFileDialogCommand = new OpenFileDialogCommand(fileDialog, this);
            ImportCommand = new ImportMusicCommand(this, selectedGameStore, modalNavigationStore, trackController);
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            _selectedGameStore.SelectedGameChanged += SelectedGameStoreSelectedGameChanged;
        }

        protected override void Dispose()
        {
            _selectedGameStore.SelectedGameChanged -= SelectedGameStoreSelectedGameChanged;
            base.Dispose();
        }

        private void SelectedGameStoreSelectedGameChanged()
        {
            OnPropertyChanged(nameof(HasSelectedGame));
        }
    }
}

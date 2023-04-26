using SlamReborn.Commands;
using SlamReborn.Core;
using SlamReborn.Models;
using System.Windows.Input;

namespace SlamReborn.ViewModels
{
    public class YouTubeImportViewModel : ViewModelBase
    {
        private string _reference;

        public string Reference
        {
            get
            {
                return _reference;
            }
            set
            {
                _reference = value;
                OnPropertyChanged(nameof(Reference));
            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Reference);

        public ICommand DownloadCommand { get; }
        public ICommand CancelCommand { get; }

        public YouTubeImportViewModel(ModalNavigationStore modalNavigationStore, YTDownloader downloader)
        {
            DownloadCommand = new YouTubeImportCommand(downloader, this, modalNavigationStore);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }
    }
}

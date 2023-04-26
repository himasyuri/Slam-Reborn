using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SlamReborn.Commands
{
    public class OpenYouTubeImportCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly YTDownloader _downloader;

        public OpenYouTubeImportCommand(ModalNavigationStore modalNavigationStore, YTDownloader downloader)
        {
            _modalNavigationStore = modalNavigationStore;
            _downloader = downloader;
        }

        public override void Execute(object? parameter)
        {
            YouTubeImportViewModel viewModel = new YouTubeImportViewModel(_modalNavigationStore, _downloader);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}

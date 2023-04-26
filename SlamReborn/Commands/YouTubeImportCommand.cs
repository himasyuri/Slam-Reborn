using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class YouTubeImportCommand : AsyncCommandBase
    {
        private readonly YouTubeImportViewModel _youTubeImportViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly YTDownloader _downloader;

        public YouTubeImportCommand(YTDownloader downloader, YouTubeImportViewModel youTubeImportViewModel,
                                    ModalNavigationStore modalNavigationStore)
        {
            _downloader = downloader;
            _youTubeImportViewModel = youTubeImportViewModel;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var reference = _youTubeImportViewModel.Reference;

            //TODO: download music lasts a lot of time
            //and user doesnt know about this process(when its
            //starts and when its end
            try
            {
                await _downloader.DownloadVideoByMp3(reference);
                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
    }
}

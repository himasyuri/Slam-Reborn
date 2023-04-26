using SlamReborn.Core;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class OpenFileDialogCommand : CommandBase
    {
        private readonly ImportMusicViewModel _importMusicViewModel;
        private readonly IFileDialogService _dialogService;

        public OpenFileDialogCommand(IFileDialogService fileDialogService, ImportMusicViewModel importMusicViewModel)
        {
            _dialogService = fileDialogService;
            _importMusicViewModel = importMusicViewModel;
        }

        public override void Execute(object? parameter)
        {
            var result = OpenFileDialg();
            _importMusicViewModel.FileName = result;
        }

        private string OpenFileDialg()
        {
            return _dialogService.Browse();
        }
    }
}

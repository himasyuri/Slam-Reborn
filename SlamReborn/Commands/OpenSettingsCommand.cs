using SlamReborn.Models;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class OpenSettingsCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenSettingsCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            SettingsViewModel viewModel = new SettingsViewModel();
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}

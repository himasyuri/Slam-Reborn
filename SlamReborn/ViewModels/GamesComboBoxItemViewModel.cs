using SlamReborn.Core;
using SlamReborn.Models;
using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SlamReborn.ViewModels
{
    public class GamesComboBoxItemViewModel : ViewModelBase
    {
        private string _errorMessage;

        public SourceGame SourceGame { get; private set; }

        public string Name => SourceGame.Name;

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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public GamesComboBoxItemViewModel(SourceGame game)
        {
            SourceGame = game;
        }
    }
}

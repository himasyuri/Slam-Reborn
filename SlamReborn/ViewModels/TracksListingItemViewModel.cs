using SlamReborn.Core;
using SlamReborn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SlamReborn.ViewModels
{
    public class TracksListingItemViewModel : ViewModelBase
    {
        public Track Track { get; private set; }
        

        public string Name => Track.Name;

        private string _errorMesage;

        public string ErrorMesage
        {
            get
            {
                return _errorMesage;
            }
            set
            {
                _errorMesage = value;
                OnPropertyChanged(nameof(ErrorMesage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMesage);

        ICommand EditCommand { get; }
        ICommand DeleteCommand { get; }

        public TracksListingItemViewModel(Track track, ModalNavigationStore modalNavigationStore)
        {
            Track = track;
            
        }
    }
}

using SlamReborn.Commands;
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
    public class SettingsViewModel : ViewModelBase
    {

        public bool LogErrors
        {
            get
            {
                return Properties.Settings.Default.LogError;
            }
            set
            {
                Properties.Settings.Default.LogError = value;
            }
        }

        public bool HoldToPlay
        {
            get
            {
                return Properties.Settings.Default.HoldToPlay;
            }
            set
            {
                Properties.Settings.Default.HoldToPlay = value;
            }
        }

        public string RelayKey
        {
            get
            {
                return Properties.Settings.Default.RelayKey;
            }
            set
            {
                Properties.Settings.Default.RelayKey = value;
            }
        }

        public string PlayKey
        {
            get
            {
                return Properties.Settings.Default.PlayKey;
            }
            set
            {
                Properties.Settings.Default.PlayKey = value;
            }
        }

        public string SteamAppsPath
        {
            get
            {
                return Properties.Settings.Default.Steamapps;
            }
            set
            {
                Properties.Settings.Default.Steamapps = value;
            }
        }

        public string Userdata
        {
            get
            {
                return Properties.Settings.Default.Userdata;
            }
            set
            {
                Properties.Settings.Default.Userdata = value;
            }
        }

        public ICommand Confirm { get; }
        public ICommand Cancel { get; }

        public SettingsViewModel(ModalNavigationStore modalNavigationStore)
        {
            Cancel = new CloseModalCommand(modalNavigationStore);
            Confirm = new CloseModalCommand(modalNavigationStore);
        }
    }
}

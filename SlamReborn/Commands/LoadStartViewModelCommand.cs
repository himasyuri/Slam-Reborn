using SlamReborn.Stores;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class LoadStartViewModelCommand : CommandBase
    {
        private readonly StartViewModel _startViewModel;
        private readonly GamesStore _gamesStore;

        public LoadStartViewModelCommand(StartViewModel startViewModel, GamesStore gamesStore)
        {
            _startViewModel = startViewModel;
            _gamesStore = gamesStore;
        }

        public override void Execute(object? parameter)
        {
            _gamesStore.LoadGames();
        }
    }
}

using SlamReborn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Stores
{
    public class SelectedGameStore
    {
        private readonly GamesStore _gamesStore;
        private SourceGame _selectedGame; 

        public SourceGame SelectedGame
        {
            get
            {
                return _selectedGame;
            }
            set
            {
                _selectedGame = value;
                SelectedGameChanged?.Invoke();
            }
        }

        public event Action SelectedGameChanged;

        public SelectedGameStore(GamesStore gamesStore)
        {
            _gamesStore = gamesStore;
            _gamesStore.GameUpdated += GamesStoreUpdatedGame;
        }

        private void GamesStoreUpdatedGame(SourceGame game)
        {
            if (game.Id == SelectedGame?.Id) 
            {
                SelectedGame = game;
            }
        }
    }
}

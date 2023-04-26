using SlamReborn.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class LoadStartViewAsyncCommand : AsyncCommandBase
    {
        private readonly GamesStore _gameStore;

        public LoadStartViewAsyncCommand(GamesStore gameStore)
        {
            _gameStore = gameStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _gameStore.LoadGamesAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}

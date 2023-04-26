using SlamReborn.Core;
using SlamReborn.Core.SourceEngine;
using SlamReborn.Models;
using SlamReborn.Stores;
using SlamReborn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Commands
{
    public class StartCommand : CommandBase
    {
        private readonly Cfg _cfg;
        private readonly SelectedGameStore _store;
        private readonly Logger _logger;

        public StartCommand(SelectedGameStore selectedGameStore)
        {
            _store = selectedGameStore;
            _logger = new Logger();
            _cfg = new Cfg(_logger);
        }

        public override void Execute(object? parameter)
        {
            var selectedGame = _store.SelectedGame;
            _cfg.CreateCfgFiles(selectedGame);
        }
    }
}

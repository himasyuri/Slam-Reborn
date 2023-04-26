using SlamReborn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlamReborn.Stores
{
    public class GamesStore
    {
        private readonly List<SourceGame> _games;

        public IEnumerable<SourceGame> Games => _games;

        public event Action GamesLoaded;
        public event Action<SourceGame> GameUpdated;

        public GamesStore()
        {
            _games = new List<SourceGame>();
        }

        public void LoadGames()
        {
            _games.Clear();

            SourceGame csgo = new SourceGame
            {
                Name = "Counter-Strike: Global Offensive",
                Id = 730,
                Directory = "common\\Counter-Strike Global Offensive\\",
                ToCfg = "csgo\\cfg\\",
                LibraryName = "csgo\\",
                Exename = "csgo",
                SampleRate = 22050,
                VoiceFadeOut = false,
            };
            IEnumerable<string> buttons = new List<string>() { "attack", "attack2", "autobuy",
                "back", "buy", "buyammo1", "buyammo2", "buymenu", "callvote", "cancelselect",
                "cheer", "compliment", "coverme", "drop", "duck", "enemydown", "enemyspot",
                "fallback", "followme", "forward", "getout", "go", "holdpos", "inposition",
                "invnext", "invprev", "jump", "lastinv", "messagemode", "messagemode2",
                "moveleft", "moveright", "mute", "negative", "quit", "radio1", "radio2",
                "radio3", "rebuy", "regroup", "reload", "report", "reportingin", "roger",
                "sectorclear", "showscores", "slot1", "slot10", "slot2", "slot3", "slot4",
                "slot5", "slot6", "slot7", "slot8", "slot9", "speed", "sticktog", "takepoint",
                "takingfire", "teammenu", "thanks", "toggleconsole", "use", "voicerecord" };
            csgo.BlackList.AddRange(buttons);

            _games.Add(csgo);

            GamesLoaded?.Invoke();
        }

        public Task LoadGamesAsync()
        {
            _games.Clear();

            SourceGame csgo = new SourceGame
            {
                Name = "Counter-Strike: Global Offensive",
                Id = 730,
                Directory = "common\\Counter-Strike Global Offensive\\",
                ToCfg = "csgo\\cfg\\",
                LibraryName = "csgo\\",
                Exename = "csgo",
                SampleRate = 22050,
                VoiceFadeOut = false
            };
            IEnumerable<string> buttons = new List<string>() { "attack", "attack2", "autobuy",
                "back", "buy", "buyammo1", "buyammo2", "buymenu", "callvote", "cancelselect",
                "cheer", "compliment", "coverme", "drop", "duck", "enemydown", "enemyspot",
                "fallback", "followme", "forward", "getout", "go", "holdpos", "inposition",
                "invnext", "invprev", "jump", "lastinv", "messagemode", "messagemode2",
                "moveleft", "moveright", "mute", "negative", "quit", "radio1", "radio2",
                "radio3", "rebuy", "regroup", "reload", "report", "reportingin", "roger",
                "sectorclear", "showscores", "slot1", "slot10", "slot2", "slot3", "slot4",
                "slot5", "slot6", "slot7", "slot8", "slot9", "speed", "sticktog", "takepoint",
                "takingfire", "teammenu", "thanks", "toggleconsole", "use", "voicerecord" };
            csgo.BlackList.AddRange(buttons);

            _games.Add(csgo);

            GamesLoaded?.Invoke();

            return Task.CompletedTask;
        }

        //private List<Track> GetTracks()
        //{

        //}
    }
}

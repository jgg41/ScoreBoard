using System;
using System.Collections.Generic;

namespace ScoreBoard
{
    public class Service
    {
        public SortedList<string, Game> Games = new SortedList<string, Game>();

        public void StartGame(Game game)
        {
            ValidateGame(game);

            var key = $"{game.HomeTeam} - {game.AwayTeam}";

            if (GameExists(key)) throw new Exception("Game already started!");

            Games.Add(key, game);
        }

        public void FinishGame(string key)
        {
            if (!GameExists(key)) throw new Exception("Game does not exist!");

            Games.Remove(key);
        }

        private void ValidateGame(Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.HomeTeam) || string.IsNullOrWhiteSpace(game.AwayTeam))
            {
                throw new ArgumentNullException();
            }
        }

        private bool GameExists(string key)
        {
            return Games.ContainsKey(key);
        }
    }
}

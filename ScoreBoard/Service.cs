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
            CheckIfGameAlreadyStarted($"{game.HomeTeam} - {game.AwayTeam}");

            Games.Add($"{game.HomeTeam} - {game.AwayTeam}", game);
        }

        private void ValidateGame(Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.HomeTeam) || string.IsNullOrWhiteSpace(game.AwayTeam))
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckIfGameAlreadyStarted(string key)
        {
            if (Games.ContainsKey(key))
            {
                throw new Exception("Game already started!");
            }
        }
    }
}

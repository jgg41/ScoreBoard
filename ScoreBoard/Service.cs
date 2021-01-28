using System;
using System.Collections.Generic;

namespace ScoreBoard
{
    public class Service
    {
        public SortedList<string, Game> Games = new SortedList<string, Game>();

        public void StartGame(Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.HomeTeam) || string.IsNullOrWhiteSpace(game.AwayTeam))
            {
                throw new ArgumentNullException();
            }

            if (Games.ContainsKey($"{game.HomeTeam} - {game.AwayTeam}"))
            {
                throw new Exception("Game already started!");
            }

            Games.Add($"{game.HomeTeam} - {game.AwayTeam}", game);
        }
    }
}

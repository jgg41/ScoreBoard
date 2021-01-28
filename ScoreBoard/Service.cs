using System;
using System.Collections.Generic;

namespace ScoreBoard
{
    public class Service
    {
        public SortedList<string, Game> Games = new SortedList<string, Game>();

        public void StartGame(Game game)
        {
            ValidationExtensions.ValidateGame(game);

            var key = $"{game.HomeTeam} - {game.AwayTeam}";

            if (ValidationExtensions.GameExists(key, Games)) throw new Exception("Game already started!");

            Games.Add(key, game);
        }

        public void FinishGame(string key)
        {
            ValidationExtensions.ValidateKey(key);

            if (!ValidationExtensions.GameExists(key, Games)) throw new Exception("Game does not exist!");

            Games.Remove(key);
        }

        public void UpdateGame(string key, int homeTeamScore, int awayTeamScore)
        {
            ValidationExtensions.ValidateKey(key);
            ValidationExtensions.ValidateScores(homeTeamScore, awayTeamScore);

            var gameToUpdate = Games[key];
            if(gameToUpdate == null) throw new Exception();

            gameToUpdate.HomeTeamScore = homeTeamScore;
            gameToUpdate.AwayTeamScore = awayTeamScore;
            gameToUpdate.TotalScore = homeTeamScore + awayTeamScore;
        }
    }
}

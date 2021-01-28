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
            ValidateKey(key);

            if (!GameExists(key)) throw new Exception("Game does not exist!");

            Games.Remove(key);
        }

        public void UpdateGame(string key, int homeTeamScore, int awayTeamScore)
        {
            ValidateKey(key);
            ValidateScores(homeTeamScore, awayTeamScore);

            var gameToUpdate = Games[key];
            if(gameToUpdate == null) throw new Exception();

            gameToUpdate.HomeTeamScore = homeTeamScore;
            gameToUpdate.AwayTeamScore = awayTeamScore;
            gameToUpdate.TotalScore = homeTeamScore + awayTeamScore;
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

        private void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }
        }

        private void ValidateScores(int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamScore < 0 || awayTeamScore < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}

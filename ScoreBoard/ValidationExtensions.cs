using System;
using System.Collections.Generic;

namespace ScoreBoard
{
    public static class ValidationExtensions
    {
        public static void ValidateGame(Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.HomeTeam) || string.IsNullOrWhiteSpace(game.AwayTeam))
            {
                throw new ArgumentNullException();
            }
        }

        public static bool GameExists(string key, IReadOnlyDictionary<string, Game> games)
        {
            return games.ContainsKey(key);
        }

        public static void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }
        }

        public static void ValidateScores(int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamScore < 0 || awayTeamScore < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}

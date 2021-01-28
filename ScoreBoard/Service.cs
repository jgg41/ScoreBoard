
using System;

namespace ScoreBoard
{
    public class Service
    {
        public void StartGame(Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.HomeTeam) || string.IsNullOrWhiteSpace(game.AwayTeam))
            {
                throw new ArgumentNullException();
            }
        }
    }
}

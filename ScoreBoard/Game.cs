
using System;

namespace ScoreBoard
{
    public class Game
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int TotalScore { get; set; }

        public DateTime InsertedTime { get; set; }

        public Game()
        {
            InsertedTime = DateTime.UtcNow;
        }
    }
}

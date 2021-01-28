
using System;

namespace ScoreBoard
{
    public class Service
    {
        public void StartGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}

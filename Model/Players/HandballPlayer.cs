using CodeTestMostValuablePlayerProyect.Interface;

namespace CodeTestMostValuablePlayerProyect.Model.Players
{
    public class HandballPlayer : Player, IPlayer
    {
        public string PlayerName { get; set; }
        public string Nickname { get; set; }
        public int Number { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
        public int InitialRatingPoints { get; set; }
        public int GoalsMade { get; set; }
        public int GoalsReceived { get; set; }
        public int PointCounter
        {
            get
            {
                return InitialRatingPoints + GoalsMade * 5 - GoalsReceived * 2 + MatchWinCounter * 10;
            }
        }

    }
}

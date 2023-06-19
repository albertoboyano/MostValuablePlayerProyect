namespace CodeTestMostValuablePlayerProyect.Interface
{
    public interface IPlayer
    {
        public string PlayerName { get; set; }
        public string Nickname { get; set; }
        public int Number { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
        public int PointCounter { get; }

    }
}

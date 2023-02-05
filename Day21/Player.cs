namespace Day21
{
    internal class Player
    {
        public int Position { get; set; }
        public int Score { get; set; }

        public Player(int startPosition)
        {
            Position = startPosition;
            Score = 0;
        }

        public int Move(int moves)
        {
            Position += moves;

            while (Position > 10)   // can't use mod, Position = 1..10
                Position -= 10;

            return Position;
        }
    }
}

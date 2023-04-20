namespace Day19
{
    public record Coord(int x, int y, int z);

    public class Beacon : IEquatable<Beacon>
    {
        public int X { get; set; } 
        public int Y { get; set; }
        public int Z { get; set; }
        public Coord coord { get; set; }

        public Beacon(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            coord = new Coord(x, y, z);
        }

        public bool Equals(Beacon? other)
        {
            if (other is null)
                return false;

            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode() => (X, Y, Z).GetHashCode();

        public override string ToString()
        {
            return $"[{X,4},{Y,4},{Z,4}]";
        }
    }
}

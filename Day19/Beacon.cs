namespace Day19
{
    public record Vector(int x, int y, int z);

    public class Beacon : IEquatable<Beacon>
    {
        public int X { get; set; } 
        public int Y { get; set; }
        public int Z { get; set; }
        public Vector Location { get; set; }
        
        public Beacon(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Location = new Vector(x, y, z);
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Beacon);
        }
    }
}

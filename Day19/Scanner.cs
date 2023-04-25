namespace Day19
{
    public class Scanner
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<Beacon> Beacons { get; set; }
        public List<Vector> Vectors { get; set; }

        public Vector OriginOffset;
        public int Orientation;

        public Scanner()
        {
            Id = -1;
            X = -1;
            Y = -1;
            Z = -1;
            Beacons = new();
            Vectors = new();
            OriginOffset = new(0, 0, 0);
            Orientation = 0;
        }

        public Scanner(int id, List<Beacon> beacons)
        {
            Id = id;
            X = -1;
            Y = -1;
            Z = -1;
            Beacons = beacons;
            Vectors = CalculateVectors();
            OriginOffset = new(0, 0, 0);
            Orientation = 0;
        }

        private List<Vector> CalculateVectors()
        {
            //Console.Write($"Calculating vector relationships for scanner {Id,2}, ");
            //Console.WriteLine($"number of beacons seen {Beacons.Count}");

            List<Vector> rels = new();

            for (int i = 0; i < Beacons.Count; i++)
            {
                for (int j = i + 1; j < Beacons.Count; j++)
                {
                    List<int> diffs = new()
                    {
                        Math.Abs(Beacons[i].X - Beacons[j].X),
                        Math.Abs(Beacons[i].Y - Beacons[j].Y),
                        Math.Abs(Beacons[i].Z - Beacons[j].Z)
                    };
                    diffs.Sort();
                    Vector v = new(diffs[0], diffs[1], diffs[2]);
                    //Console.WriteLine(v.ToString());
                    rels.Add(v);
                }
            }
            
            return rels;
        }

        public bool IsPlaced()
        {
            if (X == -1 && Y == -1 && Z == -1)
                return false;
            else
                return true;
        }

        public void PrintScannerInfo()
        {
            Console.Write($"==== Scanner {Id,2}: ");
            Console.WriteLine($"[{X,6},{Y,6},{Z,6}]");
            Console.Write("Origin Offset  : ");
            Console.WriteLine($"[{OriginOffset.x,6},{OriginOffset.y,6},{OriginOffset.z,6}]");
        }

        public void PrintBeacons()
        {
            Console.WriteLine($"Beacons: {Beacons.Count}");
            foreach (Beacon b in Beacons)
                Console.WriteLine($"[{b.X,6},{b.Y,6},{b.Z,6}]");
            
            Console.WriteLine();
        }

#pragma warning disable 1717
        private Vector RotateVector(Vector co, int orientation)
        {
            var (x, y, z) = co;

            switch (orientation)
            {
                case  0: (x, y, z) = ( x,  y,  z); break;
                case  1: (x, y, z) = ( z,  y, -x); break;
                case  2: (x, y, z) = (-x,  y, -z); break;
                case  3: (x, y, z) = (-z,  y,  x); break;
                case  4: (x, y, z) = (-y,  x,  z); break;
                case  5: (x, y, z) = ( z,  x,  y); break;
                case  6: (x, y, z) = ( y,  x, -z); break;
                case  7: (x, y, z) = (-z,  x, -y); break;
                case  8: (x, y, z) = ( y, -x,  z); break;
                case  9: (x, y, z) = ( z, -x, -y); break;
                case 10: (x, y, z) = (-y, -x, -z); break;
                case 11: (x, y, z) = (-z, -x,  y); break;
                case 12: (x, y, z) = ( x, -z,  y); break;
                case 13: (x, y, z) = ( y, -z, -x); break;
                case 14: (x, y, z) = (-x, -z, -y); break;
                case 15: (x, y, z) = (-y, -z,  x); break;
                case 16: (x, y, z) = ( x, -y, -z); break;
                case 17: (x, y, z) = (-z, -y, -x); break;
                case 18: (x, y, z) = (-x, -y,  z); break;
                case 19: (x, y, z) = ( z, -y,  x); break;
                case 20: (x, y, z) = ( x,  z, -y); break;
                case 21: (x, y, z) = (-y,  z, -x); break;
                case 22: (x, y, z) = (-x,  z,  y); break;
                case 23: (x, y, z) = ( y,  z,  x); break;
            }

            return new Vector(x, y, z);
        }
#pragma warning restore

        // Rotate the remote scanner and return the number of common beacons
        public int RotateRemoteScanner(Scanner remoteScanner, int orientation)
        {
            // reorient beacons in remote scanner
            foreach (Beacon b in remoteScanner.Beacons)
            {
                Vector co = new(b.X, b.Y, b.Z);
                b.Location = RotateVector(co, orientation);
            }

            // compare each beacon of this scanner to each 
            // beacon of the remote scanner, add each comparison to a list
            List<Vector> diffs = new();
            for (int i = 0; i < Beacons.Count; i++)
            {
                for (int j = 0; j < remoteScanner.Beacons.Count; j++)
                {
                    int x = Beacons[i].Location.x - remoteScanner.Beacons[j].Location.x;
                    int y = Beacons[i].Location.y - remoteScanner.Beacons[j].Location.y;
                    int z = Beacons[i].Location.z - remoteScanner.Beacons[j].Location.z;

                    Vector co = new(x, y, z);
                    diffs.Add(co);
                }
            }

            // group by value, return the most common Location
            var results = diffs.GroupBy(x => x)
                .Select(x => new { Offsets = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .First();

            int matches = 0;
            if (results != null)
            {
                matches = results.Count;

                if (matches >= 12)
                {
                    //Console.WriteLine($"[{results.Offsets.x}, {results.Offsets.y}, {results.Offsets.z}]");
                    remoteScanner.OriginOffset = new(results.Offsets.x, results.Offsets.y, results.Offsets.z);
                    remoteScanner.Orientation = orientation;
                }
            }

            return matches;
        }

        // Place and orient scanner relative to our origin scanner at [0, 0, 0]
        // This method should only ever be called on a scanner that has not been placed
        public void Reposition()
        {
            if (IsPlaced())
            {
                Console.WriteLine("Error: Reposition() got called on a placed scanner");
                return;
            }

            // update scanner origin
            X = OriginOffset.x;
            Y = OriginOffset.y;
            Z = OriginOffset.z;

            // update beacons
            foreach (Beacon b in Beacons)
            {
                Vector rotatedBeacon = RotateVector(new Vector(b.X, b.Y, b.Z), Orientation);
                b.X = rotatedBeacon.x + OriginOffset.x;
                b.Y = rotatedBeacon.y + OriginOffset.y;
                b.Z = rotatedBeacon.z + OriginOffset.z;
                b.Location = new(b.X, b.Y, b.Z);
            }
        }
    }
}

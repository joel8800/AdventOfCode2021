using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Scanner
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<Beacon> Beacons { get; set; }
        public List<Beacon> Relationships { get; set; }

        public Coord offsets;
        public int orientation;

        public Scanner()
        {
            Id = -1;
            X = -1;
            Y = -1;
            Z = -1;
            Beacons = new();
            Relationships = new();
            offsets = new(0, 0, 0);
            orientation = 0;
        }

        public Scanner(int id, List<Beacon> beacons)
        {
            Id = id;
            X = -1;
            Y = -1;
            Z = -1;
            Beacons = beacons;
            Relationships = CalculateRelationships();
            offsets = new(0, 0, 0);
            orientation = 0;
        }

        private List<Beacon> CalculateRelationships()
        {
            Console.WriteLine($"Calculating relationships for scanner {Id}, number of beacons seen {Beacons.Count}");
            List<Beacon> rels = new();

            for (int i = 0; i < Beacons.Count; i++)
            {
                for (int j = i + 1; j < Beacons.Count; j++)
                {
                    int xDiff = Math.Abs(Beacons[i].X - Beacons[j].X);
                    int yDiff = Math.Abs(Beacons[i].Y - Beacons[j].Y);
                    int zDiff = Math.Abs(Beacons[i].Z - Beacons[j].Z);

                    List<int> diffs = new() { xDiff, yDiff, zDiff };
                    diffs.Sort();
                    Beacon r = new(diffs[0], diffs[1], diffs[2]);
                    //Console.WriteLine(r.ToString());
                    rels.Add(r);
                }
            }
            Console.WriteLine();
            return rels;
        }

        public bool IsPlaced()
        {
            if (X == -1 && Y == -1 && Z == -1)
                return false;
            else
                return true;
        }

        public void PrintBeacons()
        {
            Console.WriteLine($"===== Scanner {Id} =====");
            foreach (Beacon b in Beacons)
            {
                Console.WriteLine($"X:{b.X,-8} Y:{b.Y,-8} Z:{b.Z,-8}"); // M:{b.Manhattan, -8}");
            }
            Console.WriteLine($"Beacons: {Beacons.Count}");
        }

#pragma warning disable 1717
        private Coord RotateCoord(Coord co, int orientation)
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

            return new Coord(x, y, z);
        }
#pragma warning restore

        // rotate the remote scanner and return the number of common beacons
        public int RotateRemoteScanner(Scanner remoteScanner, int orientation)
        {
            // reorient beacons in remote scanner
            foreach (Beacon b in remoteScanner.Beacons)
            {
                Coord co = new(b.X, b.Y, b.Z);
                b.coord = RotateCoord(co, orientation);
            }

            // get differences between all beacons
            int matches = 0;
            List<Coord> diffs = new();

            for (int i = 0; i < Beacons.Count; i++)
            {
                for (int j = 0; j < remoteScanner.Beacons.Count; j++)
                {
                    int x = Beacons[i].coord.x - remoteScanner.Beacons[j].coord.x;
                    int y = Beacons[i].coord.y - remoteScanner.Beacons[j].coord.y;
                    int z = Beacons[i].coord.z - remoteScanner.Beacons[j].coord.z;

                    Coord co = new(x, y, z);
                    diffs.Add(co);
                }
            }

            // group by value, return the most common coord
            var results = diffs.GroupBy(x => x)
                .Select(x => new { Offsets = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .First();

            //var results = sums.GroupBy(g => g).OrderByDescending(c => c.Count);
            //var results = from co in sums
            //              group co by co into g
            //              let count = g.Count()
            //              orderby count ascending
            //              select new { Count = count, x = g.Key.x, y =g.Key.y, z = g.Key.z };
            if (results != null)
            {
                matches = results.Count;

                if (matches >= 12)
                {
                    //Console.WriteLine($"{results.Offsets.x}, {results.Offsets.y}, {results.Offsets.z}");
                    remoteScanner.offsets = new(results.Offsets.x, results.Offsets.y, results.Offsets.z);
                    remoteScanner.orientation = orientation;
                }
            }

            return matches;
        }

        public void Reposition()
        {
            Coord rotatedOrigin = RotateCoord(new Coord(X, Y, Z), orientation);
            //Coord originOffsets = RotateCoord(new Coord(offsets.x, offsets.y, offsets.z), orientation);

            // update scanner origin
            X = rotatedOrigin.x + offsets.x;
            Y = rotatedOrigin.y + offsets.y;
            Z = rotatedOrigin.z + offsets.z;

            // update beacons
            foreach (Beacon b in Beacons)
            {
                Coord rotatedBeacon = RotateCoord(new Coord(b.X, b.Y, b.Z), orientation);
                b.X = rotatedBeacon.x + offsets.x;
                b.Y = rotatedBeacon.y + offsets.y;
                b.Z = rotatedBeacon.z + offsets.z;
                b.coord = new(b.X, b.Y, b.Z);
            }

        }

    }
}

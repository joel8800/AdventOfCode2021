using System.Text.RegularExpressions;

namespace Day22
{
    internal class Cuboid
    {
        public bool status;
        public int xMin;
        public int xMax;
        public int yMin;
        public int yMax;
        public int zMin;
        public int zMax;

        public Cuboid(string rebootStep)
        {
            MatchCollection mc = Regex.Matches(rebootStep, @"-?\d+");

            status = rebootStep.StartsWith("on"); 

            xMin = Convert.ToInt32(mc[0].Value);
            xMax = Convert.ToInt32(mc[1].Value);
            yMin = Convert.ToInt32(mc[2].Value);
            yMax = Convert.ToInt32(mc[3].Value);
            zMin = Convert.ToInt32(mc[4].Value);
            zMax = Convert.ToInt32(mc[5].Value);

            //Console.WriteLine($"{status,5}: x:{xMin,6} {xMax,6} | y:{yMin,6} {yMax,6} | z:{zMin,6} {zMax,6}");
        }

        public Cuboid(bool stat, int x0, int x1, int y0, int y1, int z0, int z1)
        {
            status = stat;

            xMin = x0;
            xMax = x1;
            yMin = y0;
            yMax = y1;
            zMin = z0;
            zMax = z1;
        }

        /// <summary>
        /// Calculates volume of Cuboid
        /// </summary>
        /// <returns>volume as long</returns>
        public long Volume()
        {
            long xSize = Math.Abs(xMax - xMin) + 1;
            long ySize = Math.Abs(yMax - yMin) + 1;
            long zSize = Math.Abs(zMax - zMin) + 1;

            return (1L * xSize * ySize * zSize);
        }

        /// <summary>
        /// Determines if other Cuboid overlaps with this one
        /// </summary>
        /// <param name="other"></param>
        /// <returns>boolean true if there is an overlap</returns>
        public bool Overlaps(Cuboid other)
        {
            bool xOverlap = (xMin <= other.xMax) && (xMax >= other.xMin);
            bool yOverlap = (yMin <= other.yMax) && (yMax >= other.yMin);
            bool zOverlap = (zMin <= other.zMax) && (zMax >= other.zMin);

            return xOverlap && yOverlap && zOverlap;
        }

        public bool Contains(Cuboid other)
        {
            bool xContains = (xMin <= other.xMin) && (xMax >= other.xMax);
            bool yContains = (yMin <= other.yMin) && (yMax >= other.yMax);
            bool zContains = (zMin <= other.zMin) && (zMax >= other.zMax);

            return xContains && yContains && zContains;
        }

        /// <summary>
        /// Determines area that overlaps
        /// </summary>
        /// <param name="other"></param>
        /// <returns>New Cuboid of overlapping area</returns>
        public Cuboid? Intersection(Cuboid other)
        {
            int xStart, xEnd;
            int yStart, yEnd;
            int zStart, zEnd;

            var x0Range = Enumerable.Range(xMin, xMax - xMin + 1).ToArray();
            var y0Range = Enumerable.Range(yMin, yMax - yMin + 1).ToArray();
            var z0Range = Enumerable.Range(zMin, zMax - zMin + 1).ToArray();

            var x1Range = Enumerable.Range(other.xMin, other.xMax - other.xMin + 1).ToArray();
            var y1Range = Enumerable.Range(other.yMin, other.yMax - other.yMin + 1).ToArray();
            var z1Range = Enumerable.Range(other.zMin, other.zMax - other.zMin + 1).ToArray();

            var xIntersect = x0Range.Intersect(x1Range);
            var yIntersect = y0Range.Intersect(y1Range);
            var zIntersect = z0Range.Intersect(z1Range);

            try
            {
                xStart = xIntersect.First();
                yStart = yIntersect.First();
                zStart = zIntersect.First();

                xEnd = xIntersect.Last();
                yEnd = yIntersect.Last();
                zEnd = zIntersect.Last();
            }
            catch
            {
                return null;
            }

            Cuboid retCube = new(status, xStart, xEnd, yStart, yEnd, zStart, zEnd);

            return retCube;
        }

        public List<Cuboid> Minus(Cuboid other)
        {
            List<(int, int)> xSplits = SplitAxis(this.xMin, this.xMax, other.xMin, other.xMax);
            List<(int, int)> ySplits = SplitAxis(this.yMin, this.yMax, other.yMin, other.yMax);
            List<(int, int)> zSplits = SplitAxis(this.zMin, this.zMax, other.zMin, other.zMax);

            List<Cuboid> splitCubes = new();

            if (xSplits.Count == 0 || ySplits.Count == 0 || zSplits.Count == 0)
            {
                splitCubes.Add(this);
            }
            else
            {
                foreach (var xItem in xSplits)
                {
                    foreach (var yItem in ySplits)
                    {
                        foreach (var zItem in zSplits)
                        {
                            Cuboid splitCube = new(other.status, xItem.Item1, xItem.Item2, yItem.Item1, yItem.Item2, zItem.Item1, zItem.Item2);
                            if (!other.Contains(splitCube))
                            {
                                splitCubes.Add(splitCube);
                            }
                        }
                    }
                }

            }

            return splitCubes;
        }

        public static List<(int, int)> SplitAxis(int thisLow, int thisHigh, int otherLow, int otherHigh)
        {
            List<(int, int)> results = new();
            if (thisLow > otherHigh || thisHigh < otherLow)
            {
                // no overlap, return empty list
                return results;
            }
            else
            {
                if (otherLow <= thisLow)
                {
                    if (otherHigh < thisHigh)
                    {
                        // Condition-1
                        results.Add((thisLow, otherHigh));
                        results.Add((otherHigh + 1, thisHigh));
                    }
                    else
                    {
                        // Condition-2
                        results.Add((thisLow, thisHigh));
                    }
                }
                else
                {
                    if (otherHigh < thisHigh)
                    {
                        // Condition-3
                        results.Add((thisLow, otherLow - 1));
                        results.Add((otherLow, otherHigh));
                        results.Add((otherHigh + 1, thisHigh));
                    }
                    else
                    {
                        // Condition-4
                        results.Add((thisLow, otherLow - 1));
                        results.Add((otherLow, thisHigh));
                    }
                }
            }

            return results;
        }
    }
}

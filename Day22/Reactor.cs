namespace Day22
{
    internal class Reactor
    {
        bool _isPart1;
        private Cuboid _pt1Region;
        private List<Cuboid> _cubes;

        public Reactor(bool isPart1)
        {
            _cubes = new();
            _isPart1 = isPart1;
            _pt1Region = new(false, -50, 50, -50, 50, -50, 50);
        }

        public void ExecuteStep(string rebootStep)
        {
            List<Cuboid> newCubes = new();
            Cuboid thisCube = new(rebootStep);

            if (_isPart1)
            {   // ignore cuboids that don't overlap _pt1Region
                if (thisCube.Overlaps(_pt1Region) == false)
                    return;
            }

            foreach (Cuboid cube in _cubes)
                cube.Minus(thisCube).ForEach(c => newCubes.Add(c));

            if (thisCube.status)
                newCubes.Add(thisCube);

            _cubes.Clear();
            _cubes = newCubes;
        }

        public long CubeCount()
        {
            long totalVolume = 0;

            foreach (Cuboid cube in _cubes)
                totalVolume += cube.Volume();

            return totalVolume;
        }

        public void PrintCubesList()
        {
            Console.WriteLine($"number of cubes: {_cubes.Count}");
            foreach (Cuboid c in _cubes)
                Console.WriteLine($"({c.xMin,8} {c.xMax,8}) ({c.yMin,8} {c.yMax,8}) ({c.zMin,8} {c.zMax,8}) : {c.Volume()}");
        }

    }
}

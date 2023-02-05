namespace Day20
{
    internal class PixelEnhancer
    {
        private readonly List<(int x, int y)> surround = new()
        {
            (x:-1, y:-1),   // nw
            (x: 0, y:-1),   // n
            (x: 1, y:-1),   // ne
            (x:-1, y: 0),   // w
            (x: 0, y: 0),   // center
            (x: 1, y: 0),   // e
            (x:-1, y: 1),   // sw
            (x: 0, y: 1),   // s
            (x: 1, y: 1)    // se
        };

        private int _universe;
        private int[,] _image;
        private List<int> _algorithm;

        public int Ones { get; set; }

        public PixelEnhancer(string algorithm, string image)    //fileName)
        {
            _universe = 0;

            // create algorithm list
            _algorithm = new();
            for (int i = 0; i < algorithm.Length; i++)
                _algorithm.Add(algorithm[i] == '#' ? 1 : 0);

            // split off _image lines
            string[] imgLines = image.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

            // get _image dimensions
            int ySize = imgLines.Length;
            int xSize = imgLines[0].Length;

            _image = new int[xSize, ySize];

            // fill _image data
            for (int y = 0; y < imgLines.Length; y++)
            {
                string line = imgLines[y];

                for (int x = 0; x < imgLines[0].Length; x++)
                    _image[x, y] = line[x] == '#' ? 1 : 0;
            }

            //Console.WriteLine("input _image");
            //PrintImage();
        }

        public int EnhanceImage()
        {
            int border = 1; // 2;

            int xSize = _image.GetLength(0);
            int ySize = _image.GetLength(1);

            int newXSize = xSize + border * 2;
            int newYSize = ySize + border * 2;

            int[,] newImage = new int[newXSize, newYSize];

            for (int y = -border; y < ySize + border; y++)
                for (int x = -border; x < xSize + border; x++)
                    newImage[x + border, y + border] = EnhancePixel(x, y);

            // check the infinite _universe
            // if it is dark and 9 darks turn into a light => flip the background
            // if it is lit and 9 lights turn into dark => also flip
            if (_algorithm[0] == 1)
                _universe = _universe == 1 ? 0 : 1;

            _image = newImage;
            //PrintImage();

            Ones = CountOnes();
            return Ones;
        }

        private int EnhancePixel(int x, int y)
        {
            int index = 0;
            int xSize = _image.GetLength(0);
            int ySize = _image.GetLength(1);

            foreach (var offset in surround) //s_arounds)
            {
                index <<= 1;

                int localX = x + offset.x;
                int localY = y + offset.y;

                if (localX >= 0 && localX < xSize && localY >= 0 && localY < ySize)
                {
                    // Within bounds, we look at the input _image
                    if (_image[localX, localY] == 1)
                        index |= 1;
                }
                else
                {
                    // Outside of bounds, we check the fake infinite surroundings
                    if (_universe == 1)
                        index |= 1;
                }
            }

            return _algorithm[index];
        }

        private int CountOnes()
        {
            int ones = 0;

            for (int y = 0; y < _image.GetLength(1); y++)
                for (int x = 0; x < _image.GetLength(0); x++)
                    if (_image[x, y] == 1)
                        ones++;

            return ones;
        }

        public void PrintImage()
        {
            Console.WriteLine();
            for (int y = 0; y < _image.GetLength(1); y++)
            {
                for (int x = 0; x < _image.GetLength(0); x++)
                    Console.Write($"{(_image[x, y] == 1 ? '#' : '.')}");

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

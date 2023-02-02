namespace Day14
{
    internal class Polymer
    {
        private string _template;
        private string _lastChar;
        private Dictionary<string, string> _insertionRules;
        private Dictionary<string, long> _histogram;
        private Dictionary<string, long> _elements;
        private HashSet<char> _charSet;

        public Polymer()
        {
            _template = string.Empty;
            _insertionRules = new();
            _histogram = new();
            _elements = new();
            _lastChar = string.Empty;
            _charSet = new();
        }

        public void Initialize(string initialPolymer, string insertionRules)
        {
            _template = initialPolymer;
            foreach (char c in _template)
            {
                AddToElementCount(c.ToString(), 1);
                _charSet.Add(c);
            }

            string[] rules = insertionRules.Split(Environment.NewLine);
            foreach (string line in rules)
            {
                var rule = line.Split(" -> ");
                _insertionRules.Add(rule[0], rule[1]);
                _histogram.Add(rule[0], 0);
                _charSet.Add(Convert.ToChar(rule[1]));
            }

            // add pairs in _template to our _histogram
            for (int i = 0; i < _template.Length - 1; i++)
            {
                string pair = _template.Substring(i, 2);

                if (_histogram.ContainsKey(pair))
                {
                    _histogram[pair]++;
                }
                else
                {
                    throw new Exception($"rule pair {pair}, not found in _histogram");
                }
            }
        }

        private void AddToElementCount(string element, long addCount)
        {
            //Console.WriteLine($"adding {addCount} to element[{element}]");
            if (_elements.ContainsKey(element))
                _elements[element] += addCount;
            else
                _elements.Add(element, addCount);
        }

        /// <summary>
        /// Histogram method
        /// 
        /// </summary>
        public void PerformInsertionStep()
        {
            Dictionary<string, long> tempHisto = new();

            // temp _histogram to store additions
            foreach (string pair in _histogram.Keys)
                tempHisto.Add(pair, 0);

            // check each pair of chars, increment counts of resulting new pairs after insertion
            foreach (string pair in _histogram.Keys)
            {
                if (_histogram[pair] > 0)
                {
                    if (_insertionRules.ContainsKey(pair))
                    {
                        long insertsToDo = _histogram[pair];

                        string newPair0 = pair[0] + _insertionRules[pair];
                        string newPair1 = _insertionRules[pair] + pair[1];

                        tempHisto[newPair0] += insertsToDo;
                        tempHisto[newPair1] += insertsToDo;

                        // only increment inserted element
                        AddToElementCount(_insertionRules[pair], insertsToDo);
                    }
                    else
                    {
                        throw new Exception($"rule for pair {pair}, not found in insertion rules");
                    }
                }
            }

            // replace new pair counts to _histogram
            foreach (string pair in _histogram.Keys)
            {
                _histogram[pair] = tempHisto[pair];
            }
        }

        public long GetMostCommonElementCount()
        {
            long MCCount = _elements.Max(e => e.Value);
            string MCElement = _elements.First(e => e.Value == MCCount).Key;

            //Console.WriteLine($"most common char : {MCElement}, count: {MCCount}");
            return MCCount;
        }

        public long GetLeastCommonElementCount()
        {
            long LCCount = _elements.Min(e => e.Value);
            string LCElement = _elements.First(e => e.Value == LCCount).Key;

            //Console.WriteLine($"least common char: {LCElement}, count: {LCCount}");
            return LCCount;
        }

        public void PrintHisto()
        {
            long i = 0;

            Console.WriteLine("Pairs:");
            foreach (string key in _histogram.Keys)
            {
                if (_histogram[key] > 0)
                {
                    Console.WriteLine($"{key} : {_histogram[key]}");
                    i += _histogram[key];
                }
            }
            Console.WriteLine($"total pairs = {i}");
            i = 0;
            Console.WriteLine("Elements");
            foreach (string elem in _elements.Keys)
            {
                if (_elements[elem] > 0)
                {
                    Console.WriteLine($"{elem}  : {_elements[elem]}");
                    i += _elements[elem];
                }
            }
            Console.WriteLine($"total _elements = {i}");
        }
    }
}

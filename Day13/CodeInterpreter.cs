using System.Text;

namespace Day13
{
    internal class CodeInterpreter
    {
        private List<string> Dataset;
        private List<string> Codes;

        public CodeInterpreter()
        {
            Dataset = new List<string>();
            Codes = new List<string>();

            string[] rawData = File.ReadAllLines("CodeDataset.txt");
            if ((rawData.Length != 6) || (rawData[0].Length != 130))
            {
                throw new Exception("Origami Dataset does not have the right number of bits");
            }

            Dataset.Add(new string('.', 30));   // add 'space' as location 0, A = 1, B = 2, etc.
            for (int letterIdx = 0; letterIdx < 130; letterIdx += 5)
            {
                string letter = string.Empty;

                for (int i = 0; i < 6; i++)
                {
                    letter += rawData[i].Substring(letterIdx, 5);
                }
                //Console.WriteLine($"letter: {letter}");
                Dataset.Add(letter);
            }
        }

        /// <summary>
        /// Convert an entire code string into list of letter strings
        /// </summary>
        /// <param name="codeString"></param>
        private void TransposeCodes(string codeString)
        {
            string[] codes = codeString.Trim().Split('\n');

            for (int letterIdx = 0; letterIdx < codes[0].Length - 1; letterIdx += 5)
            {
                string letter = string.Empty;

                for (int i = 0; i < 6; i++)
                {
                    string row = codes[i].Substring(letterIdx, 5);
                    letter += row;
                }
                Codes.Add(letter);
            }
        }

        /// <summary>
        /// Find letters in Dataset, build string as you go, return the entire code string in ASCII
        /// </summary>
        /// <param name="codeString"></param>
        /// <returns></returns>
        public string TranslateCodes(string codeString)
        {
            TransposeCodes(codeString);

            StringBuilder sb = new();
            foreach (string letter in Codes)
            {
                int codeIdx = Dataset.FindIndex(x => x == letter);
                //Console.WriteLine($"codeIdx: {codeIdx}");
                sb.Append(Convert.ToChar(codeIdx + 64));
            }

            return sb.ToString();
        }
    }
}

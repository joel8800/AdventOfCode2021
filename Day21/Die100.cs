namespace Day21
{
    public class Die100
    {
        int die;

        public int Rolls { get; set; }

        public Die100()
        {
            die = 0;
            Rolls = 0;
        }

        public int Roll()
        {
            die++;
            Rolls++;

            if (die > 100)      // wrap to 1 at 100
                die = 1;

            return die;
        }

        public int Roll3()
        {
            int total = 0;

            total += Roll();
            total += Roll();
            total += Roll();

            return total;
        }
    }
}

namespace Delagates
{
    public class Fibonacci
    {
        public long Calculate(long value)
        {
            if (value == 0)
                return 0;
            if (value == 1)
                return 1;
            var nMinus1 = Calculate(value - 1);
            var nMinus2 = Calculate(value - 2);
            var result = nMinus1 + nMinus2;
            return result;
        }
    }
}

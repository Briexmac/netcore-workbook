using System.Collections.Generic;

namespace Delagates
{
    public class Aggregator
    {
        public object Aggregate(IEnumerable<object> input)
        {
            return input;
        }
    }
}

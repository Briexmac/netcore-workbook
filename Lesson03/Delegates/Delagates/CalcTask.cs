using System.Threading.Tasks;

namespace Delagates
{
    public class CalcTask
    {
        public Task<long> Run()
        {
            return Task.Run(() =>
            {
                var result = new Fibonacci().Calculate(6);

                System.Console.WriteLine(result);

                return result;

            });
        }
    }
}

namespace Delagates
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App(args);
            app.Run();

            var value = new CalcTask().Run().Result;

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadKey();
        }
    }
}

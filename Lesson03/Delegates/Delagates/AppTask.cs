namespace Delagates
{
    public class AppTask
    {
        public void Run()
        {
            var connector = new GitHubConnector("daniefer");
            var info = connector.GetFormattedUserInfoAsync().Result;
            System.Console.WriteLine(info);
        }
    }
}

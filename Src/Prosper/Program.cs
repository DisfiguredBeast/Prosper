namespace Prosper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var gameConfig = new GameConfig();
            using (var window = new GameWindow(gameConfig))
            {
                window.Run(30);
            }
        }
    }
}

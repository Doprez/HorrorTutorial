using Stride.Engine;

namespace HorrorTutorial
{
    class HorrorTutorialApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}

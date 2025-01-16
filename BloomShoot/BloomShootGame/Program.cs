using BloomShootGame;
using BloomShootGameSinglePlayer;


internal class Program
{
    static void Main(string[] args)
    {
        using (var menu = new BloomShootMenuProgram())
        {
            menu.Run();

            // After menu closes, check which game was selected
            switch (menu.SelectedGameSettings)
            {
                case 0:
                    using (var game = new BloomShootGameProgram())
                        game.Run();
                    break;
                case 1:
                    using (var game = new BloomShootGameSinglePlayerProgram())
                        game.Run();
                    break;
                case 2:
                    break;
            }
        }
    }
}
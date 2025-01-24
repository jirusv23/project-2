using BloomShootGame;
using BloomShootGame.Menu;
using BloomShootGameSinglePlayer;
using Microsoft.Xna.Framework;
using System.Diagnostics;


internal class Program
{
    static void Main(string[] args)
    {
    startMenuLoop:

        using (var menu = new BloomShootMenuProgram())
        {
            menu.Run();

            // Po ukončení hry se dle výběru pokračuje
            switch (menu.SelectedGameSettings)
            {
                case 1:
                    using (var game = new BloomShootGameSinglePlayerProgram())
                    { 
                        Debug.WriteLine("Started singleplayer"); 
                        game.Run();
                    }
                    goto startMenuLoop;

                case 0:
                    using (var gameMenu = new MultiplayerMenu())
                    {
                        gameMenu.Run();
                        using (var game = new BloomShootGameProgram(gameMenu.Password, gameMenu.IpAddress))
                        {
                            game.Run();
                        }
                    }
                    break;

                case 3:
                    using (var gameSettings = new Settings())
                    {
                        gameSettings.Run();
                    }
                    // runs a new menu
                    goto startMenuLoop;

                case 2: // exit
                    break;
            }
        }
    }
}
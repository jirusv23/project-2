﻿using BloomShootGame;
using BloomShootGameSinglePlayer;


internal class Program
{
    static void Main(string[] args)
    {
        using (var menu = new BloomShootMenuProgram())
        {
            menu.Run();

            // Po ukončení hry se dle výběru pokračuje
            switch (menu.SelectedGameSettings)
            {
                case 0:
                    using (var game = new BloomShootGameProgram())
                        game.Run();
                    break;
                case 1:
                    using (var gameMenu = new MultiplayerMenu())
                    {
                        gameMenu.Run();
                        using (var game = new BloomShootGameSinglePlayerProgram())
                            game.Run();
                    }
                    break;
                case 2:
                    break;
            }
        }
    }
}
using BloomShootGame;

internal class Program
{
    static void Main(string[] args)
    {
        using var menu = new BloomShootMenuProgram();
        menu.Run();
        
        using var game = new BloomShootGameProgram();
        game.Run(); 
    }
}
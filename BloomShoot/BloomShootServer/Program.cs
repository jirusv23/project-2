internal class Program
{
    static void Main(string[] args)
    {
        using var game = new BloomShootServer.Game1();
        game.Run(); 
    }
}
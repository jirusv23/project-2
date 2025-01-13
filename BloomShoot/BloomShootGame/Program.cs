internal class Program
{
    static void Main(string[] args)
    {
        using var game = new BloomShootGame.BloomShootGameProgram();
        game.Run(); 
    }
}
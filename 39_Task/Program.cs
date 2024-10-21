namespace _39_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Работа со свойствами";
            string markPlayer = "@";
            int positionX = 5;
            int pisitionY = 5;

            Player player = new Player(markPlayer, positionX, pisitionY);
            Renderer renderer = new();
            renderer.Draw(player);

            Console.ReadLine();
        }
    }

    public class Player
    {
        public Player(string markSymbol, int positionX, int positionY)
        {
            MarkSymbol = markSymbol;
            PositionX = positionX;
            PositionY = positionY;
        }

        public string MarkSymbol { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
    }

    public class Renderer
    {
        public void Draw(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.WriteLine(player.MarkSymbol);
        }
    }
}
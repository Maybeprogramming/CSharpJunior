namespace _04_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Картинки";
            int pictures = 52;
            int maxPicturesInRow = 3;
            int usedPictures;
            int unusedPictures;

            usedPictures = pictures / maxPicturesInRow;
            unusedPictures = pictures % maxPicturesInRow;

            Console.WriteLine($"Полностью заполненных рядов: {usedPictures}.");
            Console.WriteLine($"Картинок сверх меры: {unusedPictures}.");

            Console.ReadKey();
        }
    }
}

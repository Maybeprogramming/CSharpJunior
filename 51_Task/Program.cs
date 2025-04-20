namespace _51_Task
{
    public class Program
    {
        static void Main()
        {
        }
    }

    public class Criminal
    {
        public string Name { get; }
        public string Nationality { get; }
        public int Height { get; }
        public int Weight { get; }
        bool IsUnderArrest { get; }
    }

    public class CrimanalFactory
    { 

    }

    public static class CrimanalData
    {
        private static string s_names;
        private static string s_surNames;
        private static int s_height;
        private static int s_weight;


    }
}

namespace DefaultSeedConsoleApp.Extensions
{
    public static class IntExtensions
    {
        public static int StringToInt(string s, int @default)
        {
            int number;
            if (int.TryParse(s, out number))
                return number;
            return @default;
        }
    }
}

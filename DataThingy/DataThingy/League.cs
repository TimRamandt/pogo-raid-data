namespace DataThingy
{
    public enum League
    {
        Gl = 1500,
        Ul = 2500,
        Ml = 10000
    }

    public static class LeagueExtensions
    {
        public static string ReadData(this League league)
        {
            var name = Enum.GetName(typeof(League), league);
            return File.ReadAllText($"{name.ToLower()}.json");
        }

        public static string DownloadUrl(this League league)
        {
            return $"https://pvpoke.com/data/rankings/all/overall/rankings-{(int) league}.json";
        }

    }
}

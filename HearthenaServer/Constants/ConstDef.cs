namespace HearthenaServer.Constants
{
    public static class ConstDef
    {
        public static Guid Card1Guid = new Guid("94afb56c-d0ca-40f8-98d5-251b0065352b");
        public static Guid Player1Guid = new Guid("88dc9f73-1f15-4f14-a797-6fd2f055c075");
        public static Guid Player2Guid = new Guid("394eff80-9bc9-4e8e-8e1e-b1ebd0e9115d");

        public static Guid TrollGuid = new Guid("76dd1ba3-7d17-44af-92ad-6cb24039cb42");

        public static Dictionary<string, string> trollProperties = new Dictionary<string, string>()
        {
                {"mana", "3"},
                {"atk", "3" },
                {"hp", "2" },
        };
    }
}

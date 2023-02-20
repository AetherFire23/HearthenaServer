using System.Collections.Generic;

namespace HearthenaServer.Constants
{
    public static class CardProperties
    {
        public static Dictionary<string, string> TrollProperties = new Dictionary<string, string>()
        {
                {"hp", "2" },
                {"atk", "3" }
        };
        public static Dictionary<string, string> FireBallProperties = new Dictionary<string, string>()
        {
                {StringParameters.SpellDamage, "6" },

        };
    }
}

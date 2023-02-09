using HearthenaServer.Utils;
using System.Collections;

namespace HearthenaServer.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Shuffle<T>(this List<T> self)
        {
            List<T> buffer = self;
            List<T> shuffled = new();
            while (buffer.Count > 0)
            {
                var randomIndex = Rand.r.Next(0, buffer.Count);
                var randomCard = self.ElementAt(randomIndex);
                shuffled.Add(randomCard);
                buffer.Remove(randomCard);
            }

            return shuffled.ToList();
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            bool isNUll = list is null;
            return isNUll || !list.Any();
        }
    }
}

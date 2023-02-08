using HearthenaServer.Constants;
using HearthenaServer.Entities;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;

namespace HearthenaServer.Extensions
{
    public static class Extensions
    {
        public static int GetMinionInsertIndex(this Dictionary<string, string> self)
        {
            var index = self.GetValueOrDefault(StringParameters.MinionInsertIndex);

            if (index is null) throw new NullReferenceException(StringParameters.MinionInsertIndex);

            return Convert.ToInt32((index));
        }

        public static Type GetTargetType(this Dictionary<string, string> self)
        {
            string serializedType = self.GetValueOrDefault(StringParameters.TargetType);
            Type type = JsonConvert.DeserializeObject<Type>(serializedType);

            if (type == null) throw new NullReferenceException(StringParameters.TargetType);

            return type;
        }

        public static int GetSpellDamage(this Dictionary<string, string> self)
        {
            string damageAsString = self.GetValueOrDefault(StringParameters.Damage);

            if (damageAsString is null) throw new NullReferenceException();

            return Convert.ToInt32((damageAsString));
        }

        public static Guid GetTargetId(this Dictionary<string, string> self)
        {
            Guid targetId = new Guid(self.GetValueOrDefault(StringParameters.TargetId));

            if (targetId == Guid.Empty) throw new NullReferenceException(StringParameters.TargetId);

            return targetId;
        }

    }
}

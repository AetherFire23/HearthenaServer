using HearthenaServer.Enums;
using HearthenaServer.Models;
using System;
using System.Collections.Generic;

namespace HearthenaServer.Constants
{
    public static class Enchant
    {
        private static Dictionary<EnchantmentType, object> Enchantments { get; set; } = new Dictionary<EnchantmentType, object>()
        {
            { EnchantmentType.DivineShield, true},
            { EnchantmentType.Poison, true},
            { EnchantmentType.SunwellCleric, new StatBonus(){Attack = 2, Health =2 } },
        };

        public static T TryGetEnchant<T>(EnchantmentType enchantType)
        { 
            object enchant = null;
            bool exists = Enchantments.TryGetValue(enchantType, out enchant);

            if (!exists) throw new Exception($"Enchant ({enchantType}) is not implemented.");

            return (T)enchant;
        }
        // All effects are active

        // some effects modify other stats
        // must calculate these effects before dealing damage.
        // need a mapping between enchants and bonuses

        // some effects affect certain logic (whether damage is dealt OR NOT)
        // ex. whether or not it instantly kills

        // 
        // 
    }
}

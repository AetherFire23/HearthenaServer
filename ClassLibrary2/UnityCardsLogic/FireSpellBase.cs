using HearthenaServer.Entities;
using HearthenaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.GameTasks;

namespace Shared_Models.UnityCardsLogic
{
    public class FireSpellBase : SpellBase
    {
        public FireSpellBase()
        {

        }
        public override Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters)
        {
            throw new NotImplementedException();
        }

        public override bool IsPlayable(GameState gameState) 
        {
            throw new NotImplementedException();
        }

        public override bool IsValidTarget()
        {
            throw new NotImplementedException();
        }
    }
}

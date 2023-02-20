using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.GameTasks;

namespace HearthenaServer.CardServices.Spells
{
    [GameTask(GameTaskCode.FireBall)]
    public class FireBallLogicUnity : SpellBase// abstract pr impleter ds unity 
    {
        public override bool IsPlayable()
        {
            return false;
        }

        public override bool IsValidTarget() // implemented ds le abstract
        {
            return false;
        }

        // les get se font en-dehors de la classse  ??
        // en fiat apply spell effect 
        public override async Task ApplySpellEffect(Card card, Dictionary<string, string> targetParameters)
        {

        }

        // MinionDiesAfterAction

        public bool MinionDiesAfterTarget()
        {
            return false;
        }
    }
}
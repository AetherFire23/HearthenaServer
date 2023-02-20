using HearthenaServer.Entities;
using HearthenaServer.Extensions;
using HearthenaServer.Interfaces;
using HearthenaServer.Models;
using Newtonsoft.Json;
using Shared_Models.UnityCardsLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.GameTasks;

namespace HearthenaServer.CardServices.Spells
{
    [GameTask(GameTaskCode.FireBall)]
    public class FireBallLogicUnity : FireSpellBase// abstract pr impleter ds unity 
    {
        public override bool IsPlayable(GameState gameState)
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
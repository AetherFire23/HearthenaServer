using HearthenaServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Models.Constants
{
    public static class SampleModels
    {
        public static Card CreateBlankCard()
        {
            var card = new Card()
            {
                Id = Guid.NewGuid(),
                Type = WebAPI.GameTasks.GameTaskCode.Blank,
                IsInHand = true,
            };
            return card;
        }

    }
}

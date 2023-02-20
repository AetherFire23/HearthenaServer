using HearthenaServer.Entities;
using Shared_Models.DTO;

namespace HearthenaServer.Models
{
    public class GameState
    {

        public PlayerDTO LocalPlayer { get; set; }
        public PlayerDTO Opponent { get; set; }
    }
}

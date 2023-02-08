using HearthenaServer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HearthenaServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly HearthenaContext _context;
        public GameController(ILogger<GameController> logger, HearthenaContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPut]
        [Route("PlayCard")]
        public async Task<ActionResult> PlayCard(Dictionary<string, string> onPlayParameters)
        {
            // sometimes actions are done OnPlay. Player must choose battlecary before playing, by example, so targetId will be contained in onPlayParameters



            return Ok();
            // Test!
        }

        [HttpGet]
        [Route("GetGameState")]
        public async Task<ActionResult> GetGameState()
        {
            return Ok();

        }

        [HttpPut]
        [Route("HandSelection")]
        public async Task<ActionResult> HandSelection()
        {
            return Ok();
        }



        [HttpGet]
        [Route("GetCards")]
        public async Task<ActionResult<List<Card>>> GetCards()
        {
            var all = _context.Cards.ToList();
            return Ok(all);
        }
    }
}
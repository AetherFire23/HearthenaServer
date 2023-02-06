using Microsoft.AspNetCore.Mvc;

namespace HearthenaServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        [Route("PlayCard")]
        public async Task<ActionResult> PlayCard()
        {
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

        [HttpPut]
        [Route("MinionAttack")]
        public async Task<ActionResult> MinionAttack()
        {
            return Ok();
        }
    }
}
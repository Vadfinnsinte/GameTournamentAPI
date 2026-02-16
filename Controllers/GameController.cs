using GameTournamentAPI.Models.GameDTOs;
using GameTournamentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameTournamentAPI.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GameResponeDTO>> Get([FromQuery] string? title)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(title))
                {
                    var game = _gameService.GetByTitle(title);
                    if (game == null) return NotFound();
                    return Ok(new[] { game });
                }
                return Ok(_gameService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
        [HttpGet("{id:Guid}")]
        public ActionResult<GameResponeDTO> Get(Guid id)
        {
            try
            {
                var game = _gameService.GetById(id);
                if (game == null) return NotFound();
                return Ok(game);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<GameCreateDTO>> Create(GameCreateDTO dto)
        {
            try
            {
                var createdgame = _gameService.Create(dto);
                return CreatedAtAction(
                        nameof(Get),
                        new { id = createdgame.Id },
                        createdgame
            );

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPut("{id:Guid}")]
        public ActionResult Update(Guid id, GameUpdateDTO game)
        {
            try
            {
                if (!_gameService.Update(id, game)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }

        }

        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                if (!_gameService.Delete(id)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

    }
}

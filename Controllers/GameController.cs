using GameTournamentAPI.Models.GameDTOs;
using GameTournamentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameTournamentAPI.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<GameResponseDTO>>> Get([FromQuery] string? title)
        {
     
                if (!string.IsNullOrWhiteSpace(title))
                {
                    var game = await _gameService.GetByTitleAsync(title);
                    if (game == null) return NotFound();
                    return Ok(new[] { game });
                }
            var games = await _gameService.GetAllAsync();
            return Ok(games);
            
           
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GameResponseDTO>> Get(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null) return NotFound();
            
            return Ok(game);

            
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<GameCreateDTO>>> Create(GameCreateDTO dto)
        {
            
            var createdgame = await _gameService.CreateAsync(dto);
            if (createdgame == null ) return NotFound();


            return Ok(createdgame);


        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Update(Guid id, GameUpdateDTO game)
        {

            var updated = await _gameService.UpdateAsync(id, game);
            if (!updated) return NotFound();

            return NoContent();
        }
            

        

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _gameService.DeleteAsync(id);
                if (!deleted) return NotFound();

                return NoContent();
           
        }

    }
}

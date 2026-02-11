using GameTournamentAPI.Models;
using GameTournamentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameTournamentAPI.Controllers
{
    [ApiController]
    [Route("api/tournament")]
    public class TournamentController : ControllerBase
    {

        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TournamentResponseDTO>> Get([FromQuery] string? title)
        {
            try
            {

            if (!string.IsNullOrWhiteSpace(title))
            {
                var tournament = _tournamentService.GetByTitle(title);
                if (tournament == null) return NotFound();
                return Ok(new[] { tournament });
            }
            return Ok(_tournamentService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<TournamentCreateDTO>> Create(TournamentCreateDTO dto)
        {
            try
            {
            var createdTournament = _tournamentService.Create(dto);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = createdTournament.Id },
                    createdTournament
        );

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

    }
}

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
            if (!string.IsNullOrWhiteSpace(title))
            {
                var tournament = _tournamentService.GetByTitle(title);
                if (tournament == null) return NotFound();
                return Ok(new[] { tournament });
            }
            return Ok(_tournamentService.GetAll());
        }

        [HttpPost]
        public ActionResult<IEnumerable<TournamentCreateDTO>> Create(TournamentCreateDTO dto)
        {
            var createdTournament = _tournamentService.Create(dto);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = createdTournament.Id },
                    createdTournament
        );
        }

    }
}

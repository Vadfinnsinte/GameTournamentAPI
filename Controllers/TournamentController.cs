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
        [HttpGet("{id:Guid}")]
        public ActionResult<TournamentResponseDTO> Get(Guid id)
        {
            try
            {
            var tournament = _tournamentService.GetById(id);
            if (tournament == null) return NotFound();
            return Ok( tournament );

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

        [HttpPut("{id:Guid}")]
        public ActionResult Update(Guid id, TournamentUpdateDTO tournament)
        {
            try
            {
                if (!_tournamentService.Update(id, tournament)) return NotFound();
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
                if (!_tournamentService.Delete(id)) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}

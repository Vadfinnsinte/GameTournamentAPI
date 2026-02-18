using GameTournamentAPI.Models.TournamentDTOs;
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
        public async Task<ActionResult<IEnumerable<TournamentResponseDTO>>> Get([FromQuery] string? title)
        {
 
            if (!string.IsNullOrWhiteSpace(title))
            {
                var tournament = await _tournamentService.GetByTitleAsync(title);
                if (tournament == null) return NotFound();
                return Ok(new[] { tournament });
            }
            var tournaments = await _tournamentService.GetAllAsync();
            return Ok(tournaments);

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<TournamentResponseDTO>> Get(Guid id)
        {
       
            var tournament = await _tournamentService.GetByIdAsync(id);
            if (tournament == null) return NotFound();

            return Ok( tournament );

           
        }

        [HttpPost]
        public async Task <ActionResult<IEnumerable<TournamentCreateDTO>>> Create(TournamentCreateDTO dto)
        {
      
            var createdTournament = await _tournamentService.CreateAsync(dto);
            return CreatedAtAction(
                    nameof(Get),
                    new { id = createdTournament.Id },
                    createdTournament
        );

        
        }

        [HttpPut("{id:Guid}")]
        public async Task <ActionResult> Update(Guid id, TournamentUpdateDTO tournament)
        {
            var existingTournament = await _tournamentService.UpdateAsync(id, tournament);
                
            if (!existingTournament) return NotFound();
                
            return NoContent();
            
           

        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var tournamentToDelete = await _tournamentService.DeleteAsync(id);
             
              if (!tournamentToDelete) return NotFound();
                return NoContent();
            
            
        }
    }
}

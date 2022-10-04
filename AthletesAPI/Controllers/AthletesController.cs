using AthletesRestAPI.Exceptionss;
using AthletesRestAPI.Models;
using AthletesRestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Controllers
{
   
    [Route("api/disciplines/{disciplineId}/athletes")]
    [Authorize(Roles = "Admin")]
    public class AthletesController:Controller
    {
        private IAthleteService _athleteService;
        private IFileService _fileService;
        public AthletesController(IAthleteService athleteService,IFileService fileService)
        {
            _athleteService = athleteService;
            _fileService = fileService;
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortAthleteModel>>> GetAthletesAsync(int disciplineId)
        {
            //return await _athleteService.GetAthletesAsync(disciplineId);
            try
            {
                var athletes = await _athleteService.GetAthletesAsync(disciplineId);
                return Ok(athletes);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet("{athleteId:int}")]
        //VERIFICAR ORDEN CORRECTO DE PARAMETROS ID
        public async Task<ActionResult<AthleteModel>> GetAthleteAsync(int athleteId,int disciplineId)
        {           
            try
            {
                var athlete = await _athleteService.GetAthleteAsync(athleteId, disciplineId);
                return Ok(athlete);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }           
        }

       // [Authorize(Roles = "Admin")]
        [HttpPut("{athleteId:int}")]
        public async Task<ActionResult<AthleteModel>> UpdateAthleteAsync(int athleteId, [FromBody] AthleteModel athlete,int disciplineId)
        {
            try
            {
                var updatedAthlete = await _athleteService.UpdateAthleteAsync(athleteId, athlete, disciplineId);
                return Ok(updatedAthlete);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AthleteModel>> CreateAthleteAsync([FromBody] AthleteModel athlete,int disciplineId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                athlete.ImagePath = athlete.ImagePath ?? "Resources\\Images\\genericAthlete.jpg";
                var newAthlete = await _athleteService.CreateAthleteAsync(athlete, disciplineId);
                return Created($"/api/disicplines/{disciplineId}/athletes/{newAthlete.Id}", newAthlete);
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }

        [HttpPost("Form")]
        public async Task<ActionResult<AthleteModel>> CreateAthleteFormAsync([FromForm] AthleteFormModel newAthlete, int disciplineId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                //var bookModel = newBook;
                var file = newAthlete.Image;
                string imagePath = _fileService.UploadFile(file);

                newAthlete.ImagePath = imagePath;

                var athlete = await _athleteService.CreateAthleteAsync(newAthlete, disciplineId);
                //var id = discipline.Id;
                //var a = Created($"/api/books/{discipline.Id}", new BookModel() { Id = newBook.Id, Title = newBook.Title, Author = newBook.Author, Genre = newBook.Genre, ImagePath = newBook.ImagePath });
                return Created($"/api/disciplines/{disciplineId}/athletes/{newAthlete.Id}", athlete); //new DisciplineModel() { Id = newBook.Id, Title = newBook.Title, Author = newBook.Author, Genre = newBook.Genre, ImagePath = newBook.ImagePath });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{athleteId:int}")]
        public async Task<ActionResult> DeleteAthleteAsync(int athleteId,int disciplineId)
        {
            try
            {
                await _athleteService.DeleteAthleteAsync(athleteId, disciplineId);
                return Ok();
            }
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }


       /* [HttpGet("race")]
        public ActionResult<IEnumerable<Object>> Race(string discipline, string gender, string podium = "false")
        {
            try
            {
                var athletes= _athleteService.Race(discipline, gender, podium);
                return Ok(athletes);
            }
            catch (IncompleteRequestException ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
            catch (InvalidElementOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
            
        }


        [HttpGet("relayTeam")]
        public ActionResult<IEnumerable<AthleteModel>> GetRelayTeam(string discipline, string gender, string country, string ageCategory="all")
        {
            try
            {
                var team = _athleteService.GetRelayTeam(discipline, gender, country, ageCategory);
                return Ok(team);
            }
            catch (IncompleteRequestException ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
            catch (InvalidElementOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotEnoughMembersForRelayTeamException ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }*/
    }
}

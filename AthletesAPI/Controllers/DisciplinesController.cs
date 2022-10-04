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

namespace DisciplinesRestAPI.Controllers
{   
    [Route("api/disciplines")]
    public class DisciplinesController:Controller
    {
        private IDisciplineService _disciplineService;
        private IFileService _fileService;
        public DisciplinesController(IDisciplineService disciplineService, IFileService fileService)
        {
            _disciplineService = disciplineService;
            _fileService = fileService;
        }
        //[Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplineModel>>> GetDisciplinesAsync()
        {
            //return _disciplineService.GetDisciplinesAsync();
            try
            {
                var disciplines =await _disciplineService.GetDisciplinesAsync();
                return Ok(disciplines);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{disciplineId:int}")]
        public async Task<ActionResult<DisciplineModel>> GetDisciplineAsync(int disciplineId, string showAthletes)
        {
            try
            {
                bool showAthletesBool;
                if (!Boolean.TryParse(showAthletes, out showAthletesBool))
                    showAthletesBool = false;
                var Discipline = await _disciplineService.GetDisciplineAsync(disciplineId, showAthletesBool);
                return Ok(Discipline);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{disciplineId:int}")]
        public async Task<ActionResult<DisciplineModel>> UpdateDisciplineAsync(int disciplineId, [FromBody] DisciplineModel Discipline)
        {
            try
            {
                //var user = User;
                var updatedDiscipline = await _disciplineService.UpdateDisciplineAsync(disciplineId, Discipline);
                return Ok(updatedDiscipline);
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DisciplineModel>> CreateDisciplineAsync([FromBody] DisciplineModel Discipline)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Discipline.ImagePath = Discipline.ImagePath ?? "Resources\\Images\\genericDiscipline.jfif";
                var newDiscipline = await _disciplineService.CreateDisciplineAsync(Discipline);
                return Created($"/api/Disciplines/{newDiscipline.Id}", newDiscipline);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Form")]
        public async Task<ActionResult<DisciplineModel>> CreateDisciplineFormAsync([FromForm] DisciplineFormModel newDiscipline)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var file = newDiscipline.Image;
                string imagePath = _fileService.UploadFile(file);

                newDiscipline.ImagePath = imagePath;

                var discipline = await _disciplineService.CreateDisciplineAsync(newDiscipline);
                return Created($"/api/disciplines/{discipline.Id}", discipline); //new DisciplineModel() { Id = newBook.Id, Title = newBook.Title, Author = newBook.Author, Genre = newBook.Genre, ImagePath = newBook.ImagePath });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

         [Authorize(Roles = "Admin")]
        [HttpDelete("{disciplineId:int}")]
        public async Task<ActionResult> DeleteDisciplineAsync(int disciplineId)
        {
            try
            {
                await _disciplineService.DeleteDisciplineAsync(disciplineId);
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
        [Authorize(Roles = "Admin")]
        [HttpGet("{disciplineId:int}/worldRankings")]
        public async Task<ActionResult<IEnumerable<ShortAthleteModel>>> GetWorldRankingsAsync(int disciplineId,string gender)
        {
            try
            {
                var worldRankings =  await _disciplineService.GetWorldRankingsAsync(disciplineId,gender);
                return Ok(worldRankings);
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
        [Authorize(Roles = "Admin")]
        [HttpGet("{disciplineId:int}/race")]
        public async Task<ActionResult<IEnumerable<Object>>> RaceAsync(int disciplineId,string gender, string podium = "false")
        {
            try
            {
                var athletes = await _disciplineService.RaceAsync(disciplineId,gender, podium);
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
            catch (NotFoundElementException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NoAthletesToRaceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");
            }

        }


    }
}

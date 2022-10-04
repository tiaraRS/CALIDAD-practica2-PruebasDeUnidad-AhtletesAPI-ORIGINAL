using AthletesRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Services
{
    public interface IDisciplineService
    {

        public Task<IEnumerable<DisciplineModel>> GetDisciplinesAsync();
        public Task<DisciplineModel> GetDisciplineAsync(int disciplineId, bool showAthletes = false);
        public Task<DisciplineModel> UpdateDisciplineAsync(int disciplineId, DisciplineModel discipline);
        public Task DeleteDisciplineAsync(int disciplineId);
        public Task<DisciplineModel> CreateDisciplineAsync (DisciplineModel discipline);

        //endpoints
        public Task<IEnumerable<AthleteModel>> GetWorldRankingsAsync(int disciplineId, string gender);
        public Task<RaceInfoModel> RaceAsync(int disciplineId, string gender, string podium);
    }
}

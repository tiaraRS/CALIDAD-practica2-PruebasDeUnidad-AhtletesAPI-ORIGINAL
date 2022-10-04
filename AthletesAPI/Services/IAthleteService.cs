using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthletesRestAPI.Models;

namespace AthletesRestAPI.Services
{
    public interface IAthleteService
    {
        public Task<IEnumerable<ShortAthleteModel>> GetAthletesAsync(int disciplineId);
        public Task<AthleteModel> GetAthleteAsync(int athleteId, int disciplineId);
        public Task<AthleteModel> UpdateAthleteAsync(int athleteId, AthleteModel athlete, int disciplineId);
        public Task DeleteAthleteAsync(int athleteId, int disciplineId);
        public Task<AthleteModel> CreateAthleteAsync(AthleteModel athlete, int disciplineId);

        //public IEnumerable<Object> Race(string discipline, string gender, string podium);
        //public IEnumerable<ShortAthleteModel> GetRelayTeam(string discipline, string gender, string country, string ageCategory);
    }
}

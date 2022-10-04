using AthletesRestAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Data.Repository
{
    public interface IAthleteRepository
    {
        //ATHLETE
        public Task<IEnumerable<AthleteEntity>> GetAthletesAsync(int disciplineId);
        public Task<AthleteEntity> GetAthleteAsync(int athleteId,int disciplineId);
        public Task UpdateAthleteAsync(int athleteId, AthleteEntity athlete, int disciplineId);
        public Task DeleteAthleteAsync(int athleteId, int disciplineId);

        public void CreateAthlete(AthleteEntity athlete, int disciplineId);

       // public IList<AthleteEntity> Race(string discipline, string gender);

       // public IList<AthleteEntity> GetRelayTeam(string discipline, string gender, string country);

        //DISCIPLINE
        public void CreateDiscipline(DisciplineEntity discipline);
        public Task DeleteDisciplineAsync(int disciplineId);

        public Task<DisciplineEntity> GetDisciplineAsync(int disciplineId, bool showAthletes);
        public Task<IEnumerable<DisciplineEntity>> GetDisciplinesAsync();
        public Task UpdateDisciplineAsync(int disciplineId, DisciplineEntity discipline);

        Task<bool> SaveChangesAsync();
    }
}

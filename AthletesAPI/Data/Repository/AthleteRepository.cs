using AthletesRestAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Data.Repository
{

    public class AthleteRepository:IAthleteRepository
    {
       // private IList<AthleteEntity> _athletes;
        //private IList<DisciplineEntity> _disciplines;

        private AthleteDBContext _dbContext;
        public AthleteRepository(AthleteDBContext athleteDBContext)
        {
            _dbContext = athleteDBContext;
           // _disciplines = new List<DisciplineEntity>();
           // _athletes = new List<AthleteEntity>();
            //DISCIPLINES
            /*_disciplines.Add(new DisciplineEntity()
            {
                Id = 1,
                Name = "100M",
                CreationDate = new DateTime(1500, 8, 21),
                FemaleWorldRecord = 9.85m,
                MaleWorldRecord = 9.58m,
                Rules = "100M has to be run in a straight line"
                //Athletes = new ICollection<AthleteEntity>()
            }) ;
            _disciplines.Add(new DisciplineEntity()
            {
                Id = 2,
                Name = "200M",
                CreationDate = new DateTime(1500, 8, 21),
                FemaleWorldRecord = 19.85m,
                MaleWorldRecord = 19.58m,
                Rules = "200M has to be run in a curve plus a straight line"
            });
            _disciplines.Add(new DisciplineEntity()
            {
                Id = 3,
                Name = "400M",
                CreationDate = new DateTime(1500, 8, 21),
                FemaleWorldRecord = 49.85m,
                MaleWorldRecord = 49.58m,
                Rules = "400M has to be run in a curve plus a straight line"
            });
            _disciplines.Add(new DisciplineEntity()
            {
                Id = 4,
                Name = "Triple Jump",
                CreationDate = new DateTime(1500, 8, 21),
                FemaleWorldRecord = 15.85m,
                MaleWorldRecord = 19.58m,
                Rules = "TRIPLE JUMP"
            });*/
            //ATHLETES
            /*_athletes.Add(new AthleteEntity()
            {
                Id = 1,
                Name = "Usain Bolt",
                //DisciplineId = 1,
                BirthDate = new DateTime(1986, 8, 21),
                Gender = Models.Gender.M,
                IsActive = false,
                Nationality = "Jamaica",
                NumberOfCompetitions = 15,
                PersonalBest = 9.58m
            }) ;
            _athletes.Add(new AthleteEntity()
            {
                Id = 2,
                Name = "Shelly-Ann Fraser-Pryce",
                //DisciplineId = 1,
                BirthDate = new DateTime(1986, 12, 27),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 32,
                PersonalBest = 10.6m,
                SeasonBest = 10.60m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 3,
                Name = "Shericka Jackson",
               // DisciplineId = 1,
                BirthDate = new DateTime(1994, 7, 16),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 25,
                PersonalBest =  10.76m,
                SeasonBest = 10.76m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 4,
                Name = "Elaine Thompson-Herah",
                //DisciplineId = 1,
                BirthDate = new DateTime(1992, 6, 28),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 19,
                PersonalBest = 10.54m,
                SeasonBest = 10.54m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 5,
                Name = "Yohan Blake",
                //DisciplineId = 1,
                BirthDate = new DateTime(1989, 12, 26),
                Gender = Models.Gender.M,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 17,
                PersonalBest = 9.69m,
                SeasonBest = 9.89m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 6,
                Name = "Oblique Seville",
                //DisciplineId = 1,
                BirthDate = new DateTime(2001, 3, 16),
                Gender = Models.Gender.M,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 8,
                PersonalBest = 10.04m,
                SeasonBest = 10.46m
            });

            _athletes.Add(new AthleteEntity()
            {
                Id = 7,
                Name = "Natasha Morrison",
                //DisciplineId = 1,
                BirthDate = new DateTime(1992, 11, 17),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Jamaica",
                NumberOfCompetitions = 10,
                PersonalBest = 11.12m,
                SeasonBest = 11.15m
            });


            //SALTO TRIPLE
            _athletes.Add(new AthleteEntity()
            {
                Id = 1,
                Name = "Allyson Felix",
               // DisciplineId = 3,
                BirthDate = new DateTime(1987, 5, 14),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "USA",
                NumberOfCompetitions = 37,
                PersonalBest = 49.97m
            });            
           
            _athletes.Add(new AthleteEntity()
            {
                Id = 2,
                Name = "Marileidy Paulino",
               // DisciplineId = 3,
                BirthDate = new DateTime(1996, 5, 25),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Dominican Republic",
                NumberOfCompetitions = 21,
                PersonalBest = 49.20m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 3,
                Name = "Sydney McLaughlin",
               // DisciplineId = 3,
                BirthDate = new DateTime(1999, 8, 7),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "USA",
                NumberOfCompetitions = 14,
                PersonalBest = 50.07m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 4,
                Name = "Shaunae Miller-Uibo",
               // DisciplineId = 3,
                BirthDate = new DateTime(1994, 4, 15),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Bahamas",
                NumberOfCompetitions = 30,
                PersonalBest = 48.36m
            });
            //TRIPLE

            _athletes.Add(new AthleteEntity()
            {
                Id = 1,
                Name = "Yulimar Rojas",
                //DisciplineId = 4,
                BirthDate = new DateTime(1995, 10, 21),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Venezuela",
                NumberOfCompetitions = 26,
                PersonalBest = 15.67m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 2,
                Name = "Ana Peleteiro",
                //DisciplineId = 4,
                BirthDate = new DateTime(1995, 12, 2),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Portugal",
                NumberOfCompetitions = 29,
                PersonalBest = 14.87m
            });
            _athletes.Add(new AthleteEntity()
            {
                Id = 3,
                Name = "Caterine Ibargüen",
                //DisciplineId = 4,
                BirthDate = new DateTime(1984, 10, 21),
                Gender = Models.Gender.F,
                IsActive = true,
                Nationality = "Venezuela",
                NumberOfCompetitions = 46,
                PersonalBest = 15.31m
            });*/
          


        }

        public void CreateAthlete(AthleteEntity athlete, int disciplineId)
        {

            _dbContext.Entry(athlete.Discipline).State = EntityState.Unchanged;
            _dbContext.Athletes.Add(athlete);
        }

        public async Task DeleteAthleteAsync(int athleteId, int disciplineId)
        {
            var athleteToDelete = await _dbContext.Athletes.FirstOrDefaultAsync(a => a.Id == athleteId && a.Discipline.Id == disciplineId);           
            _dbContext.Athletes.Remove(athleteToDelete);
        }

        public async Task<AthleteEntity> GetAthleteAsync(int athleteId, int disciplineId)
        {
            IQueryable<AthleteEntity> query = _dbContext.Athletes;
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(a => a.Id == athleteId && a.Discipline.Id == disciplineId);
        }

        public async Task<IEnumerable<AthleteEntity>> GetAthletesAsync(int disciplineId)
        {
            IQueryable<AthleteEntity> query = _dbContext.Athletes;
            query = query.AsNoTracking();

            query = query.Where(a=>a.Discipline.Id == disciplineId);
            var result = await query.ToListAsync(); //aqui va recien a bd

            return result;

        }       

        public async Task UpdateAthleteAsync(int athleteId, AthleteEntity athlete, int disciplineId)
        {
            var athleteToUpdate = await _dbContext.Athletes.FirstOrDefaultAsync(a => a.Id == athleteId && a.Discipline.Id == disciplineId);
            // no modifico el discipline id
           // var athleteToUpdate = _athletes.FirstOrDefault(a => a.Id == athleteId && a.DisciplineId==disciplineId);
             athleteToUpdate.Gender = athlete.Gender ?? athleteToUpdate.Gender;
             athleteToUpdate.BirthDate = athlete.BirthDate ?? athleteToUpdate.BirthDate;            
             athleteToUpdate.Name = athlete.Name ?? athleteToUpdate.Name;
             athleteToUpdate.IsActive = athlete.IsActive ?? athleteToUpdate.IsActive;
             athleteToUpdate.Nationality = athlete.Nationality ?? athleteToUpdate.Nationality;
             athleteToUpdate.NumberOfCompetitions = athlete.NumberOfCompetitions ?? athleteToUpdate.NumberOfCompetitions;
             athleteToUpdate.PersonalBest = athlete.PersonalBest ?? athleteToUpdate.PersonalBest;
             athleteToUpdate.SeasonBest = athlete.SeasonBest ?? athleteToUpdate.SeasonBest;
             athleteToUpdate.Points = athlete.Points ?? athleteToUpdate.Points;

        }

        //DISCIPLINE

        public void CreateDiscipline(DisciplineEntity discipline)
        {

            _dbContext.Disciplines.Add(discipline);
        }

        public async Task DeleteDisciplineAsync(int disciplineId)
        {
            //IQueryable<DisciplineEntity> query = _dbContext.Disciplines;
            var disciplineToDelete = await _dbContext.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);
            var athletesFromDiscipline =  await GetAthletesAsync(disciplineId);
            foreach(var athlete in athletesFromDiscipline)
            {
                await DeleteAthleteAsync(athlete.Id, disciplineId);
            }
           // _dbContext.Entry(disciplineToDelete).State = EntityState.Deleted;
            _dbContext.Disciplines.Remove(disciplineToDelete);
           // return true;
        }

        public async Task<DisciplineEntity> GetDisciplineAsync(int disciplineId, bool showAthletes = false) //aumentar showAthletes ? 
        {
            IQueryable<DisciplineEntity> query = _dbContext.Disciplines;
            query = query.AsNoTracking();
            if (showAthletes)
            {
                query = query.Include(d => d.Athletes);
            }
            return await query.FirstOrDefaultAsync(d => d.Id == disciplineId); 
        }

        public async Task<IEnumerable<DisciplineEntity>> GetDisciplinesAsync()
        {
            IQueryable<DisciplineEntity> query = _dbContext.Disciplines;
            query = query.AsNoTracking();
            query = query.OrderBy(d => d.Id);
            //query = query.Where()
            var result = await query.ToListAsync(); //aqui va recien a bd

            return result;
        }
        public async Task UpdateDisciplineAsync(int disciplineId, DisciplineEntity discipline)
        {
            //_dbContext.Entry(discipline).State = EntityState.Modified;
            var disciplineToUpdate = await _dbContext.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId);
            disciplineToUpdate.Name = discipline.Name ?? disciplineToUpdate.Name;
            disciplineToUpdate.Rules = discipline.Rules ?? disciplineToUpdate.Rules;
            disciplineToUpdate.CreationDate = discipline.CreationDate ?? disciplineToUpdate.CreationDate;
            disciplineToUpdate.FemaleWorldRecord = discipline.FemaleWorldRecord ?? disciplineToUpdate.FemaleWorldRecord;
            disciplineToUpdate.MaleWorldRecord = discipline.MaleWorldRecord ?? disciplineToUpdate.MaleWorldRecord;  
            
           
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync();
                return result > 0? true: false;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}

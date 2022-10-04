using AthletesRestAPI.Data.Entity;
using AthletesRestAPI.Data.Repository;
using AthletesRestAPI.Exceptionss;
using AthletesRestAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Services
{
    public class AthleteService : IAthleteService
    {
        private IAthleteRepository _athleteRepository;
        private IMapper _mapper;

        public Dictionary<string, Func<Decimal?, Decimal, bool>> _markComparer;
        private Dictionary<string, Func<DateTime?, bool>> _ageCategories;
        private Dictionary<string, Func<IEnumerable<Object>, IEnumerable<Object>>> _best3MarksFilter;

        private HashSet<string> _allowedGenderValues = new HashSet<string> { "f", "m", "mix" };
        private HashSet<string> _allowedPodiumValues = new HashSet<string> { "true", "false" };
        private HashSet<string> _allowedRelayDisciplineValues = new HashSet<string> { "100m", "200m", "400m" };

        public AthleteService(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
           // CreateMarkComparer();
            //CreateAgeCategories();
           // CreateBest3MarksFilter();
        }
       
      /*  Func<DateTime?, bool> AgeCategoryComparer(int age)
        {
            return (birthdate) => DateTime.Today.Year - birthdate.Value.Year < age;
        }
        public void CreateAgeCategories()
        {
            _ageCategories = new Dictionary<string, Func<DateTime?, bool>>();
            _ageCategories["under-18"] = AgeCategoryComparer(18);
            _ageCategories["under-20"] = AgeCategoryComparer(20); 
            _ageCategories["under-23"] = AgeCategoryComparer(23);            
            _ageCategories["senior"] = AgeCategoryComparer(34);
            _ageCategories["master"] = (birthdate) => DateTime.Today.Year - birthdate.Value.Year >= 34;
            _ageCategories["all"] = (birthdate) => DateTime.Today.Year - birthdate.Value.Year > 0;

        }
        public void CreateMarkComparer()
        {
            _markComparer = new Dictionary<string, Func<Decimal?,Decimal,bool>>();
            
            _markComparer["400M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["100M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["200M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["1500M"] = (personalMark, mark) => mark < personalMark;

            _markComparer["LongJump"] = (personalMark, mark) => mark > personalMark;
            _markComparer["TripleJump"] = (personalMark, mark) => mark > personalMark;
        }

        IEnumerable<Object> Best3MarkFilterAscending(IEnumerable<Object> competingResults)
        {
            return competingResults.Select(competingResult => new
            {
                Name = competingResult?.GetType().GetProperty("Name")?.GetValue(competingResult, null),
                Mark = competingResult?.GetType().GetProperty("Mark")?.GetValue(competingResult, null)
            }).OrderBy(r => r.Mark).Take(3);
        }

        IEnumerable<Object> Best3MarkFilterDescending(IEnumerable<Object> competingResults)
        {
            return competingResults.Select(competingResult => new
            {
                Name = competingResult?.GetType().GetProperty("Name")?.GetValue(competingResult, null),
                Mark = competingResult?.GetType().GetProperty("Mark")?.GetValue(competingResult, null)
            }).OrderByDescending(r => r.Mark).Take(3);
        }

        public void CreateBest3MarksFilter()
        {
            _best3MarksFilter = new Dictionary<string, Func<IEnumerable<Object>,IEnumerable<Object>>>();
            _best3MarksFilter["400M"] = Best3MarkFilterAscending;
            _best3MarksFilter["200M"] = Best3MarkFilterAscending;
            _best3MarksFilter["100M"] = Best3MarkFilterAscending;
            _best3MarksFilter["1500M"] = Best3MarkFilterAscending;
            _best3MarksFilter["LongJump"] = Best3MarkFilterDescending;
            _best3MarksFilter["TripleJump"] = Best3MarkFilterDescending;
        }
      */
        private async Task<DisciplineEntity> GetDisciplineAsync(int disciplineId)
        {
            var discipline = await _athleteRepository.GetDisciplineAsync(disciplineId,false);
            if (discipline == null)
                throw new NotFoundElementException($"Discipline with id {disciplineId} was not found");
            return discipline;
        }

        public async Task<AthleteModel> CreateAthleteAsync(AthleteModel athlete, int disciplineId)
        {
            await GetDisciplineAsync(disciplineId);
            athlete.DisciplineId = disciplineId;
            var athleteEntity = _mapper.Map<AthleteEntity>(athlete);
             _athleteRepository.CreateAthlete(athleteEntity, disciplineId);
            var result = await _athleteRepository.SaveChangesAsync();

            if (result)
            {
                return map(_mapper.Map<AthleteModel>(athleteEntity));
            }
            throw new Exception("Database Error");
        }

        private AthleteModel map(AthleteModel athlete)
        {
            return new AthleteModel
            {
                Id = athlete.Id,
                Name = athlete.Name,
                Nationality = athlete.Nationality,
                IsActive = athlete.IsActive,
                NumberOfCompetitions = athlete.NumberOfCompetitions,
                DisciplineId = athlete.DisciplineId,
                Gender = athlete.Gender,
                PersonalBest = athlete.PersonalBest,
                SeasonBest = athlete.SeasonBest,
                BirthDate = athlete.BirthDate,
                Points = athlete.Points,
                ImagePath = athlete.ImagePath
            };
        }

        public async Task DeleteAthleteAsync(int athleteId, int disciplineId)
        {
            await GetAthleteAsync(athleteId, disciplineId);
            await _athleteRepository.DeleteAthleteAsync(athleteId,disciplineId);
            var result = await _athleteRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            
        }

        public async Task<AthleteModel> GetAthleteAsync(int athleteId, int disciplineId)
        {
            await GetDisciplineAsync(disciplineId);
            var athleteEntity = await _athleteRepository.GetAthleteAsync(athleteId, disciplineId);
            if (athleteEntity == null)
                throw new NotFoundElementException($"Athlete with id {athleteId} does not exist in discipline {disciplineId}");
            var athleteModel = _mapper.Map<AthleteModel>(athleteEntity);
            athleteModel.DisciplineId = disciplineId;
            return athleteModel;
        }

        public async Task<IEnumerable<ShortAthleteModel>> GetAthletesAsync(int disciplineId)
        {
            await GetDisciplineAsync(disciplineId);
            var athletesListEntity = await _athleteRepository.GetAthletesAsync(disciplineId);
            var athletes = _mapper.Map<IList<ShortAthleteModel>>(athletesListEntity);
            foreach(var athlete in athletes)
            {
                athlete.DisciplineId = disciplineId;
            }
            return athletes;
        }

        public async Task<AthleteModel> UpdateAthleteAsync(int athleteId, AthleteModel athlete, int disciplineId)
        {
            var discipline = await GetDisciplineAsync(disciplineId);
            await GetAthleteAsync(athleteId, disciplineId);
            //athlete.Id = athleteId;
            //athlete.DisciplineId = disciplineId;
            var athleteEntity = _mapper.Map<AthleteEntity>(athlete);
            await _athleteRepository.UpdateAthleteAsync(athleteId, athleteEntity, disciplineId);
            var result = await _athleteRepository.SaveChangesAsync();
            if (result)
            {
                //var athleteModel = _mapper.Map<AthleteModel>(athleteEntity);
                // athleteModel.DisciplineId = disciplineId;
                //athleteEntity.Id = athleteId;
                athleteEntity.Discipline = discipline;
                return _mapper.Map<AthleteModel>(athleteEntity);
            }
            throw new Exception("Database Error");
            
        }

      

       
    }
}

using AthletesRestAPI.Data.Entity;
using AthletesRestAPI.Data.Repository;
using AthletesRestAPI.Exceptionss;
using AthletesRestAPI.Models;
using AthletesRestAPI.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Services
{
    public class DisciplineService : IDisciplineService
    {
        private IAthleteRepository _athleteRepository;
        private IMapper _mapper;

        //query params
        private HashSet<string> _allowedGenderValues = new HashSet<string> { "f", "m", "all" };

        //endpoint RACE
        public Dictionary<string, Func<Decimal?, Decimal, bool>> _markComparer;       
        private Dictionary<string,bool> _best3MarksFilter;

        public DisciplineService(IAthleteRepository disciplineRepository, IMapper mapper)
        {
            _athleteRepository = disciplineRepository;
            _mapper = mapper;
            CreateMarkComparer();           
            CreateBest3MarksFilter();
        }
        public async Task<DisciplineModel> CreateDisciplineAsync(DisciplineModel discipline)
        {
            var disciplineEntity = _mapper.Map<DisciplineEntity>(discipline);
            _athleteRepository.CreateDiscipline(disciplineEntity);
            var result = await _athleteRepository.SaveChangesAsync();
            if (result)
            {
               return  _mapper.Map<DisciplineModel>(disciplineEntity);
            }
            throw new Exception("Database Error");
        }

        public async Task DeleteDisciplineAsync(int disciplineId)
        {
           await GetDisciplineAsync(disciplineId);
           await _athleteRepository.DeleteDisciplineAsync(disciplineId);
           var result = await _athleteRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
           
        }

        public async Task<DisciplineModel> GetDisciplineAsync(int disciplineId, bool showAthletes = false)
        {
            var disciplineEntity = await _athleteRepository.GetDisciplineAsync(disciplineId,showAthletes);
            
            if (disciplineEntity == null)
                throw new NotFoundElementException($"discipline with id {disciplineId} does not exist");
            return _mapper.Map<DisciplineModel>(disciplineEntity);
        }

        public async Task<IEnumerable<DisciplineModel>> GetDisciplinesAsync()
        {
            var disciplineEntityList = await _athleteRepository.GetDisciplinesAsync();
            var disciplines = _mapper.Map<IList<DisciplineModel>>(disciplineEntityList);
            return disciplines;
        }

        public async Task<DisciplineModel> UpdateDisciplineAsync(int disciplineId, DisciplineModel discipline)
        {
            await GetDisciplineAsync(disciplineId);
            var disciplineEntity = _mapper.Map<DisciplineEntity>(discipline);           
            await _athleteRepository.UpdateDisciplineAsync(disciplineId, disciplineEntity);
           // return _mapper.Map<DisciplineModel>(_athleteRepository.UpdateDiscipline(disciplineId, DisciplineEntity));

            var result = await _athleteRepository.SaveChangesAsync();
            if (result)
            {
                disciplineEntity.Id = disciplineId;
                return _mapper.Map<DisciplineModel>(disciplineEntity);
            }
            throw new Exception("Database Error");
        }
        


        //-----------------------------------------ENDPOINTS-------------------------------
        //1. WORLD RANKINGS
        public async Task<IEnumerable<AthleteModel>> GetWorldRankingsAsync(int disciplineId, string gender = "all")
        {
            
            if (!_allowedGenderValues.Contains(gender.ToLower()))
                throw new InvalidElementOperationException($"invalid gender value : {gender}. The allowed values for param are: {string.Join(',', _allowedGenderValues)}");
            var discipline = await _athleteRepository.GetDisciplineAsync(disciplineId, true);
            var athletes = _mapper.Map < IList < AthleteModel >> (discipline.Athletes.ToList());
            athletes = athletes.OrderByDescending(a => a.Points).ToList();
            if (gender != "all")
            {
               athletes =  athletes.Where(a => a.Gender.ToString() == gender).ToList();
            }
            //var listAthletes = _mapper.Map<IList<AthleteModel>>(athletes);
            return athletes;
           
        }

        //2. RACE

        public void CreateMarkComparer()
        {
            _markComparer = new Dictionary<string, Func<Decimal?, Decimal, bool>>();

            _markComparer["600M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["800M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["400M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["400MH"] = (personalMark, mark) => mark < personalMark;
            _markComparer["100MH"] = (personalMark, mark) => mark < personalMark;
            _markComparer["60M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["100M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["200M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["1500M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["5000M"] = (personalMark, mark) => mark < personalMark;
            _markComparer["3000MS"] = (personalMark, mark) => mark < personalMark;
            _markComparer["10000M"] = (personalMark, mark) => mark < personalMark;

            _markComparer["Long Jump"] = (personalMark, mark) => mark > personalMark;
            _markComparer["Pole Vault"] = (personalMark, mark) => mark > personalMark;
            _markComparer["High Jump"] = (personalMark, mark) => mark > personalMark;
            _markComparer["Triple Jump"] = (personalMark, mark) => mark > personalMark;

            _markComparer["Javaline Throw"] = (personalMark, mark) => mark > personalMark;
            _markComparer["Disc Throw"] = (personalMark, mark) => mark > personalMark;
            _markComparer["Hammer Throw"] = (personalMark, mark) => mark > personalMark;
            _markComparer["Shot Put"] = (personalMark, mark) => mark > personalMark;
        }


        public void CreateBest3MarksFilter()
        {
            _best3MarksFilter = new Dictionary<string, bool>();
            _best3MarksFilter["600M"] = true;
            _best3MarksFilter["800M"] = true;
            _best3MarksFilter["400M"] = true;
            _best3MarksFilter["400MH"] = true;
            _best3MarksFilter["100MH"] = true;
            _best3MarksFilter["60M"] = true;
            _best3MarksFilter["100M"] = true;
            _best3MarksFilter["200M"] = true;
            _best3MarksFilter["1500M"] = true;
            _best3MarksFilter["5000M"] = true;
            _best3MarksFilter["10000M"] = true;
            _best3MarksFilter["3000MS"] = true;

            _best3MarksFilter["Pole Vault"] = false;
            _best3MarksFilter["Long Jump"] = false;
            _best3MarksFilter["Triple Jump"] = false;
            _best3MarksFilter["Hight Jump"] = false;

            _best3MarksFilter["Javaline Throw"] = false;
            _best3MarksFilter["Disc Throw"] = false;
            _best3MarksFilter["Hammer Throw"] = false;
            _best3MarksFilter["Shot Put"] = false;
        }
        public Decimal GetRandomMark(Decimal? minimumMark, Decimal? maximumMark)
        {
            Random randomMarkGenerator = new Random();
            int minMarkInt = Convert.ToInt32(minimumMark * 100);
            int maximumMarkInt = Convert.ToInt32(maximumMark * 100);
            Decimal randomMark = randomMarkGenerator.Next(minMarkInt, maximumMarkInt);
            randomMark = randomMark / 100.0m;
            return randomMark;
        }

        public bool CheckPersonalBest(AthleteModel athlete, Decimal mark, string discipline)
        {
            Console.WriteLine(_markComparer);
            var f = _markComparer[discipline];
            bool personalBest = f(athlete.PersonalBest, mark);
            if (personalBest)
            {
                athlete.PersonalBest = mark;
            }
            return personalBest;
        }


        public bool CheckSeasonBest(AthleteModel athlete, Decimal mark, string discipline)
        {
            bool seasonBest = _markComparer[discipline](athlete.SeasonBest, mark);
            if (athlete.SeasonBest == null)
            {
                seasonBest = true;
            }
            if (seasonBest)
            {
                athlete.SeasonBest = mark;
            }
            return seasonBest;
        }
        public Decimal Mark(AthleteModel athlete)
        {
            Decimal mark;
            if (athlete.SeasonBest != null)
            {
                mark = GetRandomMark(athlete.SeasonBest - 0.2m, athlete.SeasonBest + 0.2m);
            }
            else
            {
                mark = GetRandomMark(athlete.PersonalBest - 0.2m, athlete.PersonalBest + 0.5m);
            }
            return mark;
        }

        async Task updateWorldRecord(int disciplineId,decimal worldRecord, string gender)
        {
            if (gender.ToLower() == "f")
            {
                await _athleteRepository.UpdateDisciplineAsync(disciplineId, new DisciplineEntity()
                {
                    FemaleWorldRecord = worldRecord
                });
            }
            else
            {
                await _athleteRepository.UpdateDisciplineAsync(disciplineId, new DisciplineEntity()
                {
                    MaleWorldRecord = worldRecord
                });
            }
        }

        bool checkWorldRecord(string gender,DisciplineModel discipline, RaceInfoModel competingResults,out decimal worldRecord)
        {
            var bestMark = competingResults.GetHighestMark();
            if (_best3MarksFilter[discipline.Name])
            {
                bestMark = competingResults.GetLowestMark();
            }
            worldRecord = bestMark;
            if (gender.ToLower() == "f")
            {
                if (_best3MarksFilter[discipline.Name])
                {
                    return discipline.FemaleWorldRecord > bestMark;
                }
                return discipline.FemaleWorldRecord < bestMark;
            }

            if (gender.ToLower() == "m")
            {
                if (_best3MarksFilter[discipline.Name])
                {
                    return discipline.MaleWorldRecord > bestMark;
                }
                return discipline.MaleWorldRecord < bestMark;
            }
            return false;

        }

        async Task GetAthletesResults(IEnumerable<AthleteModel> racingAthletes, RaceInfoModel competingResults, string disciplineName, int disciplineId)
        {
            foreach (var athlete in racingAthletes)
            {
                var mark = Mark(athlete);
                var personalBest = CheckPersonalBest(athlete, mark, disciplineName);
                var seasonBest = CheckSeasonBest(athlete, mark, disciplineName);
                competingResults.AddRaceAthleteModel(new RaceAthleteModel
                {
                    Id = athlete.Id,
                    Name = athlete.Name,
                    Country = athlete.Nationality,
                    Mark = mark,
                    PB = personalBest,
                    SB = seasonBest
                });
                var athleteUpdateVals = new AthleteEntity()
                {
                    NumberOfCompetitions = athlete.NumberOfCompetitions + 1,
                    Points = athlete.Points == null ? 200 : athlete.Points + 200
                };
                if (personalBest)
                {
                    athleteUpdateVals = new AthleteEntity()
                    {
                        PersonalBest = mark,
                        SeasonBest = mark,
                        NumberOfCompetitions = athlete.NumberOfCompetitions + 1,
                        Points = athlete.Points == null ? 400 : athlete.Points + 400
                    };
                }
                else if(seasonBest){
                    athleteUpdateVals = new AthleteEntity()
                    {
                        SeasonBest = mark,
                        NumberOfCompetitions = athlete.NumberOfCompetitions + 1,
                        Points = athlete.Points == null ? 300 : athlete.Points + 300
                    };
                }
                await _athleteRepository.UpdateAthleteAsync(athlete.Id, athleteUpdateVals, disciplineId);
            }
        }
        public async Task<RaceInfoModel> RaceAsync(int disciplineId,string gender = null, string podium = "false")
        {
            var competingResults = new RaceInfoModel();
            if (gender == null)
            {
                throw new IncompleteRequestException("Unable to complete request. Please specify gender as param");
            }
            var discipline = await GetDisciplineAsync(disciplineId);
            var disciplineName = discipline.Name;
            var athletesListEntity = await _athleteRepository.GetAthletesAsync(disciplineId);
            if (athletesListEntity.ToList().Count==0) throw new NoAthletesToRaceException($"There are no athletes in discipline to perform race");
            var athletes = _mapper.Map<IList<AthleteModel>>(athletesListEntity);            
            var racingAthletes = athletes.Where(a => a.Gender.ToString() == gender && a.IsActive==true);
            if (racingAthletes.ToList().Count == 0) throw new NoAthletesToRaceException($"There are no athletes in discipline with gender {gender} which are active to perform race");
            await GetAthletesResults(racingAthletes, competingResults, disciplineName, disciplineId);

            decimal worldRecord = -1;
            competingResults.WorldRecord = checkWorldRecord(gender, discipline, competingResults,out worldRecord);
            if (_best3MarksFilter[disciplineName])
            {
                competingResults.AthletesRaceInfo = competingResults.AthletesRaceInfo.OrderBy(a => a.Mark).ToList();
            }
            else
            {
                competingResults.AthletesRaceInfo = competingResults.AthletesRaceInfo.OrderByDescending(a => a.Mark).ToList();
            }
            if (podium == "true")
            {
                if (_best3MarksFilter[disciplineName])
                {
                    competingResults.AthletesRaceInfo = competingResults.AthletesRaceInfo.OrderBy(a => a.Mark).Take(3).ToList();
                }
                else
                {
                    competingResults.AthletesRaceInfo = competingResults.AthletesRaceInfo.OrderByDescending(a => a.Mark).Take(3).ToList();
                }
                
            }           
            if (competingResults.WorldRecord) await updateWorldRecord(disciplineId, worldRecord, gender);
            var result = await _athleteRepository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return competingResults;

        }
    }
}

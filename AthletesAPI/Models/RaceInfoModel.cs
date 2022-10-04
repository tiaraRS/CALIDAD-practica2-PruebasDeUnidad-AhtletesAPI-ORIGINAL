using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public class RaceInfoModel
    {
        public IList<RaceAthleteModel> AthletesRaceInfo { get; set; }       
        public bool WorldRecord { get; set; }

        public RaceInfoModel()
        {
            AthletesRaceInfo = new List<RaceAthleteModel>();
        }

        public void AddRaceAthleteModel(RaceAthleteModel raceAthleteModel)
        {
            AthletesRaceInfo.Add(raceAthleteModel);
        }

        public decimal GetLowestMark()
        {
            //AthletesRaceInfo.Select(a => a.Mark).Min();
            return AthletesRaceInfo.Select(a=>a.Mark).Min();
        }

        public decimal GetHighestMark()
        {
            return AthletesRaceInfo.Select(a => a.Mark).Max();
        }
    }
}

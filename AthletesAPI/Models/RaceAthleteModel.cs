using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public class RaceAthleteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public decimal Mark { get; set; }

        public bool PB { get; set; }
        public bool SB { get; set; }
       
    }
}

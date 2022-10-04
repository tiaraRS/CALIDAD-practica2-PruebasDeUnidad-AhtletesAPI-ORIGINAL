using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public class ShortAthleteModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Nationality { get; set; }
        [Required]
        public int DisciplineId { get; set; }
        public Decimal? PersonalBest { get; set; }
        public Decimal? SeasonBest { get; set; }
        public Gender Gender { get; set; }

        public int? NumberOfCompetitions { get; set; }
    }
}

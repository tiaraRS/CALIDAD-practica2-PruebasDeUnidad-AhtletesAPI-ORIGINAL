using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public enum Category
    {
       Sprint, Jumps, Throws, Middle, Long, Relays
    }
    public class DisciplineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rules { get; set; }
        public DateTime? CreationDate { get; set; }

        public Decimal? FemaleWorldRecord { get; set; }
        public Decimal? MaleWorldRecord { get; set; }

        public IEnumerable<AthleteModel> Athletes { get; set; }

        public string ImagePath { get; set; }
    }
}

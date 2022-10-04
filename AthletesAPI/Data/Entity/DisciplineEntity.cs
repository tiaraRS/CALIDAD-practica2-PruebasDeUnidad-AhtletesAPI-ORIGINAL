using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AthletesRestAPI.Data.Entity
{
    public class DisciplineEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rules { get; set; }
        public DateTime? CreationDate { get; set; }

        public Decimal? FemaleWorldRecord { get; set; }
        public Decimal? MaleWorldRecord { get; set; }

        //[JsonIgnore]
        public ICollection<AthleteEntity> Athletes { get; set; }

        //images
        public string ImagePath { get; set; }
    }
}

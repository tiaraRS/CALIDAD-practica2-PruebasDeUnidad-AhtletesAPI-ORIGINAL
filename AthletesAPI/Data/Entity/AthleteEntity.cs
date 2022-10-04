using AthletesRestAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AthletesRestAPI.Data.Entity
{
    public class AthleteEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public bool? IsActive { get; set; }
        public int? NumberOfCompetitions { get; set; }

        //[JsonIgnore]
        [ForeignKey("DisciplineId")]
        public virtual DisciplineEntity Discipline { get; set; }

        public Gender? Gender { get; set; }

        public decimal? PersonalBest { get; set; }

        public decimal? SeasonBest { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? Points { get; set; }

        //images
        public string ImagePath { get; set; }
    }
}

using AthletesRestAPI.Data.Entity;
using AthletesRestAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRestAPI.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<AthleteEntity, AthleteModel>()
                .ForMember(mod => mod.DisciplineId, ent => ent.MapFrom(entSrc => entSrc.Discipline.Id))
                .ReverseMap()
                .ForMember(ent => ent.Discipline, mod => mod.MapFrom(modSrc => new DisciplineEntity() {Id=modSrc.DisciplineId}));

            this.CreateMap<DisciplineEntity, DisciplineModel>()
                .ReverseMap();

            this.CreateMap<AthleteEntity, ShortAthleteModel>()
               .ForMember(mod => mod.DisciplineId, ent => ent.MapFrom(entSrc => entSrc.Discipline.Id))
               .ReverseMap()
               .ForMember(ent => ent.Discipline, mod => mod.MapFrom(modSrc => new DisciplineEntity() { Id = modSrc.DisciplineId }));


        }
    }
}

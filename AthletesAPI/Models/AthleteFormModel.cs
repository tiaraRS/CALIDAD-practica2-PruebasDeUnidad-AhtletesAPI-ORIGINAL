using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public class AthleteFormModel:AthleteModel
    {
        public IFormFile Image { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Models
{
    public class DisciplineFormModel:DisciplineModel
    {
        public IFormFile Image { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Exceptionss
{
    public class NoAthletesToRaceException: Exception
    {
        public NoAthletesToRaceException(string message) : base(message) { }
    }
}

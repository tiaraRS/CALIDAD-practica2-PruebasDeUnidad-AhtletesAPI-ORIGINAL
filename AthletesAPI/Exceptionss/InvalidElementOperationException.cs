﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Exceptionss
{
    public class InvalidElementOperationException:Exception
    {
        public InvalidElementOperationException(string message) : base(message) { }
    }
}

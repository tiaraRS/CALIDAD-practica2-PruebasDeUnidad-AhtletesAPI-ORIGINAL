﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthletesRestAPI.Exceptionss
{
    public class IncompleteRequestException : Exception
    {
        public IncompleteRequestException(string message) : base(message) { }
    }

}
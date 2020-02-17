using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace rubiera
{
    public class Location
    {
        [BindRequired] public double lat { get; set; }
        [BindRequired] public double lon { get; set; }

        public override string ToString()
        {
            return lat + "." + lon;
        }
    }
}
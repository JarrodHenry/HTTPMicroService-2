using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTPMicroService.Models
{
    public class CommandClass
    {
        public int commandType { get; set; }
        public int durationInMillis { get; set; }
    }
}
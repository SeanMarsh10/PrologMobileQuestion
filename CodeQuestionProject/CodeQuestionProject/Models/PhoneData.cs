using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeQuestionProject.Models
{
    public class PhoneData : DataEntity
    {
        public string UserId { get; set; }

        public int IMEI { get; set; }

        public bool Blacklist { get; set; }

        
    }
}

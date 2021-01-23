using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeQuestionProject.Models
{
    public class User : DataEntity
    {
        public string Email { get; set; }

        public int PhoneCount { get; set; }

        public string OrganizationId { get; set; }

        public List<PhoneData> PhoneDataList { get; set; }
    }
}

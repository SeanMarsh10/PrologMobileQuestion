using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeQuestionProject.Models
{
    public class Organization : DataEntity
    {
        public string Name { get; set; }

        public string  BlackListTotal { get; set; }

        public int TotalCount { get; set; }

        public List<User> Users { get; set; }
    }
}

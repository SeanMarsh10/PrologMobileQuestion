using CodeQuestionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeQuestionProject.Interfaces
{
    interface IDataHelper
    {
        Task<List<T>> GetDataList<T>(string path);

    }
}

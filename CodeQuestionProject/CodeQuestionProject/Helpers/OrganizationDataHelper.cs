using CodeQuestionProject.Interfaces;
using CodeQuestionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodeQuestionProject.Helpers
{
    public class OrganizationDataHelper : IDataHelper
    {
        private HttpClient client;


        public OrganizationDataHelper()
        {
            client = new HttpClient();
        }

        public async Task<List<T>> GetDataList<T>(string path)
        {
            var userList = new List<T>();
            try
            {
                
                var response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    userList = response.Content.ReadAsAsync<List<T>>().Result;
                }
            }
            catch(Exception e)
            {
                //I would normally log this, but I think this will work for now.
                Console.WriteLine(e.Message);
            }

            return userList;
        }

    }
}

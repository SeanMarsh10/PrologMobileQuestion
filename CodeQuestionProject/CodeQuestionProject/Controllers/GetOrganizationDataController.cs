using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeQuestionProject.Helpers;
using CodeQuestionProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeQuestionProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetOrganizationDataController : ControllerBase
    {
        private OrganizationDataHelper dataHelper;
        private Dictionary<string, DataEntity> orgsDictionary;
        private const string organizationListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations";
        private const string userListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users";
        private const string phoneListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users/1/phones";

        public GetOrganizationDataController()
        {
            dataHelper = new OrganizationDataHelper();
            orgsDictionary = new Dictionary<string, DataEntity>();
        }
        
        [HttpGet]
        public List<Organization> GetData()
        {
            return CreateOrganizationData();
        }

        private List<Organization> CreateOrganizationData()
        {
            var organizations = dataHelper.GetDataList<Organization>(organizationListUrlPath)
                                          .Result;
            var userList = GetUserList();
            orgsDictionary = BuildDataIntoDictionary(organizations.Cast<DataEntity>()
                                                                      .ToList());
            AssignUserToOrganization(userList);

            return orgsDictionary.Values.Cast<Organization>()
                                        .ToList();
        }

        private void AssignUserToOrganization(List<User> users)
        {
            foreach (var user in users)
            {
                if (orgsDictionary.ContainsKey(user.OrganizationId))
                {
                    var organization = orgsDictionary[user.OrganizationId] as Organization;
                    organization.TotalCount++;
                    organization.BlackListTotal = GetBlackListedTotal(user);
                    if (organization.Users == null)
                    {
                        organization.Users = new List<User>();
                        organization.Users.Add(user);
                    }
                    else
                    {
                        organization.Users.Add(user);
                    }
                }
            }
        }

        private string GetBlackListedTotal(User user)
        {
            var blackListed = 0;
            var organization = orgsDictionary[user.OrganizationId] as Organization;

            if (!string.IsNullOrEmpty(organization.BlackListTotal))
            {
                return organization.BlackListTotal;
            }

            if (user.PhoneDataList == null)
            {
                return blackListed.ToString();
            }
            
            foreach (var phone in user.PhoneDataList)
            {
                if (phone.Blacklist)
                {
                    blackListed++;
                }
            }

            return blackListed.ToString();
        }

        /// <summary>
        /// I want to seperate some these into a dictionary to assign their assignees.
        /// </summary>
        /// <param name="organizations"></param>
        /// <returns></returns>
        private Dictionary<string, DataEntity> BuildDataIntoDictionary(List<DataEntity> dataEntities)
        {
            var dataDictionary = new Dictionary<string, DataEntity>();
            foreach(var data in dataEntities)
            {
                dataDictionary.Add(data.Id, data);
            }
            return dataDictionary;
        }

        private List<User> GetUserList()
        {
            var userList = dataHelper.GetDataList<User>(userListUrlPath).Result;
            var userDictionary = BuildDataIntoDictionary(userList.Cast<DataEntity>()
                                                                 .ToList());

            AssignPhoneToUser(ref userDictionary);

            return userDictionary.Values.Cast<User>()
                                        .ToList();
        }

        private void AssignPhoneToUser(ref Dictionary<string, DataEntity> userDictionary)
        {
            var phoneList = dataHelper.GetDataList<PhoneData>(phoneListUrlPath).Result;
            
            foreach (var phone in phoneList)
            {
                if (userDictionary.Keys.Contains(phone.UserId))
                {
                    var user = userDictionary[phone.UserId] as User;
                    
                    if (user.PhoneDataList == null)
                    {
                        user.PhoneDataList = new List<PhoneData>();
                        user.PhoneDataList.Add(phone);
                        user.PhoneCount++;
                    }
                    else
                    {
                        user.PhoneDataList.Add(phone);
                        user.PhoneCount++;
                    }
                }
            }
        }
    }
}

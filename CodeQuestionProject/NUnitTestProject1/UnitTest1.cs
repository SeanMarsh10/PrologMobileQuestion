using CodeQuestionProject.Helpers;
using CodeQuestionProject.Models;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        private OrganizationDataHelper dataHelper;
        private string userListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users";
        private string organizationListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations";
        private const string phoneListUrlPath = "https://5f0ddbee704cdf0016eaea16.mockapi.io/organizations/1/users/1/phones";

        [SetUp]
        public void Setup()
        {
            dataHelper = new OrganizationDataHelper();
        }

        [Test]
        public void TestGetUserList()
        {
            var testUserList = dataHelper.GetDataList<User>(userListUrlPath)
                                         .Result;

            if (testUserList == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void TestOrganizationList()
        {
            var orgTestList = dataHelper.GetDataList<Organization>(organizationListUrlPath)
                                        .Result;

            if (orgTestList == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void TestPhoneList()
        {
            var phoneTestList = dataHelper.GetDataList<PhoneData>(phoneListUrlPath)
                                        .Result;

            if (phoneTestList == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }


    }
}
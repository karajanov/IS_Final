using BankApplication.Data.DTOs;
using BankApplicationTests.API.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BankApplicationTests.API
{
    public class AccountsControllerTests
    {
        private readonly AccountsServiceSetup serviceSetup;

        public AccountsControllerTests()
        {
           serviceSetup = new AccountsServiceSetup();
        }

        [Fact]
        public async Task GetAccounts_ShouldReturnAllAccounts()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;

            //Actual
            var response = await serviceSetup.GetAsync("Get");

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            var rawAccounts = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject
                <IEnumerable<AccountDTO>>(rawAccounts);


        }
    }
}

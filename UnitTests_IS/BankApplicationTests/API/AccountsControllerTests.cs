using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplicationTests.API.Setup;
using System.Net;
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
        public async Task GetAccounts_ShouldReturnOK()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;

            //Actual
            var response = await serviceSetup.GetAsync("Get");

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task GetAccount_ShouldReturnOK()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var accountId = 1;

            //Actual
            var response = await serviceSetup.GetAsync($"Get/{accountId}");

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task GetAccount_ShouldReturnNoContent()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.NoContent;
            var accountId = 48;

            //Actual
            var response = await serviceSetup.GetAsync($"Get/{accountId}");

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task NewAccount_ShouldReturnCreated()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.Created;
            var accountDto = new AccountDTO
            {
                Name = "New Account",
                Type = AccountType.SavingsAccount,
                Balance = 924m,
                IsActive = true,
                ClientId = 1
            };

            //Actual
            var response = await serviceSetup.PostAsync("NewAccount", accountDto);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task NewAccount_ShouldReturnBadRequest()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var accountDto = new AccountDTO
            {
                Name = null,
                Type = AccountType.SavingsAccount,
                Balance = 924m,
                IsActive = true,
                ClientId = 1
            };

            //Actual
            var response = await serviceSetup.PostAsync("NewAccount", accountDto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAccount_ShouldReturnOK()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var accountDto = new AccountDTO
            {
                Id = 1,
                Name = "Updated Account",
                Type = AccountType.Loan,
                Balance = 592m,
                IsActive = false,
                ClientId = 2
            };

            //Actual
            var response = await serviceSetup.PutAsync($"UpdateAccount/{accountDto.Id}", accountDto);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task UpdateAccount_ShouldReturnBadRequest()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.BadRequest;
            var accountDto = new AccountDTO
            {
                Id = 10,
                Name = null,
                Type = AccountType.Loan,
                Balance = 592m,
                IsActive = false,
                ClientId = 2
            };

            //Actual
            var response = await serviceSetup.PutAsync($"UpdateAccount/{accountDto.Id}", accountDto);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task RemoveAccount_ShouldReturnOK()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var accountId = 11;

            //Actual
            var response = await serviceSetup.DeleteAsync($"RemoveAccount/{accountId}");

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}

using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;
using BankApplication.Service.Repositories;
using BankApplication.Service.Service;
using BankApplicationTests.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BankApplicationTests.Services
{
    public class AccountsServiceTests
    {
        private readonly IMapper mapper;
        private IAccountsRepository accountsRepository;

        public AccountsServiceTests()
        {
            mapper = AutoMapperModule.CreateMapper();
        }

        [Fact]
        public async Task GetAccounts_ShouldReturnCorrectAmount()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var expected = await dbContext.Accounts.CountAsync();

            //Actual
            var actual = accountsRepository
                .GetAccounts()
                .Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAccount_ShouldReturnCorrectAccount()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountId = 1;

            //Actual
            var actual = await accountsRepository.GetAccount(accountId);

            //Assert
            Assert.Equal(accountId, actual.Id);
        }

        [Fact]
        public async Task GetAccount_ShouldReturnNull()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountId = 85;

            //Actual
            var actual = await accountsRepository.GetAccount(accountId);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task SaveAccount_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var expectedCount = await dbContext.Accounts.CountAsync() + 1;
            var accountDto = new AccountDTO
            {
                Name = "New Account",
                Type = AccountType.SavingsAccount,
                Balance = 924m,
                IsActive = true,
                ClientId = 1
            };

            //Actual
            var actual = accountsRepository.SaveAccount(accountDto);
            var actualCount = await dbContext.Accounts.CountAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(accountDto.Name, actual.Name);
            Assert.Equal(accountDto.Type, actual.Type);
            Assert.Equal(accountDto.Balance, actual.Balance);
            Assert.Equal(accountDto.IsActive, actual.IsActive);
            Assert.Equal(accountDto.ClientId, actual.ClientId);
        }

        [Fact]
        public async Task PutAccount_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountDto = new AccountDTO
            {
                Id = 1,
                Name = "Updated Account",
                Type = AccountType.CreditCard,
                Balance = 529m,
                IsActive = true,
                ClientId = 2
            };

            //Actual
            var actual = accountsRepository.PutAccount(accountDto.Id, accountDto);

            //Assert
            Assert.Equal(accountDto.Name, actual.Name);
            Assert.Equal(accountDto.Type, actual.Type);
            Assert.Equal(accountDto.Balance, actual.Balance);
            Assert.Equal(accountDto.IsActive, actual.IsActive);
            Assert.Equal(accountDto.ClientId, actual.ClientId);
        }

        [Fact]
        public async Task PutAccount_ShouldNotWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountDto = new AccountDTO
            {
                Id = 62,
                Name = "Updated Account",
                Type = AccountType.CreditCard,
                Balance = 529m,
                IsActive = true,
                ClientId = 2
            };

            //Actual

            //Assert
            var exception = Assert.Throws<Exception>
                (() => accountsRepository.PutAccount(accountDto.Id, accountDto));

            Assert.Equal("Account not found", exception.Message);
        }

        [Fact]
        public async Task DeleteAccount_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountId = 1;
            var expectedCount = await dbContext.Accounts.CountAsync() - 1;

            //Actual
            var actual = accountsRepository.DeleteAccount(accountId);
            var actualAccount = await dbContext.Accounts.FindAsync(accountId);
            var actualCount = await dbContext.Accounts.CountAsync();

            //Assert
            Assert.True(actual);
            Assert.Null(actualAccount);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task DeleteAccount_ShouldNotWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            accountsRepository = new AccountsService(dbContext, mapper);

            //Arrange
            var accountId = 92;
            var expectedCount = await dbContext.Accounts.CountAsync();

            //Actual
            var actual = accountsRepository.DeleteAccount(accountId);
            var actualCount = await dbContext.Accounts.CountAsync();

            //Assert
            Assert.False(actual);
            Assert.Equal(expectedCount, actualCount);
        }
    }
}

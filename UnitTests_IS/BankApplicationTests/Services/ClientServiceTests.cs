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
    public class ClientServiceTests
    {
        private readonly IMapper mapper;
        private IClientsRepository clientsRepository;

        public ClientServiceTests()
        {
            mapper = AutoMapperModule.CreateMapper();
        }

        [Fact]
        public async Task GetClients_ShouldReturnCorrectAmount()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var expectedAmount = await dbContext.Clients.CountAsync();

            //Actual
            var actual = clientsRepository
                .GetClients()
                .Count();

            //Assert
            Assert.Equal(expectedAmount, actual);
        }

        [Fact]
        public async Task GetClient_ShouldReturnCorrectClient()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientId = 1;

            //Actual
            var actual = await clientsRepository.GetClient(clientId);

            //Assert
            Assert.Equal(clientId, actual.Id);
        }

        [Fact]
        public async Task GetClient_ShouldReturnNull()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientId = 22;

            //Actual
            var actual = await clientsRepository.GetClient(clientId);

            //Assert
            Assert.Null(actual);
        } 

        [Fact]
        public async Task SaveClient_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var expectedCount = await dbContext.Clients.CountAsync() + 1;
            var clientDto = new ClientDTO
            {
                Name = "New Client",
                PhoneNumber = "111-111-111",
                Type = ClientType.Residential,
                Mail = "example@email.com",
                AddressId = 1
            };

            //Actual
            var actual = clientsRepository.SaveClient(clientDto);
            var actualCount = await dbContext.Clients.CountAsync();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(clientDto.Name, actual.Name);
            Assert.Equal(clientDto.PhoneNumber, actual.PhoneNumber);
            Assert.Equal(clientDto.Type, actual.Type);
            Assert.Equal(clientDto.Mail, actual.Mail);
            Assert.Equal(clientDto.AddressId, actual.AddressId);
        }

        [Fact]
        public async Task PutClient_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientDto = new ClientDTO
            {
                Id = 1,
                Name = "Updated Client",
                PhoneNumber = "999-929-391",
                Type = ClientType.Business,
                Mail = "newexample@email.com",
                AddressId = 2
            };

            //Actual
            var actual = clientsRepository.PutClient(clientDto.Id, clientDto);

            //Assert
            Assert.Equal(clientDto.Name, actual.Name);
            Assert.Equal(clientDto.PhoneNumber, actual.PhoneNumber);
            Assert.Equal(clientDto.Type, actual.Type);
            Assert.Equal(clientDto.Mail, actual.Mail);
            Assert.Equal(clientDto.AddressId, actual.AddressId);
        }

        [Fact]
        public async Task PutClient_ShouldNotWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientDto = new ClientDTO
            {
                Id = 19,
                Name = "Updated Client",
                PhoneNumber = "999-929-391",
                Type = ClientType.Business,
                Mail = "newexample@email.com",
                AddressId = 2
            };

            //Actual

            //Assert
            var exception = Assert.Throws<Exception>
                (() => clientsRepository.PutClient(clientDto.Id, clientDto));

            Assert.Equal("Client not found", exception.Message);
        }

        [Fact]
        public async Task DeleteClient_ShouldWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientId = 1;
            var expectedCount = await dbContext.Clients.CountAsync() - 1;

            //Actual
            var actual = clientsRepository.DeleteClient(clientId);
            var actualClient = await dbContext.Clients.FindAsync(clientId);
            var actualCount = await dbContext.Clients.CountAsync();

            //Assert
            Assert.True(actual);
            Assert.Null(actualClient);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task DeleteClient_ShouldNotWork()
        {
            using var factory = new SQLiteDbContextFactory();
            await using var dbContext = factory.CreateContext();
            clientsRepository = new ClientService(dbContext, mapper);

            //Arrange
            var clientId = 24;
            var expectedCount = await dbContext.Clients.CountAsync();

            //Actual
            var actual = clientsRepository.DeleteClient(clientId);
            var actualCount = await dbContext.Clients.CountAsync();

            //Assert
            Assert.False(actual);
            Assert.Equal(expectedCount, actualCount);
        }
    }
}

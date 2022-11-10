using System.Data.SqlClient;
using CarService.DL.Interfaces;
using CarService.Models.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CarService.DL.Repositories.MsSQL
{
    public class ClientRepo : IClientRepo
    {
        private readonly ILogger<ClientRepo> _logger;
        private readonly IConfiguration _configuration;

        public ClientRepo(ILogger<ClientRepo> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Client> AddClient(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Client] (ClientName, City, Age, CarId) VALUES (@ClientName, @City, @Age, @CarId)", client);

                    return client;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddClient)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Client> DeleteClient(int clientId)
        {
            var clientForDelete = await GetById(clientId);

            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("DELETE FROM Client WHERE Id=@id", new {id = clientId});

                    return clientForDelete;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteClient)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Client>("SELECT * FROM Client WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}", e);
            }

            return Enumerable.Empty<Client>();
        }

        public async Task<Client> GetById(int clientId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Client WITH(NOLOCK) WHERE Id=@Id", new { id = clientId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetById)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Client> GetClientByName(string clientName)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Client WITH(NOLOCK) WHERE ClientName = @ClientName", new { ClientName = clientName });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetClientByName)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Client> UpdateClient(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    await conn.QueryFirstOrDefaultAsync("UPDATE Client SET ClientName=@clientName, City=@city, Age=@age, CarId=@carId WHERE Id=@id", 
                        new {id = client.Id, clientName = client.ClientName, city = client.City, age = client.Age, carId = client.CarId});

                    return await GetById(client.Id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateClient)}:{e.Message}", e);
            }

            return null;
        }
    }
}

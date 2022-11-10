using CarService.Models.Models;

namespace CarService.BL.Interfaces
{
    public interface IClientService
    {
        public Task<Client> AddClient(Client client);

        public Task<Client> GetById(int clientId);

        public Task<IEnumerable<Client>> GetAll();

        public Task<Client> UpdateClient(Client client, int id);

        public Task<Client> DeleteClient(int clientId);

        public Task<Client> GetClientByName(string clientName);
    }
}

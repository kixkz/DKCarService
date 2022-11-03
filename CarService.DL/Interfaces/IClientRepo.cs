using CarService.Models.Models;

namespace CarService.DL.Interfaces
{
    public interface IClientRepo
    {
        public Task<Client> AddClient(Client client);

        public Task<Client> GetById(int clientId);

        public Task<IEnumerable<Client>> GetAll();

        public Task<Client> UpdateClient(Client client);

        public Task<Client> DeleteClient(int clientId);

        public Task<Client?> GetClientByName(string clientName);
    }
}

using AutoMapper;
using CarService.BL.Interfaces;
using CarService.DL.Interfaces;
using CarService.Models.Models;

namespace CarService.BL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;

        public ClientService(IClientRepo clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }

        public async Task<Client> AddClient(Client client)
        {
            var cli = await _clientRepo.GetClientByName(client.ClientName);

            if (cli != null)
            {
                return null;
            }

            return await _clientRepo.AddClient(client);
        }

        public async Task<Client> DeleteClient(int clientId)
        {
            return await _clientRepo.DeleteClient(clientId);
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            return _clientRepo.GetAll();
        }

        public async Task<Client> GetById(int clientId)
        {
            return await _clientRepo.GetById(clientId);
        }

        public async Task<Client> UpdateClient(Client client, int id)
        {
            client.Id = id;
            return await _clientRepo.UpdateClient(client);
        }
    }
}

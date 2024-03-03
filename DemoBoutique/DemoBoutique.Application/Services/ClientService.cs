using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Services
{
    public class ClientService : IServiceBase<Client>
    {
        IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Client> AddAsync(Client entity)
        {
            var client = await _unitOfWork.ClientRepository.CreateAsync(entity);
            return client;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _unitOfWork.ClientRepository.Delete(id)
            );
        }

        public async Task<Client> GetById(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            return client;

        }

        public async Task<List<Client>> ListAsync()
        {
            var list = await _unitOfWork.ClientRepository.AllAsync();
            return list;
        }

        public async Task UpdateAsync(Client entity)
        {
           await _unitOfWork.ClientRepository.UpdateAsync(entity);
        }
    }
}

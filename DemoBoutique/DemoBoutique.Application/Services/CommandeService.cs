using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Commande;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Services
{
    public class CommandeService : IServiceBase<Commande>
    {
        IUnitOfWork _unitOfWork;
        public CommandeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Commande> AddAsync(Commande entity)
        {
            var Commande = await _unitOfWork.CommandeRepository.CreateAsync(entity);
            return Commande;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _unitOfWork.CommandeRepository.Delete(id)
            );
        }

        public async Task<Commande> GetById(int id)
        {
            var Commande = await _unitOfWork.CommandeRepository.GetByIdAsync(id);
            return Commande;
        }

        public async Task<List<Commande>> ListAsync()
        {
            var list = await _unitOfWork.CommandeRepository.AllAsync();
            return list;
        }

        public async Task UpdateAsync(Commande entity)
        {
            await _unitOfWork.CommandeRepository.UpdateAsync(entity);
        }
    }
}

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
    public class CommandeLigneService : IServiceBase<CommandeLigne>
    {
        IUnitOfWork _unitOfWork;
        public CommandeLigneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CommandeLigne> AddAsync(CommandeLigne entity)
        {
            var CommandeLigne = await _unitOfWork.CommandeLigneRepository.CreateAsync(entity);
            return CommandeLigne;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _unitOfWork.CommandeLigneRepository.Delete(id)
            );
        }

        public async Task<CommandeLigne> GetById(int id)
        {
            var CommandeLigne = await _unitOfWork.CommandeLigneRepository.GetByIdAsync(id);
            return CommandeLigne;
        }

        public async Task<List<CommandeLigne>> ListAsync()
        {
            var list = await _unitOfWork.CommandeLigneRepository.AllAsync();
            return list;
        }

        public async Task UpdateAsync(CommandeLigne entity)
        {
            await _unitOfWork.CommandeLigneRepository.UpdateAsync(entity);
        }
    }
}

using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Client;
using DemoBoutique.Domain.Produit;
using DemoBoutique.Domain.Produit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Services
{
    public class ProduitService : IServiceBase<Produit>
    {
        IUnitOfWork _unitOfWork;
        public ProduitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Produit> AddAsync(Produit entity)
        {
            var Produit = await _unitOfWork.ProduitRepository.CreateAsync(entity);
            return Produit;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _unitOfWork.ProduitRepository.Delete(id)
            );
        }

        public async Task<Produit> GetById(int id)
        {
            var Produit = await _unitOfWork.ProduitRepository.GetByIdAsync(id);
            return Produit;
        }

        public async Task<List<Produit>> ListAsync()
        {
            var list = await _unitOfWork.ProduitRepository.AllAsync();
            return list;
        }

        public async Task UpdateAsync(Produit entity)
        {
            await _unitOfWork.ProduitRepository.UpdateAsync(entity);
        }
    }
}

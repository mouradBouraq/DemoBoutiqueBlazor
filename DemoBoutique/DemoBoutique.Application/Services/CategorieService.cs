using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.IServices;
using DemoBoutique.Domain.Categorie;
using DemoBoutique.Domain.Categorie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Services
{
    public class CategorieService : IServiceBase<Categorie>
    {
        IUnitOfWork _unitOfWork;
        public CategorieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Categorie> AddAsync(Categorie entity)
        {
            var Categorie = await _unitOfWork.CategorieRepository.CreateAsync(entity);
            return Categorie;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _unitOfWork.CategorieRepository.Delete(id)
            );
        }

        public async Task<Categorie> GetById(int id)
        {
            var Categorie = await _unitOfWork.CategorieRepository.GetByIdAsync(id);
            return Categorie;
        }

        public async Task<List<Categorie>> ListAsync()
        {
            var list = await _unitOfWork.CategorieRepository.AllAsync();
            return list;
        }

        public async Task UpdateAsync(Categorie entity)
        {
            await _unitOfWork.CategorieRepository.UpdateAsync(entity);
        }
    }
}

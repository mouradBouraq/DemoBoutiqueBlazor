using DemoBoutique.Application;
using DemoBoutique.Application.Interfaces;
using DemoBoutique.Application.Interfaces.Repositories;
using DemoBoutique.Infrastructure.Persistence;
using DemoBoutique.Infrastructure.Repos;
using DemoBoutique.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextDemoBoutique _repoContext;
        private readonly Lazy<ICommandeRepository> _commandeRepository;
        private readonly Lazy<IClientRepository> _clientRepository;
        private readonly Lazy<ICommandeLigneRepository> _commandeLigneRepository;
        private readonly Lazy<IProduitRepository> _produitRepository;
        private readonly Lazy<ICategorieRepository> _categorieRepository;


        public UnitOfWork(DbContextDemoBoutique dbContext)
        {
            _repoContext = dbContext;
            _commandeRepository = new Lazy<ICommandeRepository>(() => new CommandeRepository(dbContext));
            _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(dbContext));
            _commandeLigneRepository = new Lazy<ICommandeLigneRepository>(() => new CommandeLigneRepository(dbContext));
            _produitRepository = new Lazy<IProduitRepository>(() => new ProduitRepository(dbContext));
            _categorieRepository = new Lazy<ICategorieRepository>(() => new CategorieRepository(dbContext));
        }

        public ICommandeRepository CommandeRepository => _commandeRepository.Value;

        public IClientRepository ClientRepository => _clientRepository.Value;

        public ICommandeLigneRepository CommandeLigneRepository => _commandeLigneRepository.Value;

        public IProduitRepository ProduitRepository => _produitRepository.Value;

        public ICategorieRepository CategorieRepository => _categorieRepository.Value;

        public void Save()
        {
            _repoContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }

        public void SetCommandTimeout(int timeout)
        {
            _repoContext.Database.SetCommandTimeout(timeout);
        }

        private IDbContextTransaction? _transaction;

        public void BeginTransaction()
        {
            _transaction = _repoContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _repoContext.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            if (!HasActiveTransaction())
                throw new InvalidOperationException("Unable to commit, there is no current transaction");
            var transaction = _transaction;
            _transaction = null;
            transaction!.Commit();
        }

        public async Task CommitAsync()
        {
            if (!HasActiveTransaction())
                throw new InvalidOperationException("Unable to commit, there is no current transaction");
            var transaction = _transaction;
            _transaction = null;
            await transaction!.CommitAsync();
        }

        private bool HasActiveTransaction()
        {
            return _transaction != null && _transaction.GetDbTransaction() != null && _transaction.GetDbTransaction().Connection != null &&
                // Je doute que ce test ait la moindre pertinence, mais pour le moment je me contente de remutualiser de la fonctionnalité dispersée, telle quelle.
                _transaction!.GetDbTransaction()!.Connection!.State == ConnectionState.Open;
        }

        public void Rollback()
        {
            if (!HasActiveTransaction())
                throw new InvalidOperationException("Unable to rollback, there is no current transaction");
            var transaction = _transaction;
            _transaction = null;
            transaction!.Rollback();
        }

        public async Task RollbackAsync()
        {
            if (!HasActiveTransaction())
                throw new InvalidOperationException("Unable to rollback, there is no current transaction");
            var transaction = _transaction;
            _transaction = null;
            await transaction!.RollbackAsync();
        }

        public void ProcessInTransaction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            ProcessInTransaction<object>(() =>
            {
                action();
                return null;
            });
        }

        public T ProcessInTransaction<T>(Func<T> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            // Si on bascule dans un mode où tout serait transactionné par défaut, conviendrait à la place de valider la tran en cours, en démarrer une nouvelle, la finir, et à la fin réouvrir une tran.
            if (HasActiveTransaction())
                throw new InvalidOperationException("A transaction is already ongoing");

            BeginTransaction();
            T result;
            try
            {
                result = function();
            }
            catch
            {
                try
                {
                    if (HasActiveTransaction())
                        Rollback();
                }
                catch (Exception)
#pragma warning disable S125 // Sections of code should not be commented out
                {
                    // Ne pas laisser un sur-plantage bouffer le problème d'origine : logger le sur-plantage sans le relever.
                    //_logger.LogError("An additional error occured while attempting to rollback a transaction after a failed processing.", ex);
                }
#pragma warning restore S125 // Sections of code should not be commented out
                // Relever le plantage de "function".
                throw;
            }
            if (HasActiveTransaction())
                Commit();

            return result;
        }

        public async Task<T> ProcessInTransactionAsync<T>(Func<T> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");
            // Si on bascule dans un mode où tout serait transactionné par défaut, conviendrait à la place de valider la tran en cours, en démarrer une nouvelle, la finir, et à la fin réouvrir une tran.
            if (HasActiveTransaction())
                throw new InvalidOperationException("A transaction is already ongoing");

            await BeginTransactionAsync();
            T result;
            try
            {
                result = function();
            }
            catch
            {
                try
                {
                    if (HasActiveTransaction())
                        await RollbackAsync();
                }
                catch (Exception)
#pragma warning disable S125 // Sections of code should not be commented out
                {
                    // Ne pas laisser un sur-plantage bouffer le problème d'origine : logger le sur-plantage sans le relever.
                    //_logger.LogError("An additional error occured while attempting to rollback a transaction after a failed processing.", ex);
                }
#pragma warning restore S125 // Sections of code should not be commented out
                // Relever le plantage de "function".
                throw;
            }
            if (HasActiveTransaction())
                await CommitAsync();

            return result;
        }
    }
}

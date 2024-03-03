using DemoBoutique.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICommandeRepository CommandeRepository { get; }
        IClientRepository ClientRepository { get; }
        ICommandeLigneRepository CommandeLigneRepository { get; }
        IProduitRepository ProduitRepository { get; }
        ICategorieRepository CategorieRepository { get; }



        void Save();
        Task SaveAsync();

        void SetCommandTimeout(int timeout);
        void BeginTransaction();
        /// <summary>
        /// Saves changes and commit current transaction.
        /// </summary>
        void Commit();
        void Rollback();

        /// <summary>
        /// Encapsulate some processing in a transaction, committing it if no exception was sent back, rollbacking it otherwise.
        /// The <paramref name="action"/> may rollback the transaction itself for cancelation purposes. (Commit supported too.)
        /// </summary>
        /// <param name="action">The action to process.</param>
        void ProcessInTransaction(Action action);
        /// <summary>
        /// Encapsulate some processing in a transaction, committing it if no exception was sent back, rollbacking it otherwise.
        /// The <paramref name="function"/> may rollback the transaction itself for cancelation purposes. (Commit supported too.)
        /// </summary>
        /// <param name="function">The function to process.</param>
        /// <typeparam name="T">Return type of <paramref name="function" />.</typeparam>
        /// <returns>The return value of the function.</returns>
        T ProcessInTransaction<T>(Func<T> function);

        Task<T> ProcessInTransactionAsync<T>(Func<T> function);
    }
}

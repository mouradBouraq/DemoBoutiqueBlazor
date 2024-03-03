using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBoutique.Domain.Models
{
    public class UserIdentity
    {
        public UserIdentity()
        {

        }
        /// <summary>
        /// Constructeur pour création de compte.
        /// </summary>
        public UserIdentity(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public UserIdentity(string id, string email, string name)
        {
            Id = id;
            Email = email;
            Name = name;
        }

        public UserIdentity(string id, string userName, bool isLockedOut, int loginLeftAttempts)
        {
            Id = id;
            Email = userName;
            IsLockedOut = isLockedOut;
            LoginLeftAttempts = loginLeftAttempts;
        }

        /// <summary>
        /// Get aspnet user id
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return Id;
        }

        public string Id { get; set; }

        /// <summary>
        /// Login.
        /// </summary>
        public string Email { get; private set; }


        public string Name { get; private set; }

        /// <summary>
        /// <c>true</c> si le compte est verrouillé, <c>false</c> sinon.
        /// </summary>
        public bool IsLockedOut { get; private set; }

        /// <summary>
        /// Le nombre de tentatives restantes avant blocage du compte
        /// </summary>
        public int LoginLeftAttempts { get; private set; }

        ///// <summary>
        ///// Si l'identité est un compte d'acteur, l'identifiant du Utlisateur lui correspondant.
        ///// </summary>
        public int? UtlisateurId { get; private set; }

    }
}

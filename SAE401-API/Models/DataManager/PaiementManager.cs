using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class PaiementManager<TEntity> : IPaiementRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public PaiementManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<Commande?>> GetCommandeByIdAsync(int idcommande)
        {
            return await _milibooContext.Commandes
                .FirstOrDefaultAsync(c => c.Idcommande == idcommande);
        }

        public async Task<Paiement> AddPaiementAsync(TEntity entity)
        {
            if (entity is Paiement paiement)
            {
                await _milibooContext.Paiements.AddAsync(paiement);
                await _milibooContext.SaveChangesAsync();
                return paiement;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}

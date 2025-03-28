using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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

        public async Task AddPaiementAsync(TEntity entity)
        {
            if (entity is Paiement paiement)
            {
                _milibooContext.Paiements.Add(paiement);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}

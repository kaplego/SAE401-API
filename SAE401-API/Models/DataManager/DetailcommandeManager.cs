using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class DetailcommandeManager<TEntity> : IDetailcommandeRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public DetailcommandeManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }


        public async Task<Detailcommande> AddDetailcommandeAsync(TEntity entity)
        {
            if (entity is Detailcommande detailcommande)
            {
                await _milibooContext.Detailcommandes.AddAsync(detailcommande);
                await _milibooContext.SaveChangesAsync();
                return detailcommande;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Commande?> GetCommandeByIdAsync(int id)
        {
            return await _milibooContext.Commandes.FirstOrDefaultAsync(c => c.Idcommande == id);
        }
    }
}

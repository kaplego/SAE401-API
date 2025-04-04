using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class AimeManager<TEntity> : IAimeRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public AimeManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetAimeByIdAsync(int idclient, int idproduit)
        {
            var aime = await _milibooContext.Aimes
                .Include(a => a.ClientNavigation)  // Inclure les informations sur le client
                .Include(a => a.ProduitNavigation)  // Inclure les informations sur le produit
                .FirstOrDefaultAsync(a => a.Idclient == idclient && a.Idproduit == idproduit);

            return aime != null ? new ActionResult<TEntity>((TEntity)(object)aime) : new NotFoundResult();
        }

        public async Task<Aime> AddAimeAsync(TEntity entity)
        {
            if (entity is Aime aime)
            {
                await _milibooContext.Aimes.AddAsync(aime);
                await _milibooContext.SaveChangesAsync();
                return aime;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task DeleteAimeAsync(TEntity entity)
        {
            if (entity is Aime aime)
            {
                _milibooContext.Aimes.Remove(aime);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}

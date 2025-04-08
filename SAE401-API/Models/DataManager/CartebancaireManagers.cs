using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.DataManager
{
    public class CartebancaireManager<TEntity> : ICartebancaireRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public CartebancaireManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetCartebancaireByIdAsync(int idcartebancaire)
        {
            var carteBancaire = await _milibooContext.Cartebancaires
                .FirstOrDefaultAsync(c => c.Idcartebancaire == idcartebancaire);

            return carteBancaire != null ? new ActionResult<TEntity>((TEntity)(object)carteBancaire) : new NotFoundResult();
        }

        public async Task<ActionResult<IEnumerable<Cartebancaire>>> GetAllCartebancaireByClientAsync(int idclient)
        {
            return await _milibooContext.Cartebancaires
                .Where(c => c.Idclient == idclient).ToListAsync();
        }

        public async Task<Cartebancaire> AddCartebancaireAsync(TEntity entity)
        {
            if (entity is Cartebancaire cartebancaire)
            {
                await _milibooContext.Cartebancaires.AddAsync(cartebancaire);
                await _milibooContext.SaveChangesAsync();
                return cartebancaire;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Cartebancaire> UpdateCartebancaireAsync(Cartebancaire cartebancaire, CartebancaireDTO entity)
        {
            if (cartebancaire is Cartebancaire updatedCartebancaire)
            {
                updatedCartebancaire.Titulairecartebancaire = entity.Titulairecartebancaire;
                updatedCartebancaire.Nomcartebancaire = entity.Nomcartebancaire;
                updatedCartebancaire.Numcartebancaire = entity.Numcartebancaire;
                updatedCartebancaire.Dateenregistement = entity.Dateenregistement;
                updatedCartebancaire.Dateexpirationcarte = entity.Dateexpirationcarte;
            }

            _milibooContext.Entry(cartebancaire).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return cartebancaire;
        }

        public async Task DeleteCartebancaireAsync(Cartebancaire cartebancaire)
        {
            _milibooContext.Cartebancaires.Remove(cartebancaire);
            await _milibooContext.SaveChangesAsync();
        }
    }
}

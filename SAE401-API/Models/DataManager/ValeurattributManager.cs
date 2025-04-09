using SAE401_API.Models.Repository;
using SAE401_API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DTO;

namespace SAE401_API.Models.DataManager
{
    public class ValeurattributManager<TEntity> : IValeurattributRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public ValeurattributManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task<ActionResult<TEntity?>> GetValeurattributByIdAsync(int idattribut, int idproduit)
        {
            var valeurAttribut = await _milibooContext.Valeurattributs
                .Include(v => v.AttributNavigation)  // Inclure les informations sur l'attribut
                .Include(v => v.ProduitNavigation)  // Inclure les informations sur le produit
                .FirstOrDefaultAsync(v => v.Idattribut == idattribut && v.Idproduit == idproduit);

            return valeurAttribut != null ? new ActionResult<TEntity>((TEntity)(object)valeurAttribut) : new NotFoundResult();
        }

        public async Task<Valeurattribut> AddValeurattributAsync(TEntity entity)
        {
            if (entity is Valeurattribut valeurattribut)
            {
                await _milibooContext.Valeurattributs.AddAsync(valeurattribut);
                await _milibooContext.SaveChangesAsync();
                return valeurattribut;
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }

        public async Task<Valeurattribut> UpdateValeurattributAsync(Valeurattribut valeurattribut, ValeurattributDTO entity)
        {
            valeurattribut.Valeur = entity.Valeur;

            _milibooContext.Entry(valeurattribut).State = EntityState.Modified;
            await _milibooContext.SaveChangesAsync();
            return valeurattribut;
        }

        public async Task DeleteValeurattributAsync(Valeurattribut valeurattribut)
        {
            _milibooContext.Valeurattributs.Remove(valeurattribut);
            await _milibooContext.SaveChangesAsync();
        }
    }
}

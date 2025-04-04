using Microsoft.AspNetCore.Mvc;
using SAE401_API.Models.EntityFramework;

namespace SAE401_API.Models.Repository
{
    public interface IValeurattributRepository<TEntity>
    {
        Task<ActionResult<TEntity?>> GetValeurattributByIdAsync(int idattribut, int idproduit);
        Task<Valeurattribut> AddValeurattributAsync(TEntity entity);
        Task<Valeurattribut> UpdateValeurattributAsync(Valeurattribut valeurattribut, TEntity entity);
        Task DeleteValeurattributAsync(Valeurattribut valeurattribut);
    }
}

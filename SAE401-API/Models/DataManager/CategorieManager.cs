using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class CategorieManager : ICategorieRepository<Categorieproduit>
    {
        readonly _DBMilibooContext milibooContext;

        public CategorieManager() { }

        public CategorieManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Categorieproduit>>> GetAllCategorieAsync()
        {
            return await milibooContext.Categorieproduits.ToListAsync();
        }



    }
}

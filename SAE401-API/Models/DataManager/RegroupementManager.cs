using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class RegroupementManager : IRegroupementRepository<Regroupementproduit>
    {
        readonly _DBMilibooContext milibooContext;

        public RegroupementManager() { }

        public RegroupementManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Regroupementproduit>>> GetAllRegroupementAsync()
        {
            return await milibooContext.Regroupementproduits
                                       .ToListAsync();
        }



    }
}

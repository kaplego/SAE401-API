using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;


namespace SAE401_API.Models.DataManager
{
    public class VilleManager : IVilleRepository<Ville>
    {
        readonly _DBMilibooContext milibooContext;

        public VilleManager() { }

        public VilleManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Ville>>> GetAllVilleAsync()
        {
            return await milibooContext.Villes.ToListAsync();
        }
    }
}

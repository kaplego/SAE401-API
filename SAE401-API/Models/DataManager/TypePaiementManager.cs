using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class TypePaiementManager : ITypePaiementRepository<Typepaiement>
    {
        readonly _DBMilibooContext milibooContext;

        public TypePaiementManager() { }

        public TypePaiementManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Typepaiement>>> GetAllTypePaiementAsync()
        {
            return await milibooContext.Typepaiements
                                       .ToListAsync();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class CodePromoManager : ICodePromoRepository<Codepromo>
    {
        readonly _DBMilibooContext milibooContext;

        public CodePromoManager() { }

        public CodePromoManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Codepromo>>> GetAllCodePromoAsync()
        {
            return await milibooContext.Codepromos
                                       .ToListAsync();
        }

    }
}

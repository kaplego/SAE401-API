using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;


namespace SAE401_API.Models.DataManager
{
    public class PaysManager : IPaysRepository<Pay>
    {
        readonly _DBMilibooContext milibooContext;

        public PaysManager() { }

        public PaysManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Pay>>> GetAllPaysAsync()
        {
            return await milibooContext.Pays.ToListAsync();
        }
    }
}

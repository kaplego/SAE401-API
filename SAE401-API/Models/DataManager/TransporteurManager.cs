using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;

namespace SAE401_API.Models.DataManager
{
    public class TransporteurManager : ITransporteurRepository<Transporteur>
    {
        readonly _DBMilibooContext milibooContext;

        public TransporteurManager() { }

        public TransporteurManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Transporteur>>> GetAllTransporteurAsync()
        {
            return await milibooContext.Transporteurs
                                       .ToListAsync();
        }
    }
}

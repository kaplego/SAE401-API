using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Models.DataMethods;
using SAE401_API.Models.DTO;


namespace SAE401_API.Models.DataManager
{
    public class DepartementManager : IDepartementRepository<Departement>
    {
        readonly _DBMilibooContext milibooContext;

        public DepartementManager() { }

        public DepartementManager(_DBMilibooContext context)
        {
            milibooContext = context;
        }

        public async Task<ActionResult<IEnumerable<Departement>>>GetAllDepartementAsync()
        {
            return await milibooContext.Departements.ToListAsync();
        }
    }
}

using SAE401_API.Models.EntityFramework;
using SAE401_API.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SAE401_API.Models.DataManager
{
    public class SignalementaviManager<TEntity> : ISignalementaviRepository<TEntity> where TEntity : class
    {
        private readonly _DBMilibooContext _milibooContext;

        public SignalementaviManager(_DBMilibooContext context)
        {
            _milibooContext = context;
        }

        public async Task AddSignalementaviAsync(TEntity entity)
        {
            if (entity is Signalementavi signalementavi)
            {
                _milibooContext.Signalementavis.Add(signalementavi);
                await _milibooContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entité de type incorrect.");
            }
        }
    }
}

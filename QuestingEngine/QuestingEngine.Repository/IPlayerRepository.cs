using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IPlayerRepository
    {
        Task<ObjectId> CreateAsync(Model.Player player);
        Task<Model.Player> GetAsync(ObjectId id);
        Task<List<Model.Player>> GetAsync();
        Task UpdateAsync(Model.Player player);
    }
}

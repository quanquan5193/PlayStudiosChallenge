using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IQuestRepository
    {
        Task<ObjectId> CreateAsync(Model.Quest quest);
        Task<Model.Quest> GetAsync(ObjectId id);
        Task<List<Model.Quest>> GetAsync();
        Task UpdateAsync(Model.Quest quest);
    }
}

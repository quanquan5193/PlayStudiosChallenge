using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestingEngine.Repository
{
    public interface IMilestoneRepository
    {
        Task<ObjectId> CreateAsync(Model.Milestone milestone);
        Task<Model.Milestone> GetAsync(ObjectId id);
        Task<List<Model.Milestone>> GetAsync();
        Task<List<Model.Milestone>> GetByMultipleId(IEnumerable<ObjectId> milestoneIds);
        Task UpdateAsync(Model.Milestone milestone);
    }
}

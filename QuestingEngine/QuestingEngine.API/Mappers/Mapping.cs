using AutoMapper;
using QuestingEngine.Contract.Requests;
using QuestingEngine.Contract.Responses;
using QuestingEngine.Model;
using QuestingEngine.Service.Commands;
using System.Linq;

namespace QuestingEngine.API.Mappers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Repository.DataModel.Player, Player>()
                .ForMember(e => e.CurrentQuest, opt => opt.Ignore())
                .ForMember(e => e.CompletedMilestones, opt => opt.Ignore());
            CreateMap<Repository.DataModel.Milestone, Milestone>();
            CreateMap<Repository.DataModel.Quest, Quest>()
                .ForMember(e => e.Milestones, opt => opt.Ignore());

            CreateMap<Player, Repository.DataModel.Player>()
                .ForMember(e => e.CurrentQuest, opt => opt.MapFrom(src => src.CurrentQuest.Id))
                .ForMember(e => e.CompletedMilestones, opt => opt.MapFrom(src => src.CompletedMilestones.Select(c => c.Id)));
            CreateMap<Quest, Repository.DataModel.Quest>()
                .ForMember(e => e.Milestones, opt => opt.MapFrom(src => src.Milestones.Select(x => x.Id)));
            CreateMap<Milestone, Repository.DataModel.Milestone>();

            CreateMap<ProgressRequest, UpdateProgressCommand>();
            CreateMap<Milestone, MilestoneResponse>();
        }
    }
}

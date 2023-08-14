using Yoma.Core.Domain.Emsi.Models;

namespace Yoma.Core.Domain.Emsi.Interfaces
{
    public interface IEmsiClient
    {
        Task<List<Skill>?> ListSkills();

        Task<List<JobTitle>?> ListJobTitles();
    }
}

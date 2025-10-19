using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICandidateRepository Candidates { get; }
        ISkillRepository Skills { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}

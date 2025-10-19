using HRPlatform.Domain.Interfaces;
using HRPlatform.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICandidateRepository _candidates;
        private ISkillRepository _skills;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICandidateRepository Candidates =>
            _candidates ??= new CandidateRepository(_context);

        public ISkillRepository Skills =>
            _skills ??= new SkillRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

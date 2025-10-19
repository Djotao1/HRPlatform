using HRPlatform.Domain.Entities;
using HRPlatform.Domain.Interfaces;
using HRPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRPlatform.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate> GetCandidateWithSkillsAsync(int id)
        {
            return await _context.Candidates
                .Include(c => c.Skills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesWithSkillsAsync()
        {
            return await _context.Candidates
                .Include(c => c.Skills)
                .ThenInclude(cs => cs.Skill)
                .ToListAsync();
        }

        public async Task<IEnumerable<Candidate>> SearchCandidatesAsync(string name, List<string> skills)
        {
            var query = _context.Candidates
                .Include(c => c.Skills)
                .ThenInclude(cs => cs.Skill)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.FullName.Contains(name));
            }

            if (skills != null && skills.Any())
            {
                query = query.Where(c => c.Skills.Any(cs => skills.Contains(cs.Skill.Name)));
            }

            return await query.ToListAsync();
        }

        // Remove optional parameter and create two separate methods
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Candidates
                .AnyAsync(c => c.Email.Value == email);
        }

        public async Task<bool> EmailExistsAsync(string email, int excludeCandidateId)
        {
            return await _context.Candidates
                .Where(c => c.Id != excludeCandidateId)
                .AnyAsync(c => c.Email.Value == email);
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task AddAsync(Candidate entity)
        {
            await _context.Candidates.AddAsync(entity);
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _context.Candidates.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Candidate entity)
        {
            _context.Candidates.Remove(entity);
            await Task.CompletedTask;
        }
    }
}
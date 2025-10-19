using HRPlatform.Domain.Entities;
using HRPlatform.Domain.Interfaces;
using HRPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPlatform.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _context.Skills.FindAsync(id);
        }

        public async Task<Skill> GetByNameAsync(string name)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<Skill>> GetSkillsByNamesAsync(List<string> names)
        {
            return await _context.Skills
                .Where(s => names.Contains(s.Name))
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Skills
                .AnyAsync(s => s.Name == name);
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task AddAsync(Skill entity)
        {
            await _context.Skills.AddAsync(entity);
        }

        public async Task UpdateAsync(Skill entity)
        {
            _context.Skills.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Skill entity)
        {
            _context.Skills.Remove(entity);
            await Task.CompletedTask;
        }
    }
}

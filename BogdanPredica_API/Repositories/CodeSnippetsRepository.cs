using BogdanPredica_API.DataContext;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BogdanPredica_API.Repositories
{
    public class CodeSnippetsRepository : ICodeSnippetsRepository
    {

        private readonly ClubLibraDataContext _context;

        public CodeSnippetsRepository(ClubLibraDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CodeSnippet>> GetCodeSnippetsAsync()
        {
            return await _context.CodeSnippets.ToListAsync();
        }

        public async Task<CodeSnippet> GetCodeSnippetByIdAsync(Guid id)
        {
            return await _context.CodeSnippets.SingleOrDefaultAsync(a => a.IdCodeSnippet == id);
        }

        public async Task CreateCodeSnippetAsync(CodeSnippet codeSnippet)
        {
            codeSnippet.IdCodeSnippet = Guid.NewGuid();

            _context.CodeSnippets.Add(codeSnippet);
            await _context.SaveChangesAsync();
        }

        public async Task<CodeSnippet> UpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet)
        {
            if (!await ExistCodeSnippetAsync(id))
            {
                return null;
            }

            if (codeSnippet != null)
            {
                _context.CodeSnippets.Update(codeSnippet);
                await _context.SaveChangesAsync();
            }

            return codeSnippet;
        }

        private async Task<bool> ExistCodeSnippetAsync(Guid id)
        {
            return await _context.CodeSnippets.CountAsync(a => a.IdCodeSnippet == id) > 0;
        }
    }
}

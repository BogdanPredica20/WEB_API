using BogdanPredica_API.DataContext;
using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
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

            bool title = await TitleExists(codeSnippet.Title);
            if(title)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.TitleExistsError);
            }

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

        public async Task<CodeSnippet> UpdatePartiallyCodeSnippetAsync(Guid id, CodeSnippet codeSnippet)
        {
            var codeSnippetFromDatabase = await GetCodeSnippetByIdAsync(id);
            bool codeSnippetIsChanged = false;

            if (codeSnippetFromDatabase == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(codeSnippet.Title) && codeSnippetFromDatabase.Title != codeSnippet.Title)
            {
                codeSnippetFromDatabase.Title = codeSnippet.Title;
                codeSnippetIsChanged = true;
            }

            if (!string.IsNullOrEmpty(codeSnippet.ContentCode) && codeSnippetFromDatabase.ContentCode != codeSnippet.ContentCode)
            {
                codeSnippetFromDatabase.ContentCode = codeSnippet.ContentCode;
                codeSnippetIsChanged = true;
            }

            if (codeSnippet.IdMember != null && codeSnippetFromDatabase.IdMember != codeSnippet.IdMember)
            {
                codeSnippetFromDatabase.IdMember = codeSnippet.IdMember;
                codeSnippetIsChanged = true;
            }

            if (codeSnippet.Revision != null && codeSnippetFromDatabase.Revision != codeSnippet.Revision)
            {
                codeSnippetFromDatabase.Revision = codeSnippet.Revision;
                codeSnippetIsChanged = true;
            }

            if (codeSnippet.DateTimeAdded != null && codeSnippetFromDatabase.DateTimeAdded != codeSnippet.DateTimeAdded)
            {
                codeSnippetFromDatabase.DateTimeAdded = codeSnippet.DateTimeAdded;
                codeSnippetIsChanged = true;
            }

            if (codeSnippet.IsPublished != null && codeSnippetFromDatabase.IsPublished != codeSnippet.IsPublished)
            {
                codeSnippetFromDatabase.IsPublished = codeSnippet.IsPublished;
                codeSnippetIsChanged = true;
            }

            if (!codeSnippetIsChanged)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.ZeroUpdatesToSave);
            }

            _context.Update(codeSnippetFromDatabase);
            await _context.SaveChangesAsync();
            return codeSnippetFromDatabase;
        }

        public async Task<bool> DeleteCodeSnipetAsync(Guid id)
        {
            if (!await ExistCodeSnippetAsync(id))
            {
                return false;
            }

            _context.CodeSnippets.Remove(new CodeSnippet { IdCodeSnippet = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistCodeSnippetAsync(Guid id)
        {
            return await _context.CodeSnippets.CountAsync(a => a.IdCodeSnippet == id) > 0;
        }

        private async Task<bool> TitleExists(string title)
        {
            return await _context.CodeSnippets.CountAsync(a => a.Title == title) > 0;
        }
    }
}

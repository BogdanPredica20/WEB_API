using BogdanPredica_API.Models;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface ICodeSnippetsRepository
    {
        public Task<IEnumerable<CodeSnippet>> GetCodeSnippetsAsync();
        Task<CodeSnippet> GetCodeSnippetByIdAsync(Guid id);
        Task CreateCodeSnippetAsync(CodeSnippet codeSnippet);
        Task<CodeSnippet> UpdateCodeSnippetAsync(Guid id, CodeSnippet codeSnippet);
    }
}

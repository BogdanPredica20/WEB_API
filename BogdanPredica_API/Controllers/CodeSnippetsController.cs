using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Repositories;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BogdanPredica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSnippetsController : ControllerBase
    {

        private ICodeSnippetsRepository _codeSnippetsRepository;
        private readonly ILogger<CodeSnippetsController> _logger;

        public CodeSnippetsController(ICodeSnippetsRepository codeSnippetsRepository, ILogger<CodeSnippetsController> logger)
        {
            _codeSnippetsRepository = codeSnippetsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var codeSnippets = await _codeSnippetsRepository.GetCodeSnippetsAsync();
                if (codeSnippets == null || codeSnippets.Count() < 1)
                {
                    _logger.LogInformation(ErrorMessagesEnum.CodeSnippet.NotFound);
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.CodeSnippet.NotFound);
                }
                return Ok(codeSnippets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAnnouncements error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            try
            {
                var codeSnippet = await _codeSnippetsRepository.GetCodeSnippetByIdAsync(id);
                if (codeSnippet == null)
                {
                    _logger.LogInformation(ErrorMessagesEnum.CodeSnippet.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.CodeSnippet.NotFoundById);
                }
                return Ok(codeSnippet);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Post([FromBody] CodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                await _codeSnippetsRepository.CreateCodeSnippetAsync(codeSnippet);
                return Ok(SuccessMessagesEnum.CodeSnippet.CodeSnippetAdded);
            }
            catch (ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create code snippet error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                codeSnippet.IdCodeSnippet = id;
                var updatedCodeSnippet = await _codeSnippetsRepository.UpdateCodeSnippetAsync(id, codeSnippet);
                if (updatedCodeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.CodeSnippet.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.CodeSnippet.CodeSnippetUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update code snippet error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] CodeSnippet codeSnippet)
        {
            try
            {
                if (codeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                var updatedCodeSnippet = await _codeSnippetsRepository.UpdatePartiallyCodeSnippetAsync(id, codeSnippet);
                if (updatedCodeSnippet == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.CodeSnippet.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.CodeSnippet.CodeSnippetUpdated);
            }
            catch (ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update code snippet error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _codeSnippetsRepository.DeleteCodeSnipetAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters code snippet cu id-ul: {id}");
                    return Ok(SuccessMessagesEnum.CodeSnippet.CodeSnippetDeleted);
                }
                _logger.LogInformation($"Code snippet cu id-ul {id} nu a fost gasit pentru a fi sters");
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.CodeSnippet.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete code snippet error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

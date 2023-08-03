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
    public class MembersController : ControllerBase
    {

        private IMembersRepository _membersRepository;
        private readonly ILogger<AnnouncementsController> _logger;

        public MembersController(IMembersRepository membersRepository, ILogger<AnnouncementsController> logger)
        {
            _membersRepository = membersRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogWarning("GetMembers started");
                var members = await _membersRepository.GetMembersAsync();
                if (members == null || members.Count() < 1)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Member.NotFound);
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.Member.NotFound);
                }
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetMembers error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("GetMember by id started");
                var member = await _membersRepository.GetMemberByIdAsync(id);
                if (member == null)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Member.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Member.NotFoundById);
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                await _membersRepository.CreateMemberAsync(member);
                return Ok(SuccessMessagesEnum.Member.MemberAdded);
            }
            catch (ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create member error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                member.IdMember = id;
                var updatedMember = await _membersRepository.UpdateMemberAsync(id, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Member.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.Member.MemberUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update member error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] Member member)
        {
            try
            {
                if (member == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                var updatedMember = await _membersRepository.UpdatePartiallyMemberAsync(id, member);
                if (updatedMember == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Member.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.Member.MemberUpdated);
            }
            catch(ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update announcement error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                
                var result = await _membersRepository.DeleteMemberAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters membrul cu id-ul: {id}");
                    return Ok(SuccessMessagesEnum.Member.MemberDeleted);
                }
                _logger.LogInformation($"Membrul cu id-ul {id} nu a fost gasit pentru a fi sters sau are code snippets atasate.");
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Member.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete member error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

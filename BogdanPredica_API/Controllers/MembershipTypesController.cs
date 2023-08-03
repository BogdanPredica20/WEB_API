using BogdanPredica_API.Exceptions;
using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories;
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
    public class MembershipTypesController : ControllerBase
    {
        private IMembershipTypesRepository _membershipTypeRepository;
        private readonly ILogger<AnnouncementsController> _logger;

        public MembershipTypesController(IMembershipTypesRepository membersRepository, ILogger<AnnouncementsController> logger)
        {
            _membershipTypeRepository = membersRepository;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogWarning("GetMembershipTypes started");
                var membershipTypes = await _membershipTypeRepository.GetMembershipTypesAsync();
                if (membershipTypes == null || membershipTypes.Count() < 1)
                {
                    _logger.LogInformation(ErrorMessagesEnum.MembershipType.NotFound);
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.MembershipType.NotFound);
                }
                return Ok(membershipTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetMembershipTypes error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetMembershipType by id started");
                var membershipType = await _membershipTypeRepository.GetMembershipTypeByIdAsync(id);
                if (membershipType == null)
                {
                    _logger.LogInformation(ErrorMessagesEnum.MembershipType.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.MembershipType.NotFoundById);
                }
                return Ok(membershipType);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MembershipType membershipType)
        {
            try
            {
                if (membershipType == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                await _membershipTypeRepository.CreateMembershipTypeAsync(membershipType);
                return Ok(SuccessMessagesEnum.MembershipType.MembershipTypeAdded);
            }
            catch (ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create membership type error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] MembershipType membershipType)
        {
            try
            {
                if (membershipType == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                membershipType.IdMembershipType = id;
                var updatedMembershipType = await _membershipTypeRepository.UpdateMembershipTypeAsync(id, membershipType);
                if (updatedMembershipType == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.MembershipType.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.MembershipType.MembershipTypeUpdated);
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
                var result = await _membershipTypeRepository.DeleteMembershipTypeAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters tipul de membership cu id-ul: {id}");
                    return Ok(SuccessMessagesEnum.MembershipType.MembershipTypeDeleted);
                }
                _logger.LogInformation($"Tipul de membership cu id-ul {id} nu a fost gasit pentru a fi sters");
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.MembershipType.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete member error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

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
    public class MembershipsController : ControllerBase
    {
        private IMembershipsRepository _membershipsRepository;
        private readonly ILogger<AnnouncementsController> _logger;

        public MembershipsController(IMembershipsRepository membershipsRepository, ILogger<AnnouncementsController> logger)
        {
            _logger = logger;
            _membershipsRepository = membershipsRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                _logger.LogWarning("GetMemberships started");
                var memberships = await _membershipsRepository.GetMembershipsAsync();
                if (memberships == null || memberships.Count() < 1)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Membership.NotFound);
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.Membership.NotFound);
                }
                return Ok(memberships);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetMemberships error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetMembership by id started");
                var membership = await _membershipsRepository.GetMembershipByIdAsync(id);
                if (membership == null)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Membership.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Membership.NotFoundById);
                }
                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Membership membership)
        {
            try
            {
                if (membership == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                await _membershipsRepository.CreateMembershipAsync(membership);
                return Ok(SuccessMessagesEnum.Membership.MembershipAdded);
            }
            catch (ModelValidationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create membership error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Membership membership)
        {
            try
            {
                if (membership == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                membership.IdMembership = id;
                var updatedMembership = await _membershipsRepository.UpdateMembershipAsync(id, membership);
                if (updatedMembership == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Membership.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessagesEnum.Membership.MembershipUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update membership error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _membershipsRepository.DeleteMembershipAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters membership-ul cu id-ul: {id}");
                    return Ok(SuccessMessagesEnum.Membership.MembershipDeleted);
                }
                _logger.LogInformation($"Membership-ul cu id-ul {id} nu a fost gasit pentru a fi sters");
                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Membership.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete membership error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

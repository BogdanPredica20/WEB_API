using BogdanPredica_API.Helpers.Enums;
using BogdanPredica_API.Models;
using BogdanPredica_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BogdanPredica_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private IAnnouncementsRepository _announcementsRepository;
        private readonly ILogger<AnnouncementsController> _logger;

        public AnnouncementsController(IAnnouncementsRepository announcementsRepository, ILogger<AnnouncementsController> logger)
        {
            _announcementsRepository = announcementsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var announcements = await _announcementsRepository.GetAnnouncementsAsync();
                if(announcements == null || announcements.Count() < 1)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Announcement.NotFound);
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.Announcement.NotFound); // (tip)valoare -> conversie explicita
                }
                return Ok(announcements);
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
                var announcement = await _announcementsRepository.GetAnnouncementByIdAsync(id);
                if(announcement == null)
                {
                    _logger.LogInformation(ErrorMessagesEnum.Announcement.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessagesEnum.Announcement.NotFoundById);
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Announcement announcement)
        {
            try
            {
                if(announcement == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessagesEnum.Announcement.BadRequest);
                }
                await _announcementsRepository.CreateAnnouncementAsync(announcement);
                return Ok(SuccessMessagesEnum.Announcement.AnnouncementAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create announcement error occured {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

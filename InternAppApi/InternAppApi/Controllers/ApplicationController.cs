using Microsoft.AspNetCore.Mvc;
using InternAppApi.Services.ApplicationService;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.ApplicationCommentDtos;
using Microsoft.AspNetCore.Authorization;


namespace InternAppApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;


        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _applicationService.GetApplications());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _applicationService.GetApplicationById(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add(AddApplicationDto newApp)
        {
            return Ok(await _applicationService.AddApplication(newApp));
        }

        [HttpPost("Comment")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentReq addComment)
        {
            return Ok(await _applicationService.AddComment(addComment.applicationId, addComment.body));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateApplicationDto updatedApplication)
        {
            var response = await _applicationService.UpdateApplication(updatedApplication);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetSelected")]
        public async Task<IActionResult> GetSelectedApplicants()
        {
            return Ok(await _applicationService.GetSelectedApplicants());
        }

        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPage(int pageIndex, int pageSize, string? searchName, EducationLevel? eduLevel, Status? status, string? sort)
        {
            var response = await _applicationService.ApplicationPaging(pageIndex, pageSize, searchName, eduLevel, status, sort);

            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}

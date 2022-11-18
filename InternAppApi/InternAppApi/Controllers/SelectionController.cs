
using Microsoft.AspNetCore.Mvc;
using InternAppApi.Dtos.SelectionDtos;
using InternAppApi.Dtos.Paging;
using InternAppApi.Services.SelectionService;
using InternAppApi.Dtos.SelectionComment;
using Microsoft.AspNetCore.Authorization;
using InternAppApi.Dtos.ApplicationDtos;

namespace InternAppApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SelectionController : ControllerBase
    {
        private readonly ISelectionService _selectionService;

        public SelectionController(ISelectionService selectionService)
        {
            _selectionService = selectionService; 
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult>Get()
        {    
            return Ok(await _selectionService.GetAllSelections());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {   
            return Ok(await _selectionService.GetSelectionsById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSelectionDto newSelection)
        {
            return Ok(await _selectionService.AddSelection(newSelection));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateSelectionDto updatedSelection)
        {
            var response=await _selectionService.UpdateSelection(updatedSelection);
            if(response.Data==null) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("comments/{selectionId}")]
        public async Task<IActionResult> GetCommentById(int selectionId)
        {
            return Ok(await _selectionService.GetCommentByUserId(selectionId));
        }

        [HttpDelete("applicants")]
        public async Task<IActionResult> DeleteSelectionApplicant([FromQuery]int selectionId,[FromQuery]int applicantId)
        {
            var response = await _selectionService.DeleteSelectionApplicant(selectionId,applicantId);
            if (response.Data == null) return NotFound(response);
            return Ok(response);
        }

        [HttpPost("comments")]
        public async Task<ActionResult<ServiceResponse<List<GetSelectionCommentDto>>>> AddSelectionComment(AddSelectionCommentDto newComment)
        {
           return(await _selectionService.AddSelectionComment(newComment));    
        }

        [HttpPost("Applicant")]
        public async Task<IActionResult> AddSelectionApplicant(AddApplicantDto newApplicant)
        {
            return Ok(await _selectionService.AddSelectionApplicant(newApplicant));
        }

        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPaginatedList(int pageIndex, int pageSize,string? name, string? description)
        {
           var response = await _selectionService.SelectionsPaging(pageIndex,pageSize,name,description);
           if(response.Data==null) return BadRequest(response);
           return Ok(response);    
        }
    }
}
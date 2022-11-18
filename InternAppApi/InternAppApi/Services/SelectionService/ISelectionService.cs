using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.Paging;
using InternAppApi.Dtos.SelectionComment;
using InternAppApi.Dtos.SelectionDtos;

namespace InternAppApi.Services.SelectionService
{
    public interface ISelectionService
    {
        Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections();
        Task<ServiceResponse<GetSelectionDto>> GetSelectionsById(int id);
        Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection);
        Task<ServiceResponse<GetSelectionDto>> UpdateSelection(UpdateSelectionDto updatedSelection);
        Task<ServiceResponse<PagedListDto<GetSelectionDto>>> SelectionsPaging(int pageIndex, int pageSize, string? search, string? description);
        Task<ServiceResponse<List<GetSelectionCommentDto>>> GetCommentByUserId(int id);
        Task<ServiceResponse<List<GetSelectionCommentDto>>> AddSelectionComment(AddSelectionCommentDto newComment);
        Task<ServiceResponse<GetApplicationDto>> AddSelectionApplicant(AddApplicantDto newApplicant);
        Task<ServiceResponse<GetSelectionDto>> DeleteSelectionApplicant(int selectionId, int applicantId);
    }
}
using InternAppApi.Dtos.ApplicationCommentDtos;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.Paging;

namespace InternAppApi.Services.ApplicationService
{
    public interface IApplicationService
    {
        Task<ServiceResponse<List<GetApplicationDto>>> GetApplications();
        Task<ServiceResponse<GetApplicationDto>> GetApplicationById(int id);
        Task<ServiceResponse<List<GetApplicationDto>>> AddApplication(AddApplicationDto newApplication);
        Task<ServiceResponse<List<GetCommentDto>>> GetCommentByApplicationId(int id);
        Task<ServiceResponse<GetApplicationDto>> UpdateApplication(UpdateApplicationDto updatedApplication);
        Task<ServiceResponse<PagedListDto<GetApplicationDto>>> ApplicationPaging(int pageIndex, int pageSize, string? searchName, EducationLevel? filterEduLevel, Status? filterStatus, string? sort);
        Task<ServiceResponse<GetCommentDto>> AddComment(int applicationId, string body);
        Task<ServiceResponse<List<GetApplicationDto>>> GetSelectedApplicants();

    }
}
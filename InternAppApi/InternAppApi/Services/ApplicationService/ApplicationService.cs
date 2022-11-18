using AutoMapper;
using InternAppApi.Data;
using InternAppApi.Dtos.ApplicationCommentDtos;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.Paging;
using InternAppApi.Helpers;
using InternAppApi.Services.MailService;
using Microsoft.EntityFrameworkCore;

namespace InternAppApi.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationService(IMapper mapper, DataContext context, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mailService = mailService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetApplicationDto>>> AddApplication(AddApplicationDto newApplication)
        {
            var serviceResponse = new ServiceResponse<List<GetApplicationDto>>();

            Application app = _mapper.Map<Application>(newApplication);
            _context.Applications.Add(app);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Applications
                .Select(a => _mapper.Map<GetApplicationDto>(a))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetApplicationDto>> GetApplicationById(int id)
        {
            var serviceResponse = new ServiceResponse<GetApplicationDto>();

            var dbApplications = await _context.Applications
                .Include(s => s.Comments)
                .Include(s=> s.Selections)
                .FirstOrDefaultAsync(a => a.Id == id);
            serviceResponse.Data = _mapper.Map<GetApplicationDto>(dbApplications);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetApplicationDto>>> GetApplications()
        {
            var response = new ServiceResponse<List<GetApplicationDto>>();

            var dbApplications = await _context.Applications
                .Include(s => s.Comments)
                .Include(s=> s.Selections)
                .ToListAsync();
            response.Data = dbApplications.Select(a => _mapper.Map<GetApplicationDto>(a))
                .ToList();

            return response;
        }

        public async Task<ServiceResponse<List<GetCommentDto>>> GetCommentByApplicationId(int id)
        {
            var response = new ServiceResponse<List<GetCommentDto>>();

            var dbComments = await _context.ApplicationComment
                .Where(c => c.Id == id)
                .ToListAsync();
            response.Data = dbComments.Select(comments => _mapper.Map<GetCommentDto>(comments))
                .ToList();

            return response;
        }

        public async Task<ServiceResponse<GetApplicationDto>> UpdateApplication(UpdateApplicationDto updatedApplication)
        {
            ServiceResponse<GetApplicationDto> response = new ServiceResponse<GetApplicationDto>();

            try
            {
                var application = await _context.Applications
                    .FirstOrDefaultAsync(c => c.Id == updatedApplication.Id);

                application.FirstName = updatedApplication.FirstName;
                application.LastName = updatedApplication.LastName;
                application.email = updatedApplication.email;
                application.EducationLevel = updatedApplication.EducationLevel;
                application.CV = updatedApplication.CV;
                application.CoverLetter = updatedApplication.CoverLetter;
                application.Status = updatedApplication.Status;

                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetApplicationDto>(application);

                if (updatedApplication.Status == Status.InSelection)
                {
                    await _mailService.SendEmailAsync(updatedApplication.email, "Welcome to the internship!", "You have been selected for the Mistral internship");
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<PagedListDto<GetApplicationDto>>> ApplicationPaging(int pageIndex, int pageSize, string? searchName, EducationLevel? filterEduLevel, Status? status, string? sort)
        {
            var response = new ServiceResponse<PagedListDto<GetApplicationDto>>();

            IQueryable<Application> apps = _context.Applications;

            //Filtering
            if (searchName != null) apps = apps.Where(s => s.FirstName.ToLower().StartsWith(searchName.ToLower()) || s.FirstName.ToLower().StartsWith(searchName.ToLower()));
            if (filterEduLevel != null) apps = apps.Where(s => s.EducationLevel == filterEduLevel);
            if (status != null) apps = apps.Where(s => s.Status == status);

            //Sorting
            if (sort != null)
            {
                switch (sort)
                {
                    case "firstNameasc":
                        apps = apps.OrderBy(a => a.FirstName);
                        break;
                    case "firstNamedesc":
                        apps = apps.OrderByDescending(a => a.FirstName);
                        break;
                    case "lastNameasc":
                        apps = apps.OrderBy(a => a.LastName);
                        break;
                    case "lastNamedesc":
                        apps = apps.OrderByDescending(a => a.LastName);
                        break;
                    case "Iddesc":
                        apps = apps.OrderByDescending(a => a.Id);
                        break;
                    default:
                        apps = apps.OrderBy(a => a.Id);
                        break;
                }
            }

            var pageList = await PaginatedList<Application>.CreateAsync(apps, pageIndex, pageSize);

            var output = new PagedListDto<GetApplicationDto>
            {
                Data = _mapper.Map<List<GetApplicationDto>>(pageList),
                TotalPages = pageList.TotalPages,
                PageIndex = pageList.PageIndex,
                HasPreviousPage = pageList.HasPreviousPage,
                HasNextPage = pageList.HasNextPage
            };
            response.Data = output;

            return response;
        }

        private string GetUserName() => _httpContextAccessor.HttpContext.User.Identity.Name;

        public async Task<ServiceResponse<GetCommentDto>> AddComment(int applicationId, string body)
        {
            {
                var serviceResponse = new ServiceResponse<GetCommentDto>();

                try
                {
                    var application = await _context.Applications.FirstOrDefaultAsync(application => application.Id == applicationId);
                    if (application != null)
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(GetUserName()));
                        var comment = new ApplicationComment();

                        comment.Body = body;
                        comment.UserId = user.Id;
                        comment.UserName = user.UserName;

                        application.Comments.Add(comment);
                        await _context.SaveChangesAsync();
                        serviceResponse.Data = _mapper.Map<GetCommentDto>(comment);
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Application not found";
                    }
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<GetApplicationDto>>> GetSelectedApplicants()
        {
            ServiceResponse<List<GetApplicationDto>> serviceResponse = new ServiceResponse<List<GetApplicationDto>>();

            var applicants = await _context.Applications
                .Where(a => a.Status.Equals(Status.InSelection))
                .ToListAsync();

            if (applicants == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Applicants not found.";
            }
            serviceResponse.Data = _mapper.Map<List<GetApplicationDto>>(applicants);

            return serviceResponse;
        }
    }
}
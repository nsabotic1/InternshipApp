using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternAppApi.Data;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.Paging;
using InternAppApi.Dtos.SelectionComment;
using InternAppApi.Dtos.SelectionDtos;
using InternAppApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InternAppApi.Services.SelectionService
{
    public class SelectionService : ISelectionService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SelectionService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetSelectionDto>> AddSelection(AddSelectionDto newSelection)
        {
            var serviceResponse = new ServiceResponse<GetSelectionDto>();
            var selection = _mapper.Map<Selection>(newSelection);
            _context.Selections.Add(selection);
            await _context.SaveChangesAsync(); 
            var mapSelections = _mapper.Map<GetSelectionDto>(selection);
            serviceResponse.Data=_mapper.Map<GetSelectionDto>(selection);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSelectionDto>> UpdateSelection(UpdateSelectionDto updatedSelection)
        {
            var serviceResponse = new ServiceResponse<GetSelectionDto>();
            try{
                var selection=await _context.Selections.FirstOrDefaultAsync(s=>s.Id==updatedSelection.Id);
                 _mapper.Map(updatedSelection,selection);
                await _context.SaveChangesAsync(); 
                serviceResponse.Data=_mapper.Map<GetSelectionDto>(selection);
                serviceResponse.Message="Your selection has been updated !";
            }catch(Exception e){
                serviceResponse.Success=false;
                serviceResponse.Message=e.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSelectionDto>>> GetAllSelections()
        {
            var serviceResponse = new ServiceResponse<List<GetSelectionDto>>();
            var selections = await _context.Selections
            .Include(s => s.Applications)
            .ToListAsync();
            serviceResponse.Data = selections.Select(selections => _mapper.Map<GetSelectionDto>(selections)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSelectionDto>> GetSelectionsById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSelectionDto>();
            var selection = await _context.Selections
            .Include(s => s.Applications)
            .Where(selection => selection.Id == id)
            .SingleOrDefaultAsync();
            serviceResponse.Data = _mapper.Map<GetSelectionDto>(selection);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSelectionCommentDto>>> GetCommentByUserId(int id)
        {
            var response = new ServiceResponse<List<GetSelectionCommentDto>>();
            var dbComments = await _context.SelectionComment .Where(c=>c.SelectionId == id).ToListAsync();
            response.Data = dbComments.Select(comments=> _mapper.Map<GetSelectionCommentDto>(comments)).ToList();
            return response;
        }

        public async Task<ServiceResponse<PagedListDto<GetSelectionDto>>> SelectionsPaging(int pageIndex, int pageSize, string? search, string? description)
        {    
            var response = new ServiceResponse<PagedListDto<GetSelectionDto>>();
            IQueryable<Selection> selections = _context.Selections;
            //u slucaju da korisnik zeli fltrirati po imenu
            if(search !=null) selections=selections.Where(s=>s.Name.Contains(search)); 
            if(description!=null)selections=selections.Where(s=>s.Description.Contains(description)); 
            selections = selections.OrderBy(s=>s.Name);
            var pageList=await PaginatedList<Selection>.CreateAsync(selections,pageIndex,pageSize); 
            var output = new PagedListDto<GetSelectionDto>
            {
                 Data = _mapper.Map<List<GetSelectionDto>>(pageList),
                 TotalPages= pageList.TotalPages,
                 PageIndex=pageList.PageIndex,
                 HasPreviousPage= pageList.HasPreviousPage,
                 HasNextPage=pageList.HasNextPage
            };
            response.Data = output;
            return response;
        }

        public async Task<ServiceResponse<List<GetSelectionCommentDto>>> AddSelectionComment(AddSelectionCommentDto newComment)
        {
            var response=new ServiceResponse<List<GetSelectionCommentDto>>();
            var selection = _mapper.Map<SelectionComment>(newComment);
            _context.SelectionComment.Add(selection);
            await _context.SaveChangesAsync();
            response.Data=await _context.SelectionComment.Select(s=>_mapper.Map<GetSelectionCommentDto>(s)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<GetApplicationDto>> AddSelectionApplicant(AddApplicantDto newApplicant)
        {
            var serviceResponse = new ServiceResponse<GetApplicationDto>();
            try
            {
                var selection = await _context.Selections
                .Include(s => s.Applications)
                .FirstOrDefaultAsync(selection => selection.Id == newApplicant.SelectionId);
                if (selection == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Selection is not found";
                    return serviceResponse;
                }
                var applicant = await _context.Applications
                .FirstOrDefaultAsync(applicant => applicant.Id == newApplicant.ApplicationId);
                if (applicant == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Applicant not found";
                    return serviceResponse;
                }
                selection.Applications.Add(applicant);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetApplicationDto>(applicant);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSelectionDto>> DeleteSelectionApplicant(int selectionId, int applicantId)
        {
            var serviceResponse = new ServiceResponse<GetSelectionDto>();
            try
            {
                var selection = await _context.Selections
                .Include(s => s.Applications)
                .FirstOrDefaultAsync(selection => selection.Id == selectionId);
                if (selection == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Selection is not found";
                    return serviceResponse;
                }
                var applicant = await _context.Applications.FirstOrDefaultAsync(applicant => applicant.Id == applicantId);
                if (applicant == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Applicant not found";
                    return serviceResponse;
                }

                selection.Applications.Remove(applicant);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetSelectionDto>(selection);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
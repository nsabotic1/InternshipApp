using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternAppApi.Dtos.ApplicationDtos;
using InternAppApi.Dtos.SelectionComment;

namespace InternAppApi.Dtos.SelectionDtos
{
    public class GetSelectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public string Description { get; set; }=string.Empty;
        public List<GetApplicationDto> Applications { get; set; } 
    }
}
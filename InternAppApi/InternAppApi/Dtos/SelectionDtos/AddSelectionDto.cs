using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAppApi.Dtos.SelectionDtos
{
    public class AddSelectionDto
    {
        public string Name { get; set; } =string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }=string.Empty;
    }
}
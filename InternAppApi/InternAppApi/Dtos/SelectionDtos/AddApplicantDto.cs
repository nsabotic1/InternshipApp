using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAppApi.Dtos.SelectionDtos
{
    public class AddApplicantDto
    {
        public int SelectionId { get; set; }
        public int ApplicationId { get; set; }
    }
}
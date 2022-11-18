using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAppApi.Dtos.SelectionComment
{
    public class AddSelectionCommentDto
    {
        public string? Body { get; set; }=string.Empty;
        public int SelectionId { get; set; }
    }
}
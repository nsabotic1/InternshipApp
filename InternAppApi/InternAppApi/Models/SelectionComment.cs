using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAppApi.Models
{
    public class SelectionComment
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public Selection? Selection { get; set; }
        public int SelectionId { get; set; }
    }
}
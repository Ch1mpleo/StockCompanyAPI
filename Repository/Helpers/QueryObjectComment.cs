using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public class QueryObjectComment
    {
        public string? Symbol {  get; set; } 
        public string? Title { get; set; } = null;
        public string? Content { get; set; } = null;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}

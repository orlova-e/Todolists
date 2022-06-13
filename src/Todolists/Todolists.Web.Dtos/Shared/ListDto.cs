using System.Collections.Generic;
using Todolists.Domain.Core.Conditions;

namespace Todolists.Web.Dtos.Shared
{
    public class ListDto<T>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public SortDir ListSorting { get; set; }
        public IEnumerable<T> Entities { get; set; }
        public string Filters { get; set; }
    }
}
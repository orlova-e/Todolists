using Todolists.Domain.Core.Conditions;

namespace Todolists.Web.Dtos.Shared
{
    public class GetEntitiesDto
    {
        public int Page { get; set; } = 1;
        public int ItemsNumber { get; set; } = 20;
        public SortDir SortDir { get; set; } = SortDir.Desc;
        public string OrderBy { get; set; }
        public string Filters { get; set; }
    }
}
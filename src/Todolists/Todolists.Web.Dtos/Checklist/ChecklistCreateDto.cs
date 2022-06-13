using System.Collections.Generic;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.Dtos.Checklist
{
    public class ChecklistCreateDto : NoteBaseCreateDto
    {
        public IEnumerable<OptionCreateDto> Options { get; set; }
    }
}
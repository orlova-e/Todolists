using System.Collections.Generic;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.Dtos.Checklist
{
    public class ChecklistGetDto : NoteBaseGetDto
    {
        public IEnumerable<OptionGetDto> Options { get; set; }
    }
}
using System.Collections.Generic;
using Todolists.Web.Dtos.Note;

namespace Todolists.Web.Dtos.Checklist
{
    public class ChecklistEditorDto : NoteBaseEditorDto
    {
        public IEnumerable<OptionEditorDto> Options { get; set; }
    }
}
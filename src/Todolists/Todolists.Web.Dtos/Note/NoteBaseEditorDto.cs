using System;
using Todolists.Web.Dtos.Interfaces;

namespace Todolists.Web.Dtos.Note
{
    public class NoteBaseEditorDto : IUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
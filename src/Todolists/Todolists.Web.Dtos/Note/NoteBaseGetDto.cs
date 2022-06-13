using System;

namespace Todolists.Web.Dtos.Note
{
    public class NoteBaseGetDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name { get; set; }
    }
}
using Todolists.Web.Dtos.Interfaces;

namespace Todolists.Web.Dtos.Note
{
    public class NoteBaseCreateDto : ICreateDto
    {
        public string Name { get; set; }
    }
}
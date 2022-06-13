using System;

namespace Todolists.Web.Dtos.Checklist
{
    public class OptionGetDto
    {
        public Guid Id { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }
    }
}
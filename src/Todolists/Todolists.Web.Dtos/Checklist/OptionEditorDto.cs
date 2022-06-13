using System;
using Todolists.Web.Dtos.Interfaces;

namespace Todolists.Web.Dtos.Checklist
{
    public class OptionEditorDto : IUpdateDto
    {
        public virtual Guid Id { get; set; }
        public virtual bool Checked { get; set; }
        public virtual string Text { get; set; }
    }
}
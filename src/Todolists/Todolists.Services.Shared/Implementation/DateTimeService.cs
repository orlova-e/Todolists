using Todolists.Domain.Core.Interfaces;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Services.Shared.Implementation;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
    
    public void Created<T>(T entity) where T : class, IHistorical
        => entity.Created = this.UtcNow;

    public void Updated<T>(T entity) where T : class, IHistorical
        => entity.Updated = this.UtcNow;

    public void Deleted<T>(T entity) where T : class, IHistorical
        => entity.Deleted = this.UtcNow;
}
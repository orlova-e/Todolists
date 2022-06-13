using AutoMapper;
using Todolists.Domain.Core.Interfaces;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Dtos.Interfaces;

namespace Todolists.Web.API.Services.Translation;

public class CollectionResolver<TSource, TDestination, TDtoItem, TModelItem> : IMemberValueResolver<TSource, TDestination, IEnumerable<TDtoItem>, ICollection<TModelItem>>
    where TDtoItem: class, IUpdateDto
    where TModelItem: class, IDomainEntity
{
    private readonly ITranslator _translator;

    public CollectionResolver(ITranslator translator)
    {
        _translator = translator;
    }

    public ICollection<TModelItem> Resolve(TSource source, TDestination destination, 
        IEnumerable<TDtoItem> sourceMember, ICollection<TModelItem> destMember, ResolutionContext context)
    {
        var result = new List<TModelItem>();
        sourceMember = sourceMember.ToArray();

        var newItems = sourceMember
            .Where(x => destMember
                .All(e => e.Id != x.Id));
        
        var deleted = destMember
            .Where(x => sourceMember
                .All(e => e.Id != x.Id));
        
        var updated = destMember
            .Where(x => sourceMember.Any(e => e.Id == x.Id))
            .Join(sourceMember, 
                x => x.Id, 
                x => x.Id, 
                (modelItem, dtoItem) => new
                {
                    Model = modelItem,
                    Dto = dtoItem
                });

        foreach (var newItem in newItems)
        {
            var item = _translator.Translate<TDtoItem, TModelItem>(newItem);
            result.Add(item);
        }

        foreach (var item in deleted)
        {
            item.IsDeleted = true;
            result.Add(item);
        }

        foreach (var item in updated)
        {
            _translator.Transform(item.Dto, item.Model);	
            result.Add(item.Model);				
        }
			
        destMember.Clear();

        foreach (var item in result)
        {
            destMember.Add(item);
        }

        return result;
    }
}
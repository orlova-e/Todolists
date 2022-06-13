using AutoMapper;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Services.Shared.Implementation;

internal class Translator : ITranslator
{
    private readonly IMapper _mapper;

    public Translator(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Translate<TSource, TDestination>(TSource source)
        => _mapper.Map<TSource, TDestination>(source);

    public TDestination Transform<TSource, TDestination>(TSource source, TDestination destination)
        => _mapper.Map(source, destination);
}
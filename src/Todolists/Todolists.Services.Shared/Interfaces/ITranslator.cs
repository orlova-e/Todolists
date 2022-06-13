namespace Todolists.Services.Shared.Interfaces;

public interface ITranslator
{
    TDestination Translate<TSource, TDestination>(TSource source);
    TDestination Transform<TSource, TDestination>(TSource source, TDestination destination);
}
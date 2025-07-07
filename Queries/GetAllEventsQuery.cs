using EventManagement.Models;
using MediatR;

public class GetAllEventsQuery : IRequest<List<string>>, ICacheable
{
    public string CacheKey => "all-events";
    public int CacheDuration => 10;
}
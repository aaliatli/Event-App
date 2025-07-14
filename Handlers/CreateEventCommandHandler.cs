using EventManagement.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Unit>
{
    private readonly IEventRepository _repository;
    private readonly IMemoryCache _cache;

    public CreateEventCommandHandler(IEventRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Location = request.Location,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Capacity = request.Capacity,
            AgeRestriction = request.AgeRestriction > 0 ? request.AgeRestriction : 0
        };
        
        await _repository.AddAsync(newEvent);
        
        _cache.Remove("GetAllEvents");
        System.Console.WriteLine("[CACHE] Yeni event eklendi, cache temizlendi.");

        return Unit.Value;
    }

}
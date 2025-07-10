using EventManagement.Data;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly IEventRepository _repository;
    private readonly IMemoryCache _cache;

    public UpdateEventCommandHandler(IEventRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var existingEvent = await _repository.GetEventById(request.Id);
        if (existingEvent == null)
        {
            throw new Exception("Etkinlik bulunamadı");
        }
        existingEvent.Title = request.Title;
        existingEvent.Location = request.Location;
        existingEvent.StartDate = request.StartDate;
        existingEvent.EndDate = request.EndDate;
        existingEvent.Capacity = request.Capacity;

        await _repository.UpdateAsync(existingEvent);

        _cache.Remove("GetAllEvents");
        System.Console.WriteLine("[CACHE] Event güncellendi, cache temizlendi.");

        return Unit.Value;
    }   
}
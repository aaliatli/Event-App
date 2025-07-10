using MediatR;
using Microsoft.Extensions.Caching.Memory;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IEventRepository _repository;
    private readonly IMemoryCache _cache;
    public DeleteEventCommandHandler(IEventRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var deletedEvent = await _repository.DeleteAsync(request.Id);
        if (!deletedEvent)
        {
            System.Console.WriteLine("[HANDLER] Veri silme hatası.");
            throw new Exception("Veri silinemedi.");
        }
        _cache.Remove("GetAllEvents");
        System.Console.WriteLine("[CACHE] Event silindiği için cache temizlendi.");
        return deletedEvent;
    }
}
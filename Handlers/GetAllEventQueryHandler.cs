using MediatR;

public class GetAllEventQueryHandler : IRequestHandler<GetAllEventsQuery, List<string>>
{
    private readonly IEventRepository _repository;

    public GetAllEventQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _repository.GetAllEventsAsync();
        if (events == null || !events.Any())
        {
            System.Console.WriteLine("[HANDLER] Veri bulunamadı.");
            throw new Exception("Db' den veri çekme hatası");
        }
        return events;
    }
 
}
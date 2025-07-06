using MediatR;

public class GetCurrentEventQueryHandler : IRequestHandler<GetCurrentEventsQuery, List<string>>
{
    private readonly IEventRepository _repository;

    public GetCurrentEventQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public 
    async Task<List<string>> Handle(GetCurrentEventsQuery request, CancellationToken cancellationToken) {
        return await _repository.GetCurrentEventsAsync();
    }
}
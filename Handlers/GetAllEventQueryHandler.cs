using MediatR;

public class GetAllEventQueryHandler : IRequestHandler<GetAllEventsQuery, List<string>>
{
    private readonly IEventRepository _repository;

    public GetAllEventQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken) {
        return await _repository.GetAllEventsAsync();
    }
 
}
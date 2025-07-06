using EventManagement.Models;
using MediatR;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event>
{
    private readonly IEventRepository _repository;

    public GetEventByIdQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetEventById(request.Id);
    }
}
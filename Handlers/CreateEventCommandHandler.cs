using EventManagement.Models;
using MediatR;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Unit>
{
    public readonly IEventRepository _repository;

    public CreateEventCommandHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var newEvent = new Event
        {
            id = Guid.NewGuid(),
            Title = request.Title,
            Location = request.Location,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Capacity = request.Capacity

        };
        await _repository.AddAsync(newEvent);
        return Unit.Value;
    }

}
using EventManagement.Data;
using MediatR;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly IEventRepository _repository;

    public UpdateEventCommandHandler(IEventRepository repository)
    {
        _repository = repository;
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
        return Unit.Value;
    }   
}
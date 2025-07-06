using MediatR;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IEventRepository _repository;
    public DeleteEventCommandHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
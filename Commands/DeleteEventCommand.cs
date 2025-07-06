using EventManagement.Models;
using MediatR;

public class DeleteEventCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
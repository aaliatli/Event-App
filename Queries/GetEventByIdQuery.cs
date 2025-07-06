using EventManagement.Models;
using MediatR;

public class GetEventByIdQuery : IRequest<Event>
{
    public Guid Id{ get; set; }
}
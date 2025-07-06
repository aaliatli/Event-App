using EventManagement.Models;
using MediatR;

public class GetCurrentEventsQuery : IRequest<List<string>>
{
}
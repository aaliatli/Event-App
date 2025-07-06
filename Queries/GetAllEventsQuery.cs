using EventManagement.Models;
using MediatR;

public class GetAllEventsQuery : IRequest<List<string>>
{
}
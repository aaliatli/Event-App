using EventManagement.Models;
using MediatR;

public class GetUserByIdQuery : IRequest<User>{
    public Guid Id {get ; set;}
}
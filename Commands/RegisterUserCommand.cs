using MediatR;

public class RegisterUserCommand : IRequest<Unit>{
    public string Name {get; set;}
    public string LastName {get; set;}
    public int Age {get; set;}
    public string Mail {get; set;}
    public string Password {get; set;}
}
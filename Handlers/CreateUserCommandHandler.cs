using EventManagement.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>{
    private readonly IUserRepository _repository;
    private readonly IMemoryCache _cache;

    public CreateUserCommandHandler(IUserRepository repository, IMemoryCache cache){
        _repository = repository;
        _cache = cache;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken){
        var newUser = new User{
            Id = Guid.NewGuid(),
            Name = request.Name,
            LastName = request.LastName,
            Age = request.Age,
            Mail = request.Mail,
            Password = request.Password
        };
        await _repository.RegisterUser(newUser);
        _cache.Remove("GetAllEvents");
        System.Console.WriteLine("[CACHE] Yeni kullanıcı kayıt edildi.");
        return Unit.Value;
    }
}
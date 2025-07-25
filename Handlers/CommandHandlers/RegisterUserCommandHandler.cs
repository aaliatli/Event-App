using EventManagement.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>{
    private readonly IUserRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly JwtService _jwtService;

    public RegisterUserCommandHandler(IUserRepository repository, IMemoryCache cache, JwtService jwtService)
    {
        _repository = repository;
        _cache = cache;
        _jwtService = jwtService;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken){
        var existingUser = await _repository.GetUserByMailAsync(request.Mail);
        if (existingUser != null)
        {
            Log.Warning("[LOG] Başarısız kayıt. Kullanıcı mevcut.");
            return Unit.Value;
        }
        var newUser = new User{
            Id = Guid.NewGuid(),
            Name = request.Name,
            LastName = request.LastName,
            Age = request.Age,
            Mail = request.Mail,
            Password = request.Password
        };
        await _repository.RegisterUser(newUser);
        var token = _jwtService.GenerateToken(newUser.Id.ToString(), "User");
        _cache.Remove("GetAllEvents");
        System.Console.WriteLine("[CACHE] Yeni kullanıcı kayıt edildi.");
        return Unit.Value;
    }
}
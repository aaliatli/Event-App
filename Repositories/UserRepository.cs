
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    public readonly EventDbContext _context;

    public UserRepository(EventDbContext context){
        _context = context;
    }
    public async Task<User> GetUserByMailAsync(string mail)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Mail == mail);
    }

    public async Task<User> LoginUser(string mail, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Mail == mail);
        if (user != null)
        {
            return user;
        }
        return null;
    }

    public async Task<User> RegisterUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
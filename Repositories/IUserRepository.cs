public interface IUserRepository
{
    Task<User> LoginUser(string mail, string password);
    Task<User> RegisterUser(User user);
    Task<User> GetUserByMailAsync(string mail);

}
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public ICollection<UserEvent> UserEvents { get; set; }

}
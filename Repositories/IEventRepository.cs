using EventManagement.Models;

public interface IEventRepository
{
    Task AddAsync(Event entity);
    Task UpdateAsync(Event entity);
    Task<List<string>> GetAllEventsAsync();
    Task<List<string>> GetCurrentEventsAsync();
    Task<Event> GetEventById(Guid Id);
    Task<bool> DeleteAsync(Guid id);
    Task<List<Event>> GetSearchedEvents();
}
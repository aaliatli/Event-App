using System;

namespace EventManagement.Models;

public class Event
{
    public Guid id { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
}

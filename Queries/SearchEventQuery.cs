using MediatR;
using EventManagement.Models;

public class SearchEventQuery : IRequest<List<Event>>
{
    public string Keyword{get; set;}

    public SearchEventQuery(string keyword){
        Keyword = keyword;
    }
}
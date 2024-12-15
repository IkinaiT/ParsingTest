using ParsingTest.Models;

namespace ParsingTest.Services.Interfaces
{
    public interface IDataBaseService
    {
        public Result LastResult { get; set; }
        public List<long> GetEventsIds();
        public Event? GetEvent(long id);
        public Task SetData(List<Sport> sports, List<Event> events, List<Factor> factors);
    }
}

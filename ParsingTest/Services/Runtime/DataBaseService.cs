using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParsingTest.Models;
using ParsingTest.Services.Interfaces;

namespace ParsingTest.Services.Runtime
{
    public class DataBaseService : IDataBaseService
    {
        private readonly DataBaseContext _context;

        private SemaphoreSlim _semaphore = new(1, 1);

        public Result LastResult { get; set; }

        public DataBaseService()
        {
            var optionsPostgresSQL = new DbContextOptionsBuilder<DataBaseContext>();

            optionsPostgresSQL.UseNpgsql("Host=localhost;Username=postgres;Password=admin;Database=parsedData;Pooling=True");

            _context = new(optionsPostgresSQL.Options);
        }

        public Task SetEvents(List<Event> events)
        {
            _semaphore.Wait();

            foreach(var e in events)
            {
                var element = _context.Events.FirstOrDefault(_ => _.Id == e.Id);

                if(element != null)
                {
                    element = e;
                }
                else
                {
                    _context.Events.Add(e);
                }

                _context.SaveChanges();
            }

            _semaphore.Release();

            return Task.CompletedTask;
        }

        public Task SetSports(List<Sport> sports)
        {
            _semaphore.Wait();

            foreach (var s in sports)
            {
                var element = _context.Sports.FirstOrDefault(_ => _.Id == s.Id);

                if (element != null)
                {
                    element = s;
                }
                else
                {
                    _context.Sports.Add(s);
                }

                _context.SaveChanges();
            }

            _semaphore.Release();

            return Task.CompletedTask;
        }

        public async Task SetData(List<Sport> sports, List<Event> events, List<Factor> factors)
        {
            _semaphore.Wait();

            foreach (var e in events)
            {
                var element = _context.Events.FirstOrDefault(_ => _.Id == e.Id);

                if (element != null)
                {
                    element = e;
                }
                else
                {
                    _context.Events.Add(e);
                }
            }

            foreach (var s in sports)
            {
                var element = _context.Sports.FirstOrDefault(_ => _.Id == s.Id);

                if (element != null)
                {
                    element = s;
                }
                else
                {
                    _context.Sports.Add(s);
                }
            }

            await _context.SaveChangesAsync();

            _semaphore.Release();

        }

        public List<long> GetEventsIds()
        {
            _semaphore.Wait();

            var result = _context.Events.Select(_ => _.Id).ToList();

            _semaphore.Release();

            return result;
        }

        public Event? GetEvent(long id)
        {
            return _context.Events.FirstOrDefault(_ => _.Id == id);
        }
    }
}

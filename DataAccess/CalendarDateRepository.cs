using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using pgce_organiser_api.Models;

namespace pgce_organiser_api.Controllers.DataAccess
{
    public class CalendarDatesRepository : ICalendarDatesRepository
    {
        private readonly ICollection<CalendarDate> _calendarDates;
        private const string JsonFile = "DataAccess/calendarDates.json";

        public CalendarDatesRepository()
        {
            using (var sr = new StreamReader(JsonFile))
            {
                _calendarDates = JsonConvert.DeserializeObject<List<CalendarDate>>(sr.ReadToEnd());
            }
        }

        public IEnumerable<CalendarDate> Get()
        {
            return _calendarDates;
        }

        public IEnumerable<CalendarDate> GetByDate(DateTime eventDate)
        {
            return _calendarDates.Where(d => d.Date.Date >= DateTime.Now.Date && d.Date.Date <= eventDate);
        }

        public void Save(Event calendarEvent)
        {
            var blah = _calendarDates.FirstOrDefault(c => c.Events.Any(e => e.Id == calendarEvent.Id));
            blah?.Events.Remove(blah.Events.First(e => e.Id == calendarEvent.Id));

            if (_calendarDates.Any(c => c.Date.Date == calendarEvent.DateTime.Date))
            {
                _calendarDates.First(c => c.Date.Date == calendarEvent.DateTime.Date).Events.Add(calendarEvent);
            }
            else
            {
                _calendarDates.Add(new CalendarDate
                {
                    Date = calendarEvent.DateTime.Date,
                    Events = new List<Event>
                    {
                        calendarEvent
                    }
                });
            }

            var json = JsonConvert.SerializeObject(_calendarDates);
            File.WriteAllText(JsonFile, json);
        }
    }

    public interface ICalendarDatesRepository : IGetCalendarDates, ISaveCalendarDates
    {

    }

    public interface IGetCalendarDates
    {
        IEnumerable<CalendarDate> Get();
        IEnumerable<CalendarDate> GetByDate(DateTime date);
    }

    public interface ISaveCalendarDates
    {
        void Save(Event calendarEvent);
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using pgce_organiser_api.Models;

namespace pgce_organiser_api.Controllers.DataAccess
{
public class CalendarDatesRepository
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

    public void Save(Event calendarEvent)
    {
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
}

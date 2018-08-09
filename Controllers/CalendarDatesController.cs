using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pgce_organiser_api.Controllers.DataAccess;
using pgce_organiser_api.Models;

namespace pgce_organiser_api.Controllers
{
    [Route("api/[controller]")]
    public class CalendarDatesController : Controller
    {
        private readonly ICalendarDatesRepository _calendarRepository = new CalendarDatesRepository();

        [HttpGet("")]
        public IEnumerable<CalendarDate> Get()
        {
            return _calendarRepository.Get();
        }

        [HttpGet("tomorrow")]
        public IEnumerable<CalendarDate> GetTomorrowsEvents()
        {
            return _calendarRepository.GetByDate(DateTime.Now.AddDays(1).Date);
        }

        [HttpGet("nextWeek")]
        public IEnumerable<CalendarDate> GetNextWeekEvents()
        {
            return _calendarRepository.GetByDate(DateTime.Now.AddDays(7).Date);
        }

        [HttpGet("upcoming")]
        public IEnumerable<CalendarDate> GetUpcomingEvents()
        {
            return _calendarRepository.GetByDate(DateTime.Now.AddMonths(1).Date);
        }

        [HttpPost]
        public void Save([FromBody] Event calendarEvent)
        {
            _calendarRepository.Save(calendarEvent);
        }
    }
    
}

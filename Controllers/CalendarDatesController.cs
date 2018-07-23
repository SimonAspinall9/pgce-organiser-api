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
        // GET api/values
        [HttpGet]
        public IEnumerable<CalendarDate> Get()
        {
            return new CalendarDatesRepository().Get();
        }

        [HttpPost]
        public void Save([FromBody] Event calendarEvent)
        {
            new CalendarDatesRepository().Save(calendarEvent);
        }
    }
    
}

using System;
using System.Collections.Generic;

namespace pgce_organiser_api.Models
{

    public class CalendarDate
    {
        public DateTime Date { get; set; }
        public ICollection<Event> Events { get; set; }
    }

}
using System;

namespace pgce_organiser_api.Models
{

    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime DateTime { get; set; }
        public string EventType { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    }

}
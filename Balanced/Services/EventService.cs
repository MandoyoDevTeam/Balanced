using Balanced.Entities;

namespace Balanced.Services
{
    public class EventService : BalancedServices<Event, EventList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/events");
            }
        }

        public new EventList Get(Event events)
        {
            return base.Get(events);
        }

        public new EventList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }
    }
}

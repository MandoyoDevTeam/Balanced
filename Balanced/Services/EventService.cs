using Balanced.Entities;

namespace Balanced.Services
{
    public class EventService : BalancedServices<Event>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/{0}/events", BalancedHttpRest.Version);
            }
        }

        public EventService(string secret) : base(secret)
        {
        }

        public new Event Get(Event events)
        {
            return base.Get(events);
        }

        public new PagedList<Event> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }
    }
}

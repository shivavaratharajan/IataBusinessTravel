using EventbriteNET.Entities;
using EventbriteNET.Xml;
using System.Collections.Generic;

namespace EventbriteNET.HttpApi
{
    class EventRequest : RequestBase
    {
        const string PATH = "event_get";

        public EventRequest(long id, EventbriteContext context)
            : base(PATH, context)
        {
            this.AddGet("id", id.ToString());
        }

        public EventRequest(Dictionary<string, string> queryParameters, EventbriteContext context)
           : base(PATH, context)
        {
          //  this.AddGet("id", id.ToString());
        }

        //
        public Event GetResponse()
        {
            return new EventBuilder(this.Context).Build(base.GetResponse());
        }
    }
}

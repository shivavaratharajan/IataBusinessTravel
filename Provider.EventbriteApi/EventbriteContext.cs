using System;
using System.Collections.Generic;
using EventbriteNET.Entities;
using EventbriteNET.HttpApi;

namespace EventbriteNET
{

    public class EventbriteContext
    {
        public string AppKey;
        public string UserKey;
        public string OAuthToken;
        public string Host = "https://www.eventbriteapi.com/v3/";

        public EventbriteContext(string oAuthToken = "DPHAVUXI5THKHTZ2ASEW", string appKey = "LXQKOF45STVYI75GW334", string userKey = null)
        {
            this.OAuthToken = oAuthToken;
            this.AppKey = appKey;
            if (userKey != null)
            {
                this.UserKey = userKey;
            }
        }

        public Organizer GetOrganizer(long id)
        {
            return new Organizer(id, this);
        }

        public Event GetEvent(long id)
        {
            return new EventRequest(id, this).GetResponse();
        }

        public Event SearchEvents(Dictionary<string,string> queryParameters)
        {
            return new EventRequest(queryParameters, this).GetResponse();
        }
    }
}

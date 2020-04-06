using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Machine.Specifications;
using Dolittle.Logging;

namespace RaaLabs.TimeSeries.NMEA.for_State.given
{
    class a_state : a_logger
    {
        protected static State state;

        Establish context = () => state = new State(new PrioritizedTags(new Dictionary<string, SourcePriority>()
        {
            { "Longitude", new SourcePriority { Priorities = new List<string> { "GPGLL", "GPRMC" }, Threshold = 10000 } },
            { "Latitude", new SourcePriority { Priorities = new List<string> { "GPGLL", "GPRMC" }, Threshold = 10000 } }
        }), logger);
    }
}

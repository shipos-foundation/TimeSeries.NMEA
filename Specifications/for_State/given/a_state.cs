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

        Establish context = () => state = new State(new PrioritizedTags(new Dictionary<string, List<SourcePriority>>()
        {
            { "Longitude", new List<SourcePriority>() { new SourcePriority("GPGLL", 10000), new SourcePriority("GPRMC", 10000) } },
            { "Latitude", new List<SourcePriority>() { new SourcePriority("GPGLL", 10000), new SourcePriority("GPRMC", 10000) } }
        }), logger);
    }
}

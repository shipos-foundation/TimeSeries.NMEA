using System;
using System.Collections.Generic;
using System.Text;

using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_State.when_reading_state
{
    class for_measurement_from_talker_without_configured_priority : given.a_state
    {
        static Dictionary<string, TagWithData> measurements = new Dictionary<string, TagWithData>();

        Establish context = () => state.StateChanged += (TagWithData newData, Timestamp timestamp) => measurements[newData.Tag] = newData;

        Because of = () =>
        {
            Timestamp now = Timestamp.UtcNow;
            Timestamp twenty_minutes_ago = now - (1000 * 60 * 20);

            state.DataReceived("GPRAA", now, new TagWithData("Longitude", 3.2));
            state.DataReceived("GPGLL", now, new TagWithData("Longitude", 4.5));
            state.DataReceived("GPRAA", now, new TagWithData("Longitude", 3.8));

            state.DataReceived("GPRAA", twenty_minutes_ago, new TagWithData("Latitude", 3.2));
            state.DataReceived("GPGLL", twenty_minutes_ago, new TagWithData("Latitude", 4.5));
            state.DataReceived("GPRAA", now, new TagWithData("Latitude", 3.8));
        };

        It should_select_the_measurement_with_configured_priority_when_data_is_not_stale = () => measurements["Longitude"].Data.ShouldEqual(4.5);
        It should_select_the_measurement_without_configured_priority_other_data_is_stale = () => measurements["Latitude"].Data.ShouldEqual(3.8);


    }
}

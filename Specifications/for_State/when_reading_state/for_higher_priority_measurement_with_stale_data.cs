using System;
using System.Collections.Generic;
using System.Text;

using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_State.when_reading_state
{
    class for_higher_priority_measurement_with_stale_data : given.a_state
    {
        static TagWithData latitude;

        Establish context = () => state.StateChanged += (TagWithData newData, Timestamp timestamp) => latitude = newData;

        Because of = () =>
        {
            Timestamp twenty_minutes_ago = Timestamp.UtcNow - (1000 * 60 * 20);
            Timestamp now = Timestamp.UtcNow;

            state.DataReceived("GPRMC", twenty_minutes_ago, new TagWithData("Latitude", 4.2));
            state.DataReceived("GPGLL", twenty_minutes_ago, new TagWithData("Latitude", 6.5));
            state.DataReceived("GPRMC", now, new TagWithData("Latitude", 4.8));
        };

        It should_select_the_non_stale_data_with_lower_priority = () => latitude.Data.ShouldEqual(4.8);


    }
}

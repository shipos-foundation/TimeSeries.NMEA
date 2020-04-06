using System;
using System.Collections.Generic;
using System.Text;

using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_State.when_reading_state
{
    class for_overlapping_recent_measurements : given.a_state
    {
        static TagWithData longitude;

        Establish context = () => state.StateChanged += (TagWithData newData, Timestamp timestamp) => longitude = newData;

        Because of = () =>
        {
            Timestamp now = Timestamp.UtcNow;

            state.DataReceived("GPRMC", now, new TagWithData("Longitude", 3.2));
            state.DataReceived("GPGLL", now, new TagWithData("Longitude", 4.5));
            state.DataReceived("GPRMC", now, new TagWithData("Longitude", 3.8));
        };

        It should_select_the_highest_priority_measurement = () => longitude.Data.ShouldEqual(4.5);


    }
}

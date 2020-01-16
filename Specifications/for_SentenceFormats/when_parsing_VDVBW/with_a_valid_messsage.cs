/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Machine.Specifications;
using RaaLabs.TimeSeries.NMEA.SentenceFormats;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats.when_parsing_VDVBW
{
    public class with_a_valid_messsage : given.a_VDVBW_parser
    {
        static string[] values = new[] { "001.00", "000.0", "A", "036.00", "000.00", "V" };
        static ParsedResult[] results;
        Because of = () => results = parser.Parse(values).ToArray();
        It should_return_four_result = () => results.Length.ShouldEqual(4);
        It should_return_a_longitudinal_speed_through_water = () => results.ShouldEmit("LongitudinalSpeedThroughWater", 0.5144445f);
        It should_return_a_transverse_speed_through_water = () => results.ShouldEmit("TransverseSpeedThroughWater", 0.00f);
        It should_return_a_longitudinal_speed_over_ground = () => results.ShouldEmit("LongitudinalSpeedOverGround", 18.52f);
        It should_return_a_transverse_speed_over_ground = () => results.ShouldEmit("TransverseSpeedOverGround", 0.00f);
    }
}
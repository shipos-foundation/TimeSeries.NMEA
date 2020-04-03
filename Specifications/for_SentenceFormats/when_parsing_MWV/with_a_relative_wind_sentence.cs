/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Machine.Specifications;
using RaaLabs.TimeSeries.NMEA.SentenceFormats;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats.when_parsing_WIMWV
{
    public class with_a_relative_wind_sentence : given.a_MWV_parser
    {
        static string[] values = new[] { "325", "R", "018.3", "M" };
        static TagWithData[] results;
        Because of = () => results = parser.Parse(values).ToArray();
        It should_return_two_result = () => results.Length.ShouldEqual(2);
        It should_return_a_relative_wind_angle = () => results.ShouldEmit("WindAngleRelative", 325f);
        It should_return_a_relative_wind_speed = () => results.ShouldEmit("WindSpeedRelative", 18.3f);
    }
}
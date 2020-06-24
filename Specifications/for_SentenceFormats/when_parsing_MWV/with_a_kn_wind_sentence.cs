/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Machine.Specifications;
using RaaLabs.TimeSeries.NMEA.SentenceFormats;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats.when_parsing_MWV
{
    public class with_a_kn_wind_sentence : given.a_MWV_parser
    {
        static string[] values = new[] { "325", "T", "018.0", "N" };
        static TagWithData[] results;
        Because of = () => results = parser.Parse(values).ToArray();
        It should_return_two_result = () => results.Length.ShouldEqual(2);
        It should_return_a_true_wind_speed = () => results.ShouldEmit("WindSpeedTrue", 9.26f);
    }
}
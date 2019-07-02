/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Machine.Specifications;
using Dolittle.TimeSeries.NMEA.SentenceFormats;
using System.Linq;

namespace Dolittle.TimeSeries.NMEA.for_SentenceFormats.when_parsing_HEHDT
{
    public class with_a_valid_message : given.a_HEHDT_parser
    {
        static string[] values = new[] { "095345","A","0557.659","N","10732.647","E","16.8","222.","280619","02.","W" };


        Because of = () => results = parser.Parse(values).ToList();

        //It should_returned_a_speed_of_0 = () => result.ShouldBeEmpty();

        //It should_not_be_able_to_parse = () => result.ShouldBeFalse();
    }
}
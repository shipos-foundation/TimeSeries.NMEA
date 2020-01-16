/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_getting_identifier
{
    public class and_sentence_has_valid_sentence : given.no_formats
    {
        static string result;
        Because of = () => result = sentence_parser.GetIdentifierFor("$GPRMC,095345,A,0557.659,N,10732.647,E,16.8,222.,280619,02.,W*6E");

        It should_return_identifier = () => result.ShouldEqual("GPRMC");
    }
}
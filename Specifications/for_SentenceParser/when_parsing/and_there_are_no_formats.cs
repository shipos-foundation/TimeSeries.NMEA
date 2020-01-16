/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_parsing
{
    public class and_there_are_no_formats : given.no_formats
    {
        static Exception result;
        Because of = () => result = Catch.Exception(() => sentence_parser.Parse("$GPRMC,095345,A,0557.659,N,10732.647,E,16.8,222.,280619,02.,W*6E"));

        It should_throw_unsupported_sentence = () => result.ShouldBeOfExactType<UnsupportedSentence>();
    }
}
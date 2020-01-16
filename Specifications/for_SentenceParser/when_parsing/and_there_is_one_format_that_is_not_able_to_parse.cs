/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_parsing
{
    public class and_there_is_one_format_that_is_not_able_to_parse : given.one_format
    {
        static Exception result;

        Because of = () => result = Catch.Exception(() => sentence_parser.Parse(unsupported_format));

        It should_throw_unsupported_sentence = () => result.ShouldBeOfExactType<UnsupportedSentence>();
    }
}
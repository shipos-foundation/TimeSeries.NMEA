/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_checking_if_can_parse
{
    public class and_there_are_two_formats_with_one_format_that_is_able_to_parse : given.two_formats
    {
        static bool result;

        Because of = () => result = sentence_parser.CanParse(second_format_sample);

        It should_be_able_to_parse = () => result.ShouldBeTrue();
    }
}
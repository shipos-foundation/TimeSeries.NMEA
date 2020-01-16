/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_checking_if_can_parse
{
    public class and_there_is_one_format_that_is_able_to_parse : given.one_format
    {
        static bool result;

        Because of = () => result = sentence_parser.CanParse(valid_format);

        It should_be_able_to_parse = () => result.ShouldBeTrue();
    }
}
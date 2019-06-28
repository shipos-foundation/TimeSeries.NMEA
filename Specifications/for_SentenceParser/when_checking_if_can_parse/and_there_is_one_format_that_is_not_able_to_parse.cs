/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.NMEA.for_SentenceParser.when_checking_if_can_parse
{
    public class and_there_is_one_format_that_is_not_able_to_parse : given.one_format
    {
        static bool result;

        Because of = () => result = sentence_parser.CanParse(unsupported_format);

        It should_not_be_able_to_parse = () => result.ShouldBeFalse();
    }
}
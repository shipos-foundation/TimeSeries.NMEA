/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_checking_if_can_parse
{
    public class and_sentence_is_shorter_than_expected_for_identification : given.no_formats
    {
        static bool result;
        Because of = () => result = sentence_parser.CanParse("$GPR");

        It should_not_be_able_to_parse = () => result.ShouldBeFalse();
    }
}
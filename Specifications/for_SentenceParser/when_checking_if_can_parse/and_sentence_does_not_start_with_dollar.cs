/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_checking_if_can_parse
{
    public class and_sentence_does_not_start_with_dollar : given.no_formats
    {
        static bool result;
        Because of = () => result = sentence_parser.CanParse("GPRMC,151437,A,2146.826,N,11858.672,E,18.2,209.,250619,03.,W*60");

        It should_not_be_able_to_parse = () => result.ShouldBeFalse();
    }
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Collections.Generic;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.when_parsing
{
    public class and_there_is_one_format_that_is_able_to_parse : given.one_format
    {
        const string sub_type = "Sub type";
        const string exepected_result = "Fourty Two";
        static IEnumerable<ParsedResult> result;

        Establish context = () => format.Setup(_ => _.Parse(values)).Returns(new[] { new ParsedResult(sub_type,exepected_result) });

        Because of = () => 
        {
            result = sentence_parser.Parse(valid_format);
        };

        It should_return_the_expected_sub_type = () => result.First().Type.ShouldEqual(sub_type);
        It should_return_the_expected_result = () => result.First().Result.ShouldEqual(exepected_result);
    }
}
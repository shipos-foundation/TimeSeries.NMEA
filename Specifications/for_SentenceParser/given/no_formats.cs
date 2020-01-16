/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.given
{
    public class no_formats : all_dependencies
    {
        protected static SentenceParser sentence_parser;
        Establish context = () => sentence_parser = new SentenceParser(sentence_formats.Object);
    }
}
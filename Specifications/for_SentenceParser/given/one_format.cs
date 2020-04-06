/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.given
{
    public class one_format : all_dependencies
    {
        protected static string[] values = new[] { "095345","A","0557.659","N","10732.647","E","16.8","222.","280619","02.","W" };
        protected static string valid_format = string.Concat("$GPRMC,", string.Join(',', values),"*6E");
        protected const string unsupported_format = "$ABABC,151437,A,2146.826,N,11858.672,E,18.2,209.,250619,03.,W*60";
        protected static SentenceParser sentence_parser;
        protected static Mock<ISentenceFormat> format;

        Establish context = () => 
        {
            format = new Mock<ISentenceFormat>();
            format.SetupGet(_ => _.Identitifer).Returns("RMC");
            actual_formats.Add(format.Object);
            sentence_parser = new SentenceParser(sentence_formats.Object);
        };
    }    
}
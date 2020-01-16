/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.given
{
    public class two_formats : all_dependencies
    {
        protected static string[] first_format_values = new[] { "095345","A","0557.659","N","10732.647","E","16.8","222.","280619","02.","W"};
        protected static string first_format_sample = string.Concat("$GPRMC,", string.Join(',', first_format_values),"*6E");
        
        protected static string[] second_format_values = new[] { "095345","0557.659","N","10732.647","E","2","08","01.0","+0026","M","+011","M","01","0777" };
        protected static string second_format_sample = string.Concat("$GPGGA,", string.Join(',', second_format_values),"*7B");
        protected static SentenceParser sentence_parser;
        protected static Mock<ISentenceFormat> first_format;
        protected static Mock<ISentenceFormat> second_format;

        Establish context = () => 
        {
            first_format = new Mock<ISentenceFormat>();
            first_format.SetupGet(_ => _.Talker).Returns("GP");
            first_format.SetupGet(_ => _.Identitifer).Returns("RMC");
            actual_formats.Add(first_format.Object);

            second_format = new Mock<ISentenceFormat>();
            second_format.SetupGet(_ => _.Talker).Returns("GP");
            second_format.SetupGet(_ => _.Identitifer).Returns("GGA");
            actual_formats.Add(second_format.Object);

            sentence_parser = new SentenceParser(sentence_formats.Object);
        };
    }    
}
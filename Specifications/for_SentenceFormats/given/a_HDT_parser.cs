/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using RaaLabs.TimeSeries.NMEA.SentenceFormats;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats.given
{
    public class a_HDT_parser
    {
        protected static HDT parser;
        Establish context = () => parser = new HDT();
    }
}
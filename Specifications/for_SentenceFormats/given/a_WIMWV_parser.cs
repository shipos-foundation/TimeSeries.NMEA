/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using RaaLabs.TimeSeries.NMEA.SentenceFormats;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats.given
{
    public class a_WIMWV_parser
    {
        protected static WIMWV parser;
        Establish context = () => parser = new WIMWV();
    }
}
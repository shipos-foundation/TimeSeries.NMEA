/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using RaaLabs.TimeSeries.DataTypes;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Track made good and Ground speed"
    /// </summary>
    public class VTG : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "VTG";

        /// <inheritdoc/>
        public IEnumerable<ParsedResult> Parse(string[] values)
        {
            return new[] {
                new ParsedResult("CourseOverGroundTrue", new Measurement<float>
                {
                    Value = float.Parse(values[0])
                }),
                new ParsedResult("CourseOverGroundMagnetic", new Measurement<float>
                {
                    Value = float.Parse(values[2])
                }),
                new ParsedResult("SpeedOverGround", new Measurement<float>
                {
                    Value = (float.Parse(values[4])*1852)/3600
                })
            };
        }



    }
}
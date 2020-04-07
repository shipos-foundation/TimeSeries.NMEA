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
    /// VHW - Water speed and heading
    /// </summary>
    public class VHW : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "VHW";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            return new[] {
                new TagWithData("HeadingTrue", float.Parse(values[0])),
                new TagWithData("HeadingMagnetic", float.Parse(values[2])),
                new TagWithData("SpeedThroughWater", (float.Parse(values[4])*1852)/3600)
            };
        }



    }
}
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
            var headingTrue = values[0];
            var headingMagnetic = values[2];
            var speedThroughWater = values[4];

            if(ValidSentence(headingTrue)) yield return  new TagWithData("HeadingTrue", float.Parse(headingTrue));
            if(ValidSentence(headingMagnetic)) yield return  new TagWithData("HeadingMagnetic", float.Parse(headingMagnetic));
            if(ValidSentence(speedThroughWater)) yield return  new TagWithData("SpeedThroughWater", (float.Parse(speedThroughWater)*1852)/3600);
        }

        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

    }
}
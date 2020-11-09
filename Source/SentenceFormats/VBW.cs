/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using RaaLabs.TimeSeries.DataTypes;
using System;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Dual Ground/Water Speed (should probably implement only send if status is valid, see documentation)"
    /// </summary>
    public class VBW : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "VBW";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var longitudinalSpeedThroughWater = values[0];
            var transverseSpeedThroughWater = values[1];
            var longitudinalSpeedOverGround = values[3];
            var transverseSpeedOverGround = values[4];

            if (ValidSentence(longitudinalSpeedThroughWater)) yield return new TagWithData("LongitudinalSpeedThroughWater", (float.Parse(longitudinalSpeedThroughWater) * 1852) / 3600);
            if (ValidSentence(transverseSpeedThroughWater)) yield return new TagWithData("TransverseSpeedThroughWater", (float.Parse(transverseSpeedThroughWater) * 1852) / 3600);
            if (ValidSentence(longitudinalSpeedOverGround)) yield return new TagWithData("LongitudinalSpeedOverGround", (float.Parse(longitudinalSpeedOverGround) * 1852) / 3600);
            if (ValidSentence(transverseSpeedOverGround)) yield return new TagWithData("TransverseSpeedOverGround", (float.Parse(transverseSpeedOverGround) * 1852) / 3600);
            if ((ValidSentence(longitudinalSpeedThroughWater)) && (ValidSentence(transverseSpeedThroughWater)))
            {
                var speedThroughWater = Math.Sqrt(Math.Pow(float.Parse(longitudinalSpeedThroughWater), 2) + Math.Pow(float.Parse(transverseSpeedThroughWater), 2));
                yield return new TagWithData("SpeedThroughWater", (speedThroughWater * 1852) / 3600);
            }
            else if ((ValidSentence(longitudinalSpeedThroughWater) == true) && (ValidSentence(transverseSpeedThroughWater)) == false)
            {
                yield return new TagWithData("SpeedThroughWater", (float.Parse(longitudinalSpeedThroughWater) * 1852) / 3600);
            }
        }
        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}

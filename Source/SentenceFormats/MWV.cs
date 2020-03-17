/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using RaaLabs.TimeSeries.DataTypes;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Wind Speed and Angle"
    /// </summary>
    public class MWV : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "MWV";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var windAngle = float.Parse(values[0]);
            var windSpeed = float.Parse(values[2]);
            var windAngleName = "WindAngleTrue";
            var windUnit = "WindSpeedTrue";
            if (values[1] == "R")
            {
                windAngleName = "WindAngleRelative";
                windUnit = "WindSpeedRelative";
            }
            if (values[3] == "K") windSpeed = (windSpeed * 1852) / 3600;
            if (values[3] == "N") windUnit = "WindForce";

            return new[] {
                new TagWithData(windAngleName, windAngle),
                new TagWithData(windUnit, windSpeed)
            };
        }
    }
}
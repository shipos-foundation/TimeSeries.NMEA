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
    /// Represents the format of "Recommended Minimum Navigation Information"
    /// </summary>
    public class GLL : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "GLL";

        /// <inheritdoc/>
        public IEnumerable<ParsedResult> Parse(string[] values)
        {
            var latitude = ConvertToDegree(values[0]);
            var longitude = ConvertToDegree(values[2]);
            if (values[1] == "S") latitude = -latitude;
            if (values[3] == "W") longitude = -longitude;

            return new[] {
                new ParsedResult("Position", new Coordinate
                {
                    Latitude = new Measurement<float>
                    {
                        Value = latitude
                    },
                    Longitude = new Measurement<float>
                    {
                        Value = longitude
                    }
                }),
            };
        }

        private float ConvertToDegree(string value)
        {
            var length = value.Split(".")[0].Length;
            var _degree = value.Substring(0, length - 2);
            var _decimal = value.Substring(length - 2);
            var result = float.Parse(_degree) + float.Parse(_decimal) / 60;

            return result;
        }

    }
}
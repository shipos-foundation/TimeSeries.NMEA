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
    /// Represents the format of "GGA - Global Positioning System Fix Data"
    /// </summary>
    public class GGA : ISentenceFormat
    {


        /// <inheritdoc/>
        public string Identitifer => "GGA";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = ConvertToDegree(values[1]);
            var longitude = ConvertToDegree(values[3]);
            if (values[2] == "S") latitude = -latitude;
            if (values[4] == "W") longitude = -longitude;

            return new[] {
                new TagWithData("Position", new Coordinate
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
                new TagWithData("Latitude", latitude),
                new TagWithData("Longitude", longitude),
                new TagWithData("GPSsatelites", float.Parse(values[6])),
                new TagWithData("HDOP", float.Parse(values[7]))

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
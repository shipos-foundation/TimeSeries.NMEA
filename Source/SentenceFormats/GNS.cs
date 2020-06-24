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
    /// GNS - Fix data
    /// </summary>
    public class GNS : ISentenceFormat
    {


        /// <inheritdoc/>
        public string Identitifer => "GNS";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = values[1];
            var longitude = values[3];
            var cardinalDirectionY = values[2];
            var cardinalDirectionX = values[4];
            var gpsSatellites = values[6];
            var hdop = values[7];

            if (ValidSentence(gpsSatellites)) yield return new TagWithData("GPSsatellites", float.Parse(gpsSatellites));
            if (ValidSentence(hdop)) yield return new TagWithData("HDOP", float.Parse(hdop));

            if (ValidSentence(latitude) && ValidSentence(cardinalDirectionY))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                yield return new TagWithData("Latitude", latitudeDeg);

            }
            if (ValidSentence(longitude) && ValidSentence(cardinalDirectionX))
            {
                var longitudeDeg = ConvertToDegree(longitude);
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;
                yield return new TagWithData("Longitude", longitudeDeg);
            }

            if (ValidSentence(latitude) && ValidSentence(cardinalDirectionY) && ValidSentence(longitude) && ValidSentence(cardinalDirectionX))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                var longitudeDeg = ConvertToDegree(longitude);

                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;

                yield return new TagWithData("Position", new Coordinate
                {
                    Latitude = new Measurement<float>
                    {
                        Value = latitudeDeg
                    },
                    Longitude = new Measurement<float>
                    {
                        Value = longitudeDeg
                    }
                });

            }

        }

        private float ConvertToDegree(string value)
        {
            var length = value.Split(".")[0].Length;
            var _degree = value.Substring(0, length - 2);
            var _decimal = value.Substring(length - 2);
            var result = float.Parse(_degree) + float.Parse(_decimal) / 60;

            return result;
        }
        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

    }
}
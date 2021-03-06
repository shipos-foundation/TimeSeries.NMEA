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
    public class RMC : ISentenceFormat
    {


        /// <inheritdoc/>
        public string Identitifer => "RMC";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            //var latitude = ConvertToDegree(values[2]);
            //var longitude = ConvertToDegree(values[4]);
            //if (values[3] == "S") latitude = -latitude;
            //if (values[5] == "W") longitude = -longitude;

            var latitude = values[2];
            var longitude = values[4];
            var cardinalDirectionY = values[3];
            var cardinalDirectionX = values[5];
            var speedOverGround = values[6];


            if (ValidSentence(speedOverGround)) yield return new TagWithData("SpeedOverGround", (float.Parse(speedOverGround) * 1852) / 3600);
            
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
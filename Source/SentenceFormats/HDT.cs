/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using RaaLabs.TimeSeries.DataTypes;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Heading - True"
    /// </summary>
    public class HDT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "HDT";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {

            var headingTrue = values[0];

            if (ValidSentence(headingTrue)) yield return new TagWithData("HeadingTrue", float.Parse(headingTrue));

        }

        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
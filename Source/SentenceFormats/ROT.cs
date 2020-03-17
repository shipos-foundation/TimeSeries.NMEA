/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using RaaLabs.TimeSeries.DataTypes;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Rate Of Turn"
    /// </summary>
    public class ROT : ISentenceFormat
    {
    
        /// <inheritdoc/>
        public string Identitifer => "ROT";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            return new[] {
                new TagWithData("RateOfTurn", float.Parse(values[0]))
            };
        }
    }
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.TimeSeries.DataTypes;

namespace Dolittle.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Rate Of Turn"
    /// </summary>
    public class ROT : ISentenceFormat
    {
    
        /// <inheritdoc/>
        public string Identitifer => "ROT";

        /// <inheritdoc/>
        public IEnumerable<ParsedResult> Parse(string[] values)
        {
            return new[] {
                new ParsedResult("RateOfTurn", new Measurement<float>
                {
                    Value = float.Parse(values[0])
                })
            };
        }
    }
}
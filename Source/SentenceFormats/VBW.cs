/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using RaaLabs.TimeSeries.DataTypes;

namespace RaaLabs.TimeSeries.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Dual Ground/Water Speed (should probably implement only send if status is valid, see documentation)"
    /// </summary>
    public class VBW : ISentenceFormat
    {
        static readonly Dictionary<int, string> _tags = new Dictionary<int, string>()
                                      {
                                          {0,"LongitudinalSpeedThroughWater"},
                                          {1, "TransverseSpeedThroughWater"},
                                          {3,"LongitudinalSpeedOverGround"},
                                          {4,"TransverseSpeedOverGround"}
                                      };
        /// <inheritdoc/>
        public string Identitifer => "VBW";

        /// <inheritdoc/>
        public IEnumerable<ParsedResult> Parse(string[] values)
        {
            foreach (var (index, name) in _tags)
            {
                if (!string.IsNullOrEmpty(values[index]))
                {
                    yield return new ParsedResult(name, new Measurement<float>
                    {
                        Value = (float.Parse(values[index]) * 1852) / 3600

                    });
                }
            }
        }
    }
}

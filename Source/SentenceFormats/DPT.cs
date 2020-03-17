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
    /// Represents the format of "Depth of Water"
    /// </summary>
    public class DPT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "DPT";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var name = "WaterDepth";
            var waterDepthRelativeToTransducer = values[0];
            var offsetFromTransducer = values[1];
            if (!string.IsNullOrEmpty(waterDepthRelativeToTransducer))
            {


                if (offsetFromTransducer.Contains("-"))
                {
                    name = "DepthBelowKeel";
                }

                yield return new TagWithData(name,
                   float.Parse(waterDepthRelativeToTransducer) + float.Parse(offsetFromTransducer)

                );
            }


        }
    }
}
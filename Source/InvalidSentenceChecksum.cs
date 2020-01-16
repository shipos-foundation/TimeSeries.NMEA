/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace RaaLabs.TimeSeries.NMEA
{
    /// <summary>
    /// Exception that gets thrown if a sentence has invalid checksum
    /// </summary>
    public class InvalidSentenceChecksum : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidSentence"/>
        /// </summary>
        /// <param name="actualChecksum">The invalid checksum </param>
        /// <param name="expectedChecksum">The expected checksum</param>
        /// <param name="sentence">The invalid sentence</param>
        public InvalidSentenceChecksum(byte actualChecksum, byte expectedChecksum, string sentence) : base($"Checksum '{actualChecksum}' is invalid, expecting '{expectedChecksum}' for sentence '{sentence}'. Please refer to the standard for NMEA.")
        {

        }
    }    
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.NMEA
{
    /// <summary>
    /// The protocol to use for connections
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Straight NMEA TCP
        /// </summary>
        Tcp = 1,

        /// <summary>
        /// Straight NMEA UDP 
        /// </summary>
        Udp
    }
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Dolittle.Collections;
using Dolittle.Logging;
using RaaLabs.TimeSeries.Modules;
using RaaLabs.TimeSeries.Modules.Connectors;

namespace RaaLabs.TimeSeries.NMEA
{
    /// <summary>
    /// Represents a <see cref="IAmAPullConnector">pull connector</see> for Modbus
    /// </summary>
    public class Connector : IAmAStreamingConnector
    {
        /// <inheritdoc/>
        public event DataReceived DataReceived = (tag, value, timestamp) => { };
        readonly ConnectorConfiguration _configuration;
        readonly ILogger _logger;
        readonly ISentenceParser _parser;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="configuration">The <see cref="ConnectorConfiguration">configuration</see></param>
        /// <param name="parser"><see cref="ISentenceParser"/> for parsing the NMEA sentences</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            ConnectorConfiguration configuration,
            ISentenceParser parser,
            ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _parser = parser;
        }

        /// <inheritdoc/>
        public Source Name => "NMEA";

        /// <inheritdoc/>
        public void Connect()
        {
            switch (_configuration.Protocol)
            {
                case Protocol.Tcp: ConnectTcp(); break;
                case Protocol.Udp: ConnectUdp(); break;
                default: _logger.Error("Protocol not defined"); break;
            }
        }
        void ConnectTcp()
        {
            while (true)
            {
                try
                {
                    var client = new TcpClient(_configuration.Ip, _configuration.Port);
                    using (var stream = client.GetStream())
                    {
                        var started = false;
                        var skip = false;
                        var sentenceBuilder = new StringBuilder();
                        for (;;)
                        {
                            var result = stream.ReadByte();
                            if (result == -1) break;

                            var character = (char)result;
                            switch (character)
                            {
                                case '$':
                                    started = true;
                                    break;
                                case '\n':
                                    {
                                        skip = true;
                                        var sentence = sentenceBuilder.ToString();
                                        ParseSentence(sentence);
                                        sentenceBuilder = new StringBuilder();
                                    }
                                    break;
                            }
                            if (started && !skip) sentenceBuilder.Append(character);
                            skip = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error while connecting to TCP stream");
                    Thread.Sleep(2000);
                }
            }
        }
        void ConnectUdp()
        {
            while (true)
            {
                try
                {
                    var listenPort = _configuration.Port;
                    using (var listener = new UdpClient(_configuration.Port))
                    {
                        var groupEP = new IPEndPoint(IPAddress.Any, _configuration.Port);
                        try
                        {
                            while (true)
                            {
                                var sentenceBuilder = new StringBuilder();
                                var bytes = listener.Receive(ref groupEP);
                                var sentence = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                                ParseSentence(sentence);
                            }
                        }
                        catch (SocketException ex)
                        {
                            _logger.Error(ex, $"Trouble connecting to socket");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error while connecting to UDP stream");
                    Thread.Sleep(2000);
                }
            }
        }
        void ParseSentence(string sentence)
        {
            if (_parser.CanParse(sentence))
            {
                var identifier = _parser.GetIdentifierFor(sentence);
                var output = _parser.Parse(sentence);
                output.ForEach(_ => DataReceived($"{identifier}.{_.Type}", _.Result, Timestamp.UtcNow));
            }
        }
    }
}
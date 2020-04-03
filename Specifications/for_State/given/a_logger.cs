/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace RaaLabs.TimeSeries.NMEA.for_State.given
{
    public class a_logger
    {
        protected static ILogger logger;
        protected static List<string> errors;

        Establish context = () =>
        {
            errors = new List<string>();
            logger = new MockedLogger();
        };

        private class MockedLogger : ILogger
        {
            public void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "") { }
            public void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "") { }
            public void Information(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "") { }
            public void Warning(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "") { }
            public void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
            {
                errors.Add(message);
            }
            public void Error(Exception exception, string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "")
            {
                errors.Add(message + exception.ToString());
            }
            public void Critical(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string member = "") { }
        }
    }
}
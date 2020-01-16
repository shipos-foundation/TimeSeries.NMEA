/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceParser.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ISentenceFormat>> sentence_formats;
        protected static List<ISentenceFormat> actual_formats;

        Establish context = () =>
        {
            sentence_formats = new Mock<IInstancesOf<ISentenceFormat>>();
            actual_formats = new List<ISentenceFormat>();
            sentence_formats.Setup(_ => _.GetEnumerator()).Returns(() => actual_formats.GetEnumerator());
        };
    }
}
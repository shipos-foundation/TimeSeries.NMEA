using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats
{
    public static class ParsedResultExtensions
    {
        public static void ShouldEmit<T>(this IEnumerable<TagWithData> results, string type, T value)
        {
            if (!results.Any(_ => _.Tag == type && _.Data.Equals(value)))
            {
                throw new SpecificationException($"Expected {type} with {value} to be emitted");
            }
        }
    }
}
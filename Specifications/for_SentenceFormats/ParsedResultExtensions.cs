using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace RaaLabs.TimeSeries.NMEA.for_SentenceFormats
{
    public static class ParsedResultExtensions
    {
        public static void ShouldEmit<T>(this IEnumerable<ParsedResult> results, string type, T value)
        {
            if (!results.Any(_ => _.Type == type &&
                            ((Measurement<T>)_.Result).Value.Equals(value)))
            {
                throw new SpecificationException($"Expected {type} with {value} to be emited");
            }
        }
    }
}
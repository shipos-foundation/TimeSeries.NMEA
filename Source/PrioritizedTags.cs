using Dolittle.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA
{
    /// <summary>
    /// A map of prioritized talkers for all tags
    /// </summary>
    [Name("prioritized")]
    public class PrioritizedTags : ReadOnlyDictionary<string, SourcePriority>, IConfigurationObject
    {
        /// <summary>
        /// 
        /// </summary>
        public PrioritizedTags(IDictionary<string, SourcePriority> priorities) : base(priorities) {}
    }


    /// <summary>
    /// 
    /// </summary>
    public struct SourcePriority
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="threshold"></param>
        public SourcePriority(List<string> priority, long threshold)
        {

            Priority = priority;
            Threshold = threshold;
        }

        /// <summary>
        /// A list of all talkers for a tag, in prioritized order
        /// </summary>
        public List<string> Priority { get; set; }

        /// <summary>
        /// The time before a tag measurement becomes stale
        /// </summary>
        public long Threshold { get; set; }
    }

}

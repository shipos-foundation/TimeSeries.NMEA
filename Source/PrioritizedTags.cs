using Dolittle.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA
{
    /// <summary>
    /// 
    /// </summary>
    [Name("prioritized")]
    public class PrioritizedTags : ReadOnlyDictionary<string, SourcePriority>, IConfigurationObject
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IDictionary<string, List<SourcePriority>> prioritized;
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
        /// <param name="priorities"></param>
        /// <param name="threshold"></param>
        public SourcePriority(List<string> priorities, long threshold)
        {

            Priorities = priorities;
            Threshold = threshold;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Priorities { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Threshold { get; set; }
    }

}

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
    public class PrioritizedTags : ReadOnlyDictionary<string, List<SourcePriority>>, IConfigurationObject
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IDictionary<string, List<SourcePriority>> prioritized;
        /// <summary>
        /// 
        /// </summary>
        public PrioritizedTags(IDictionary<string, List<SourcePriority>> prioritized) : base(prioritized) {}

        /*
        private static Dictionary<string, List<SourcePriority>> InjectTagsToTalkers(IDictionary<string, List<SourcePriority>> source)
        {
            List<SourcePriority> injectTagToTalker(KeyValuePair<string, List<SourcePriority>> prioritiesForTag) => prioritiesForTag.Value.Select(talker => new SourcePriority($"{talker.Id}.{prioritiesForTag.Key}", talker.Threshold)).ToList();
            return source.ToDictionary(tag => tag.Key, tag => injectTagToTalker(tag));
        }
        */
    }


    /// <summary>
    /// 
    /// </summary>
    public struct SourcePriority
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="threshold"></param>
        public SourcePriority(string id, long threshold)
        {
            Id = id;
            Threshold = threshold;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Threshold { get; set; }
    }

}

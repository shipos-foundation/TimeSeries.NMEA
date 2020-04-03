using Dolittle.Lifecycle;
using Dolittle.Logging;
using System.Collections.Generic;
using System.Linq;

namespace RaaLabs.TimeSeries.NMEA
{
    /// <summary>
    /// Singleton class holding all state
    /// </summary>
    [Singleton]
    public class State
    {
        private Dictionary<string, Measurement> _state = new Dictionary<string, Measurement>();
        private Dictionary<string, int> _prioritiesForFullTags;
        private Dictionary<string, long> _timeoutsForFullTags;

        private readonly ILogger _logger;

        /// <summary>
        /// Delegate functions called when state changes for a tag
        /// </summary>
        /// <param name="newState"></param>
        /// <param name="timestamp"></param>
        public delegate void StateChangedDelegate(TagWithData newState, Timestamp timestamp);

        /// <summary>
        /// Event that fires when state changes for a tag
        /// </summary>
        public event StateChangedDelegate StateChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prioritized"></param>
        /// <param name="logger"></param>
        public State(PrioritizedTags prioritized, ILogger logger)
        {
            _logger = logger;

            IEnumerable<(string, int)> talkerPrioritiesForTag(string tag, List<SourcePriority> talkerPriorities) =>
                talkerPriorities.Select((talkerPriority, index) => ($"{tag}.{talkerPriority.Id}", index));

            IEnumerable<(string, long)> talkerTimeoutForTag(string tag, List<SourcePriority> talkerPriorities) =>
                talkerPriorities.Select((talkerPriority) => ($"{tag}.{talkerPriority.Id}", talkerPriority.Threshold));

            _prioritiesForFullTags = prioritized.SelectMany(tag => talkerPrioritiesForTag(tag.Key, tag.Value)).ToDictionary(_ => _.Item1, _ => _.Item2);
            _timeoutsForFullTags = prioritized.SelectMany(tag => talkerTimeoutForTag(tag.Key, tag.Value)).ToDictionary(_ => _.Item1, _ => _.Item2);
        }

        /// <summary>
        /// Add new data point
        /// </summary>
        /// <param name="talker"></param>
        /// <param name="timestamp"></param>
        /// <param name="tagWithData"></param>
        public void DataReceived(string talker, Timestamp timestamp, TagWithData tagWithData)
        {
            string tagWithTalker = $"{tagWithData.Tag}.{talker}";
            string tag = tagWithData.Tag;

            var measurement = new Measurement
            {
                tagWithData = tagWithData,
                timestamp = timestamp,
                tag = tagWithTalker
            };

            bool hasCurrentState = _state.TryGetValue(tag, out Measurement currentState);
            long currentTimestamp = currentState?.timestamp ?? -1;
            long currentTimeout = hasCurrentState ? _timeoutsForFullTags.GetValueOrDefault(currentState.tag, -1) : -1;
            int currentPriority = hasCurrentState ? _prioritiesForFullTags.GetValueOrDefault(currentState.tag, int.MaxValue) : int.MaxValue;
            int thisPriority = _prioritiesForFullTags.GetValueOrDefault(tagWithTalker, int.MaxValue);
            bool hasHigherPriority = thisPriority <= currentPriority;
            bool currentStateStale = (timestamp - currentTimestamp) > currentTimeout;
            bool shouldSetState = !hasCurrentState || hasHigherPriority || currentStateStale;

            bool hasOverlappingTalkersWithoutPriority = thisPriority == int.MaxValue && (hasCurrentState && !tagWithTalker.Equals(currentState.tag));
            
            if(hasOverlappingTalkersWithoutPriority)
            {
                _logger.Information($"{tagWithTalker} is not configured with a priority, despite {currentState.tag} also being a source for this tag.");
            }

            if (shouldSetState)
            {
                _state[tag] = measurement;
                StateChanged.Invoke(tagWithData, timestamp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Measurement
        {
            /// <summary>
            /// 
            /// </summary>
            public TagWithData tagWithData;

            /// <summary>
            /// 
            /// </summary>
            public Timestamp timestamp;

            /// <summary>
            /// 
            /// </summary>
            public string tag;
        }
    }
}

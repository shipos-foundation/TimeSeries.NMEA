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
        private Dictionary<string, long> _timeoutsForTags;

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

            IEnumerable<(string, int)> talkerPrioritiesForTag(string tag, SourcePriority talkerPriorities) =>
                talkerPriorities.Priority.Select((talkerPriority, index) => ($"{tag}.{talkerPriority}", index));

            _prioritiesForFullTags = prioritized.SelectMany(tag => talkerPrioritiesForTag(tag.Key, tag.Value)).ToDictionary(_ => _.Item1, _ => _.Item2);
            _timeoutsForTags = prioritized.ToDictionary(tag => tag.Key, tag => tag.Value.Threshold);
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
                source = tagWithTalker
            };

            long timeout = _timeoutsForTags.GetValueOrDefault(tag);
            bool hasCurrentState = _state.TryGetValue(tag, out Measurement currentState);
            long currentTimestamp = currentState?.timestamp ?? -1;
            int currentPriority = hasCurrentState ? _prioritiesForFullTags.GetValueOrDefault(currentState.source, int.MaxValue) : int.MaxValue;
            int thisPriority = _prioritiesForFullTags.GetValueOrDefault(tagWithTalker, int.MaxValue);
            bool hasHigherPriority = thisPriority <= currentPriority;
            bool currentStateStale = (timestamp - currentTimestamp) > timeout;
            bool shouldSetState = !hasCurrentState || hasHigherPriority || currentStateStale;

            bool hasOverlappingTalkersWithoutPriority = thisPriority == int.MaxValue && (hasCurrentState && !tagWithTalker.Equals(currentState.source));
            
            if(hasOverlappingTalkersWithoutPriority)
            {
                _logger.Information($"{tagWithTalker} is not configured with a priority, despite {currentState.source} also being a source for this tag.");
            }

            if (shouldSetState)
            {
                _logger.Information($"{talker} set {tag} to {tagWithData.Data}");
                _state[tag] = measurement;
                StateChanged.Invoke(tagWithData, timestamp);
            }
        }

        /// <summary>
        /// A state data point at a certain time
        /// </summary>
        public class Measurement
        {
            /// <summary>
            /// The measurement
            /// </summary>
            public TagWithData tagWithData;

            /// <summary>
            /// The timestamp for the measurement
            /// </summary>
            public Timestamp timestamp;

            /// <summary>
            /// The source for the measurement, e.g. "Latitude.GPRPC"
            /// </summary>
            public string source;
        }
    }
}

//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogEngine.Events
{
    /// <summary>
    /// Contains methods for global event aggregation.
    /// </summary>
    public class EventAggregator
    {
        #region - Fields -

        private readonly Dictionary<Type, EventBase> mEvents = new Dictionary<Type, EventBase>();

        #endregion

        #region - Singleton implementation -

        private static EventAggregator _instance;

        /// <summary>
        /// Initialize new instance of EventAggregator.
        /// </summary>
        private EventAggregator()
        {
        }

        /// <summary>
        /// Gets EventAggregator instance.
        /// </summary>
        public static EventAggregator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventAggregator();
                }
                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Gets the single instance of the event managed by this EventAggregator. Multiple calls to this method with the same <typeparamref name="TEventType"/> returns the same event instance.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to get. This must inherit from <see cref="EventBase"/>.</typeparam>
        /// <returns>A singleton instance of an event object of type <typeparamref name="TEventType"/>.</returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            EventBase _existingEvent = null;

            if (!this.mEvents.TryGetValue(typeof(TEventType), out _existingEvent))
            {
                TEventType _newEvent = new TEventType();
                this.mEvents[typeof(TEventType)] = _newEvent;

                return _newEvent;
            }
            else
            {
                return (TEventType)_existingEvent;
            }
        }
    }
 }

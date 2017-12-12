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
    /// Represent base method for composite events.
    /// </summary>
    public abstract class EventBase
    {
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        /// <summary>
        /// Gets list of subscribers.
        /// </summary>
        protected List<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        /// <summary>
        /// Subscribe to event.
        /// </summary>
        /// <param name="subscription">Subscription to add.</param>
        protected void InternalSubscribe(IEventSubscription subscription)
        {
            if (subscription == null) throw new System.ArgumentNullException("eventSubscription");

            lock (Subscriptions)
            {
                _subscriptions.Add(subscription);
            }
        }

        /// <summary>
        /// Unsubscribe.
        /// </summary>
        /// <param name="subscription">Subscription to remove.</param>
        protected void InternalUnsubscribe(IEventSubscription subscription)
        {
            lock (Subscriptions)
            {
                this.Subscriptions.Remove(subscription);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this event has any subscribers.
        /// </summary>
        public bool HasSubscribers
        {
            get { return Subscriptions.Count > 0; }
        }
    }
}

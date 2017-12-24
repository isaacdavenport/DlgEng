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
    /// Defines a class that manages publication and subscription to events.
    /// </summary>
    /// <typeparam name="TPayload">The type of message that will be passed to the subscribers.</typeparam>
    public class CompositeEvent<TPayload> : EventBase
    {
        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="_action">The delegate that gets executed when the event is raised.</param>
        public virtual void Subscribe(Action<TPayload> _action)
        {
            this.InternalSubscribe(new EventSubscription<TPayload>(_action));
        }

        /// <summary>
        /// Removes the first subscriber matching <seealso cref="Action{TPayload}"/> from the subscribers' list.
        /// </summary>
        /// <param name="_subscriber">The <see cref="Action{TPayload}"/> used when subscribing to the event.</param>
        public virtual void Unsubscribe(Action<TPayload> _subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription _eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == _subscriber);
                if (_eventSubscription != null)
                {
                    Subscriptions.Remove(_eventSubscription);
                }
            }
        }

        /// <summary>
        /// Publishes the <see cref="CompositeEvent{TPayload}"/>.
        /// </summary>
        /// <param name="_payload">Message to pass to the subscribers.</param>
        public virtual void Publish(TPayload _payload)
        {
            lock (Subscriptions)
            {
                foreach (var _subscription in Subscriptions)
                {
                    ((Action<TPayload>)_subscription.Target)(_payload);
                }
            }
        }
    }
}

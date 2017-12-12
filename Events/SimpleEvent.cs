//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogEngine.Events
{

    public class SimpleEvent : EventBase
    {
        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is raised.</param>
        public virtual void Subscribe(Action action)
        {
            this.InternalSubscribe(new SimpleEventSubscription(action));
        }

        /// <summary>
        /// Removes the first subscriber matching <seealso cref="Action"/> from the subscribers' list.
        /// </summary>
        /// <param name="subscriber">The <see cref="Action"/> used when subscribing to the event.</param>
        public virtual void Unsubscribe(Action subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription eventSubscription = Subscriptions.Cast<SimpleEventSubscription>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        /// <summary>
        /// Publishes the <see cref="SimpleEvent"/>.
        /// </summary>
        public virtual void Publish()
        {
            lock (Subscriptions)
            {
                foreach (var subscription in Subscriptions)
                {
                    ((Action)subscription.Target)();
                }
            }
        }

    }
}

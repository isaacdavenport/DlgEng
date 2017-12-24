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
        /// <param name="_action">The delegate that gets executed when the event is raised.</param>
        public virtual void Subscribe(Action _action)
        {
            this.InternalSubscribe(new SimpleEventSubscription(_action));
        }

        /// <summary>
        /// Removes the first subscriber matching <seealso cref="Action"/> from the subscribers' list.
        /// </summary>
        /// <param name="_subscriber">The <see cref="Action"/> used when subscribing to the event.</param>
        public virtual void Unsubscribe(Action _subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription _eventSubscription = Subscriptions.Cast<SimpleEventSubscription>().FirstOrDefault(evt => evt.Action == _subscriber);
                if (_eventSubscription != null)
                {
                    Subscriptions.Remove(_eventSubscription);
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
                foreach (var _subscription in Subscriptions)
                {
                    ((Action)_subscription.Target)();
                }
            }
        }

    }
}

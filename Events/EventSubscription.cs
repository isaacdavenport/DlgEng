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
    /// Contains methods for handling event subscriptions.
    /// </summary>
    /// <typeparam name="TPayload">Payload type.</typeparam>
    public class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly Delegate _target;

        /// <summary>
        /// Initializes new instance of EventSubscription class with provided action.
        /// </summary>
        /// <param name="action">Action to execute when event is published.</param>
        public EventSubscription(Action<TPayload> action)
        {
            this._target = action;
        }

        /// <summary>
        /// Gets or sets target method.
        /// </summary>
        public Delegate Target
        {
            get { return _target; }
        }

        /// <summary>
        /// Gets the target <see cref="System.Action{T}"/> that is referenced by the Target property.
        /// </summary>
        /// <value>An <see cref="System.Action{T}"/> or <see langword="null" /> if the referenced target is not alive.</value>
        public Action<TPayload> Action
        {
            get { return (Action<TPayload>)Target; }
        }
    }
}

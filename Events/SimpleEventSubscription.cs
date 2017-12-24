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
    /// Contains methods for subscibing to parameterless events.
    /// </summary>
    public class SimpleEventSubscription : IEventSubscription
    {
        private readonly Delegate _target;

        /// <summary>
        /// Iniitalizes new instance of DefaultEventSubscription class.
        /// </summary>
        /// <param name="_action"></param>
        public SimpleEventSubscription(Action _action)
        {
            this._target = _action;
        }

        /// <summary>
        /// Gets target method.
        /// </summary>
        public Delegate Target
        {
            get { return _target; }
        }

        /// <summary>
        /// Gets the target <see cref="System.Action"/> that is referenced by the Target property.
        /// </summary>
        /// <value>An <see cref="System.Action"/> or <see langword="null" /> if the referenced target is not alive.</value>
        public Action Action
        {
            get { return (Action)Target; }
        }
    }
}

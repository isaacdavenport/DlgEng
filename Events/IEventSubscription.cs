//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogEngine.Events
{

    public interface IEventSubscription
    {
        /// <summary>
        /// Gets target method.
        /// </summary>
        Delegate Target { get; }
    }
}

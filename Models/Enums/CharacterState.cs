//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialogEngine.Models.Enums
{
    /// <summary>
    /// Represents state of character
    /// </summary>
    public enum CharacterState
    {
        On,  
        Off,  //character is deactivated
        Available //character is available for random selection if there are no radios based selection
    }
}

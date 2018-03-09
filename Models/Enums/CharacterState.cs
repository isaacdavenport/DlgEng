//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org


namespace DialogEngine.Models.Enums
{
    /// <summary>
    /// Represents state of character
    /// </summary>
    public enum CharacterState
    {
        ///character is available for random selection if there are no radios based selection
        Available = 0, 
        ///character is forced to be used in dialog
        On = 1,
        ///character is deactivated
        Off = 2  
    }
}

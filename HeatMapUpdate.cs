//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using DialogEngine.ViewModels.Dialog;

namespace DialogEngine
{
    public static class HeatMapUpdate
    {
        #region - Static methods -

        #pragma warning disable 1591
        public static void PrintHeatMap()
        #pragma warning restore 1591
        {
            DialogViewModel.Instance.HeatMapUpdate = SelectNextCharacters.HeatMap;
            DialogViewModel.Instance.Character1Prefix = DialogViewModel.Instance.CharacterCollection[SelectNextCharacters.NextCharacter1].CharacterPrefix;
            DialogViewModel.Instance.Character2Prefix = DialogViewModel.Instance.CharacterCollection[SelectNextCharacters.NextCharacter2].CharacterPrefix;
            DialogViewModel.Instance.RSSIsum = SelectNextCharacters.BigRssi;
            DialogViewModel.Instance.RSSIstable = SelectNextCharacters.RssiStable;
        }
        #endregion
    }

}

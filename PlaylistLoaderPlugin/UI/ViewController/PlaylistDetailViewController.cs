using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistDetailViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistDetailView.bsml";

        [UIComponent("playlistDescription")]
        internal TextPageScrollView songDescription;

        internal void ClearData()
        {
            if (songDescription)
                songDescription.SetText("");
        }

        internal void Initialize(string description)
        {
            songDescription.SetText(description);
        }
    }
}
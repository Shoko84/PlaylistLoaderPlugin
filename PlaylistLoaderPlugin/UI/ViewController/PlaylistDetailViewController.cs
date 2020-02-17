using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using TMPro;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistDetailViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistDetailView.bsml";

        [UIComponent("name")]
        internal TextMeshProUGUI _name;

        [UIComponent("author")]
        internal TextMeshProUGUI _author;

        [UIComponent("playlistDescription")]
        internal TextPageScrollView _songDescription;

        internal void ClearData()
        {
            if (_name)
                _name.SetText("");
            if (_author)
                _author.SetText("");
            if (_songDescription)
                _songDescription.SetText("");
        }

        internal void Initialize(string name, string author, string songDescription)
        {
            _name.SetText("Name - " + name);
            _author.SetText("Author - " + author);
             _songDescription.SetText(songDescription);
        }
    }
}
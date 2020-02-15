using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistsView.bsml";

        [UIComponent("list")]
        public CustomListTableData customListTableData;

        [UIAction("#post-parse")]
        internal void Setup()
        {
            //customListTableData.data.Add()
        }
    }

    //public class PlaylistCellInfo : CustomListTableData.CustomCellInfo
    //{

    //}
}
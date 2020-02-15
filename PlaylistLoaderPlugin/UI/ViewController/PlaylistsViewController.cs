using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using PlaylistLoaderPlugin.Objects;
using PlaylistLoaderPlugin.HarmonyPatches;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistsView.bsml";

        [UIComponent("list")]
        public CustomListTableData customListTableData;

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);
            if (firstActivation)
                PlaylistCollectionOverride.refreshPlaylists();
            InitPlaylistList();
        }
        private void InitPlaylistList()
        {
            for(int i=0; i<CustomPlaylistFileObject.playlists.Count; i++)
                customListTableData.data.Add(new PlaylistCellInfo(CustomPlaylistFileObject.playlists[i]));
            customListTableData.tableView.ReloadData();
        }
    }

    public class PlaylistCellInfo : CustomListTableData.CustomCellInfo
    {
        private CustomPlaylistFileObject _customPlaylistFileObject;
        private Action<CustomListTableData.CustomCellInfo> _callback;

        public PlaylistCellInfo(CustomPlaylistFileObject customPlaylistFileObject) : base(customPlaylistFileObject.customPlaylistSO.collectionName, customPlaylistFileObject.author, customPlaylistFileObject.customPlaylistSO.coverImage.texture)
        {
            _customPlaylistFileObject = customPlaylistFileObject;
        }
    }
}
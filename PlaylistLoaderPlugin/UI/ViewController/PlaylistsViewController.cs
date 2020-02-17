using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using PlaylistLoaderPlugin.Objects;
using PlaylistLoaderPlugin.HarmonyPatches;
using HMUI;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistsView.bsml";
        private int currentIndex = 0;
        private const int PLAYLISTVIEW_SIZE = 7; //How many playlists can be seen at once in the screen
        public Action<int> didSelectPlaylist;

        [UIComponent("list")]
        public CustomListTableData customListTableData;

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);
            if (firstActivation)
            {
                PlaylistCollectionOverride.refreshPlaylists();
                InitPlaylistList(0);
                currentIndex = 0;
            }
            customListTableData.tableView.ClearSelection();
        }
        private void InitPlaylistList(int index)
        {
            int startIndex = index * PLAYLISTVIEW_SIZE;
            int endIndex = startIndex + PLAYLISTVIEW_SIZE > CustomPlaylistFileObject.playlists.Count ? CustomPlaylistFileObject.playlists.Count : startIndex + PLAYLISTVIEW_SIZE;
            customListTableData.data.Clear();
            for(int i=startIndex; i<endIndex; i++)
                customListTableData.data.Add(new PlaylistCellInfo(CustomPlaylistFileObject.playlists[i]));
            customListTableData.tableView.ReloadData();
        }
        internal void refreshPlaylistList()
        {
            InitPlaylistList(currentIndex);
        }
        [UIAction("listSelect")]
        internal void Select(TableView tableView, int row)
        {
            didSelectPlaylist?.Invoke(currentIndex*PLAYLISTVIEW_SIZE + row);
        }
        [UIAction("pageUpPressed")]
        internal void PageUpPressed()
        {
            if(currentIndex>0)
                InitPlaylistList(--currentIndex);
        }
        [UIAction("pageDownPressed")]
        internal void PageDownPressed()
        {
            int playlistsSize = CustomPlaylistFileObject.playlists.Count;
            if ((currentIndex + 1) * PLAYLISTVIEW_SIZE <= playlistsSize - (playlistsSize % PLAYLISTVIEW_SIZE==0?PLAYLISTVIEW_SIZE:playlistsSize%PLAYLISTVIEW_SIZE)) //Allows scrolling if more playlists exist in array.
                InitPlaylistList(++currentIndex);
        }
    }

    public class PlaylistCellInfo : CustomListTableData.CustomCellInfo
    {
        public CustomPlaylistFileObject _customPlaylistFileObject { get; }
        public PlaylistCellInfo(CustomPlaylistFileObject customPlaylistFileObject) : base(customPlaylistFileObject.name, customPlaylistFileObject.author, customPlaylistFileObject.customPlaylistSO.coverImage.texture)
        {
            _customPlaylistFileObject = customPlaylistFileObject;
        }
    }
}
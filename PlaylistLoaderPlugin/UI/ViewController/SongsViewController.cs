using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using PlaylistLoaderPlugin.Objects;
using UnityEngine;
using System.Threading;

namespace PlaylistLoaderPlugin.UI
{
    public class SongsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.SongsView.bsml";

        [UIComponent("list")]
        public CustomListTableData customListTableData;

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);
            if (firstActivation)
            {
                InitSongsList(0);
            }
        }
        public void InitSongsList(int index)
        {
            customListTableData.data.Clear();
            CustomPlaylistFileObject customPlaylistFileObject = CustomPlaylistFileObject.playlists[index];
            for(int i=0; i<customPlaylistFileObject.customPlaylistSO.beatmapLevelCollection.beatmapLevels.Length; i++)
            {
                customListTableData.data.Add(new SongsCellInfo(customPlaylistFileObject.customPlaylistSO.beatmapLevelCollection.beatmapLevels[i]));
            }
            customListTableData.tableView.ReloadData();
            customListTableData.tableView.SelectCellWithIdx(0);
        }
    }

    public class SongsCellInfo : CustomListTableData.CustomCellInfo
    {
        private IPreviewBeatmapLevel _beatmapLevel;
        private CancellationToken cancellationToken;

        public SongsCellInfo(IPreviewBeatmapLevel beatmapLevel) : base(beatmapLevel.songName, beatmapLevel.songAuthorName + " [" + beatmapLevel.levelAuthorName + "]", null)
        {
            _beatmapLevel = beatmapLevel;
            LoadImage();
        }
        protected async void LoadImage()
        {
            Texture2D icon = await _beatmapLevel.GetCoverImageTexture2DAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            base.icon = icon;
        }
    }
}
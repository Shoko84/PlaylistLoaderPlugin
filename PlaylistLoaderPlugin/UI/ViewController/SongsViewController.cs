using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using PlaylistLoaderPlugin.Objects;
using UnityEngine;
using System.Threading;
using System;
using TMPro;

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
            customListTableData.data.Clear();
            customListTableData.tableView.ReloadData();
        }
        public void InitSongsList(CustomPlaylistFileObject customPlaylistFileObject)
        {
            customListTableData.data.Clear();
            for(int i=0; i<customPlaylistFileObject.customPlaylistSO.beatmapLevelCollection.beatmapLevels.Length; i++)
            {
                customListTableData.data.Add(new SongsCellInfo(customPlaylistFileObject.customPlaylistSO.beatmapLevelCollection.beatmapLevels[i], CellDidSetImage));
            }
            customListTableData.tableView.ReloadData();
            customListTableData.tableView.SelectCellWithIdx(0);
        }

        internal void CellDidSetImage()
        {
            customListTableData.tableView.RefreshCellsContent();
        }
    }

    public class SongsCellInfo : CustomListTableData.CustomCellInfo
    {
        private IPreviewBeatmapLevel _beatmapLevel;
        internal CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Action _callback;

        public SongsCellInfo(IPreviewBeatmapLevel beatmapLevel, Action callback) : base(beatmapLevel.songName, beatmapLevel.songAuthorName + " [" + beatmapLevel.levelAuthorName + "]", null)
        {
            _beatmapLevel = beatmapLevel;
            _callback = callback;
            LoadImage();
        }
        protected async void LoadImage()
        {
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Texture2D icon = await _beatmapLevel.GetCoverImageTexture2DAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            base.icon = icon;
            _callback();
        }
    }
}
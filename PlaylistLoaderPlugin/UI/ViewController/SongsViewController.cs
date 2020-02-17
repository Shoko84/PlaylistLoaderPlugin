using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using PlaylistLoaderPlugin.Objects;
using UnityEngine;
using System.Threading;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class SongsViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.SongsView.bsml";
        private CustomPlaylistFileObject _playlist;
        private const int SONGSVIEW_SIZE = 7;
        private int currentIndex = 0;

        [UIComponent("list")]
        public CustomListTableData customListTableData;

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);
            customListTableData.data.Clear();
            _playlist = null;
            customListTableData.tableView.ReloadData();
        }
        public void InitSongsList(CustomPlaylistFileObject playlist)
        {
            _playlist = playlist;
            currentIndex = 0;
            Scroll(currentIndex);
        }
        private void Scroll(int index)
        {
            int size = _playlist.customPlaylistSO.beatmapLevelCollection.beatmapLevels.Length;
            int startIndex = index * SONGSVIEW_SIZE;
            int endIndex = startIndex + SONGSVIEW_SIZE > size ? size : startIndex + SONGSVIEW_SIZE;
            customListTableData.data.Clear();
            for (int i = startIndex; i < endIndex; i++)
            {
                customListTableData.data.Add(new SongsCellInfo(_playlist.customPlaylistSO.beatmapLevelCollection.beatmapLevels[i], CellDidSetImage));
            }
            customListTableData.tableView.ReloadData();
            customListTableData.tableView.SelectCellWithIdx(0);
        }
        internal void CellDidSetImage()
        {
            try
            {
                customListTableData.tableView.RefreshCellsContent();
            }
            catch (Exception) { }
        }
        [UIAction("pageUpPressed")]
        internal void PageUpPressed()
        {
            if(_playlist!=null)
            {
                if (currentIndex > 0)
                    Scroll(--currentIndex);
            }
        }
        [UIAction("pageDownPressed")]
        internal void PageDownPressed()
        {
            if (_playlist!=null)
            {
                int songsSize = _playlist.customPlaylistSO.beatmapLevelCollection.beatmapLevels.Length;
                if ((currentIndex + 1) * SONGSVIEW_SIZE <= songsSize - (songsSize % SONGSVIEW_SIZE == 0 ? SONGSVIEW_SIZE : songsSize % SONGSVIEW_SIZE)) //Allows scrolling if more playlists exist in array.
                    Scroll(++currentIndex);
            }
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
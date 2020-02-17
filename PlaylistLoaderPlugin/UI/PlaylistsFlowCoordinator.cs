using BeatSaberMarkupLanguage;
using HMUI;
using PlaylistLoaderPlugin.Objects;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistsFlowCoordinator : FlowCoordinator
    {
        private NavigationController _playlistsNavigationController;
        private PlaylistsViewController _playlistsViewController;
        private SongsViewController _songsViewController;
        private PlaylistDetailViewController _playlistDetailViewController;

        public void Awake()
        {
            if(_playlistsViewController==null)
            {
                _playlistsViewController = BeatSaberUI.CreateViewController<PlaylistsViewController>();
                _playlistsNavigationController = BeatSaberUI.CreateViewController<NavigationController>();
                _songsViewController = BeatSaberUI.CreateViewController<SongsViewController>();
                _playlistDetailViewController = BeatSaberUI.CreateViewController<PlaylistDetailViewController>();

                _playlistsViewController.didSelectPlaylist += HandleDidSelectPlaylist;
                _playlistDetailViewController.didChangeName += HandleDidChangeName;
                _playlistDetailViewController.didChangeAuthor += HandleDidChangeAuthor;
                _playlistDetailViewController.didChangeDescription += HandleDidChangeDescription;
            }
        }
        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            try
            {
                if (firstActivation)
                {
                    showBackButton = true;
                    ProvideInitialViewControllers(_playlistsNavigationController, _playlistsViewController);
                    PushViewControllerToNavigationController(_playlistsNavigationController, _songsViewController);
                    title = "Playlist Manager";
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex);
            }
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            var mainFlow = BeatSaberMarkupLanguage.BeatSaberUI.MainFlowCoordinator;
            mainFlow.DismissFlowCoordinator(this);

        }
        private void HandleDidSelectPlaylist(int index)
        {
            CustomPlaylistFileObject selectedPlaylist = CustomPlaylistFileObject.playlists[index];
            _songsViewController.InitSongsList(selectedPlaylist);
            SetRightScreenViewController(_playlistDetailViewController);
            _playlistDetailViewController.Initialize(selectedPlaylist.name, selectedPlaylist.author, selectedPlaylist.description, index);
        }
        private void HandleDidChangeName(string name, int index)
        {
            _playlistDetailViewController.setName(name);
            CustomPlaylistFileObject.playlists[index].name = name;
            Logger.log.Critical(CustomPlaylistFileObject.playlists[index].name);
            _playlistsViewController.refreshPlaylistList();
        }
        private void HandleDidChangeAuthor(string author, int index)
        {
            _playlistDetailViewController.setAuthor(author);
            CustomPlaylistFileObject.playlists[index].author = author;
            _playlistsViewController.refreshPlaylistList();
        }
        private void HandleDidChangeDescription(string description, int index)
        {
            _playlistDetailViewController.setDescription(description);
            CustomPlaylistFileObject.playlists[index].description = description;
            _playlistsViewController.refreshPlaylistList();
        }
    }
}
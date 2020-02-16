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

        private void HandleDidSelectPlaylist(CustomPlaylistFileObject selectedPlaylist)
        {
            _songsViewController.InitSongsList(selectedPlaylist);
            SetRightScreenViewController(_playlistDetailViewController);
            _playlistDetailViewController.Initialize(selectedPlaylist.description);
        }
    }
}
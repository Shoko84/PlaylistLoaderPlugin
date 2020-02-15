using BeatSaberMarkupLanguage;
using HMUI;
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
            _playlistsViewController = BeatSaberUI.CreateViewController<PlaylistsViewController>();
            _playlistsNavigationController = BeatSaberUI.CreateViewController<NavigationController>();
            _songsViewController = BeatSaberUI.CreateViewController<SongsViewController>();
            _playlistDetailViewController = BeatSaberUI.CreateViewController<PlaylistDetailViewController>();
            _playlistsViewController.songsViewController = _songsViewController;
            _playlistsViewController.playlistDetailViewController = _playlistDetailViewController;
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
                    SetRightScreenViewController(_playlistDetailViewController);
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
    }
}
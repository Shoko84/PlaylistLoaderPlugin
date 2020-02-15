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
        public void Awake()
        {
            _playlistsViewController = BeatSaberUI.CreateViewController<PlaylistsViewController>();
            _playlistsNavigationController = BeatSaberUI.CreateViewController<NavigationController>();
            _songsViewController = BeatSaberUI.CreateViewController<SongsViewController>();
            _playlistsViewController.songsViewController = _songsViewController;
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
    }
}
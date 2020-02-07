using BeatSaberMarkupLanguage;
using HMUI;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistsFlowCoordinator : FlowCoordinator
    {
        private NavigationController _playlistsNavigationController;
        private PlaylistsViewController _playlistsViewController;
        public void Awake()
        {
            _playlistsViewController = BeatSaberUI.CreateViewController<PlaylistsViewController>();
            _playlistsNavigationController = BeatSaberUI.CreateViewController<NavigationController>();
        }
        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            try
            {
                if (firstActivation)
                {
                    showBackButton = true;
                    ProvideInitialViewControllers(_playlistsNavigationController, _playlistsViewController);
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
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;

namespace PlaylistLoaderPlugin.UI
{
    public class PluginUI : PersistentSingleton<PluginUI>
    {
        public MenuButton playlistsButton;
        internal PlaylistsFlowCoordinator _playlistsflowCoordinator;
        internal void Setup()
        {
            playlistsButton = new MenuButton("Manage Playlists", "Manage Beat Saber Playlists", PlaylistsButtonPressed, true);
            MenuButtons.instance.RegisterButton(playlistsButton);
        }

        internal void PlaylistsButtonPressed()
        {
            PlaylistsButtonFlow();
        }

        internal void PlaylistsButtonFlow()
        {
            if (_playlistsflowCoordinator == null)
                _playlistsflowCoordinator = BeatSaberUI.CreateFlowCoordinator<PlaylistsFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_playlistsflowCoordinator);
        }
    }
}
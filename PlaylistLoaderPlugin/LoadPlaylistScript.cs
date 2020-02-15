using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using PlaylistLoaderPlugin.Objects;

namespace PlaylistLoaderPlugin
{
    internal class LoadPlaylistScript
    {
        internal static CustomPlaylistSO[] load()
        {
            string[] playlistPaths = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "Playlists"), "*.*").Where(p => p.EndsWith(".json") || p.EndsWith(".bplist")).ToArray();
            List<CustomPlaylistSO> playlists = new List<CustomPlaylistSO>();
            for (int i = 0; i < playlistPaths.Length; i++)
            {
                try
                {
                    JObject playlistJSON = JObject.Parse(File.ReadAllText(playlistPaths[i]));
                    CustomPlaylistFileObject customPlaylistFileObject = new CustomPlaylistFileObject(playlistJSON);
                    playlists.Add(customPlaylistFileObject.customPlaylistSO);
                    
                } catch(Exception e)
                {
                    Logger.log.Critical($"Error loading Playlist File: " + playlistPaths[i] + " Exception: " + e.Message);
                }
            }
            return playlists.ToArray();
        }
    }
}

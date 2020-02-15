using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using SongCore;
using System.Linq;
using System;

namespace PlaylistLoaderPlugin.Objects
{
    internal class CustomPlaylistFileObject
    {
        public CustomPlaylistSO customPlaylistSO { get; }
        public string description { get; }

        public CustomPlaylistFileObject(JObject playlistJSON)
        {
            if (playlistJSON["songs"] != null)
            {
                JArray songs = (JArray)playlistJSON["songs"];
                List<IPreviewBeatmapLevel> beatmapLevels = new List<IPreviewBeatmapLevel>();
                for (int j = 0; j < songs.Count; j++)
                {
                    IPreviewBeatmapLevel beatmapLevel = null;
                    string hash = (string)songs[j]["hash"];
                    beatmapLevel = MatchSong(hash);
                    if (beatmapLevel != null)
                        beatmapLevels.Add(beatmapLevel);
                    else
                    {
                        string levelID = (string)songs[j]["levelId"];
                        if (!string.IsNullOrEmpty(levelID))
                        {
                            hash = Collections.hashForLevelID(levelID);
                            beatmapLevel = MatchSong(hash);
                            if (beatmapLevel != null)
                                beatmapLevels.Add(beatmapLevel);
                            else
                                Logger.log.Warn($"Song not downloaded, : {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + "!"))}");
                        }
                        else
                            Logger.log.Warn($"Song not downloaded, : {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + "!"))}");
                    }
                }
                CustomBeatmapLevelCollectionSO customBeatmapLevelCollection = CustomBeatmapLevelCollectionSO.CreateInstance(beatmapLevels.ToArray());
                string playlistTitle = "Untitled Playlist";
                string playlistImage = CustomPlaylistSO.DEFAULT_IMAGE;
                description = "";
                if ((string)playlistJSON["playlistTitle"] != null)
                    playlistTitle = (string)playlistJSON["playlistTitle"];
                if ((string)playlistJSON["image"] != null)
                    playlistImage = (string)playlistJSON["image"];
                if ((string)playlistJSON["playlistDescription"] != null)
                    description = (string)playlistJSON["playlistDescription"];
                customPlaylistSO = CustomPlaylistSO.CreateInstance(playlistTitle, playlistImage, customBeatmapLevelCollection);
            }
        }
        private static IPreviewBeatmapLevel MatchSong(string hash)
        {
            if (!SongCore.Loader.AreSongsLoaded || SongCore.Loader.AreSongsLoading)
            {
                Logger.log.Info("Songs not loaded. Not Matching songs for playlist.");
                return null;
            }
            IPreviewBeatmapLevel x = null;
            try
            {
                if (!string.IsNullOrEmpty(hash))
                    x = SongCore.Loader.CustomLevels.Values.FirstOrDefault(y => string.Equals(y.levelID.Split('_')[2], hash, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception)
            {
                Logger.log.Warn($"Unable to match song with {(string.IsNullOrEmpty(hash) ? " unknown hash!" : ("hash " + hash + " !"))}");
            }
            return x;
        }
    }
}

using System;
using UnityEngine;

namespace PlaylistLoaderPlugin
{	
	internal class CustomBeatmapLevelCollectionSO : PersistentScriptableObject, IBeatmapLevelCollection
	{
		public static CustomBeatmapLevelCollectionSO CreateInstance(IPreviewBeatmapLevel[] beatmapLevels)
		{
			CustomBeatmapLevelCollectionSO customBeatmapLevelCollectionSO = PersistentScriptableObject.CreateInstance<CustomBeatmapLevelCollectionSO>();
			customBeatmapLevelCollectionSO._beatmapLevels = beatmapLevels;
			return customBeatmapLevelCollectionSO;
		}

		public IPreviewBeatmapLevel[] beatmapLevels
		{
			get
			{
				return this._beatmapLevels;
			}
		}

		[SerializeField]
		protected IPreviewBeatmapLevel[] _beatmapLevels;
	}
}
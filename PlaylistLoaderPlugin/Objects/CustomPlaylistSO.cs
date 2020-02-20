﻿using System;
using Polyglot;
using UnityEngine;

namespace PlaylistLoaderPlugin.Objects
{
	public class CustomPlaylistSO : ScriptableObject, IPlaylist, IAnnotatedBeatmapLevelCollection
	{
		public const String DEFAULT_IMAGE = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAABDgAAAQ4CAIAAABjcvvYAAAEv2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6ZXhpZj0iaHR0cDovL25zLmFkb2JlLmNvbS9leGlmLzEuMC8iCiAgICB4bWxuczp0aWZmPSJodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgZXhpZjpQaXhlbFhEaW1lbnNpb249IjEwODAiCiAgIGV4aWY6UGl4ZWxZRGltZW5zaW9uPSIxMDgwIgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMTA4MCIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMTA4MCIKICAgdGlmZjpSZXNvbHV0aW9uVW5pdD0iMiIKICAgdGlmZjpYUmVzb2x1dGlvbj0iNzIuMCIKICAgdGlmZjpZUmVzb2x1dGlvbj0iNzIuMCIKICAgcGhvdG9zaG9wOkNvbG9yTW9kZT0iMyIKICAgcGhvdG9zaG9wOklDQ1Byb2ZpbGU9InNSR0IgSUVDNjE5NjYtMi4xIgogICB4bXA6TW9kaWZ5RGF0ZT0iMjAyMC0wMi0yMFQwMToxMjozNCswNDowMCIKICAgeG1wOk1ldGFkYXRhRGF0ZT0iMjAyMC0wMi0yMFQwMToxMjozNCswNDowMCI+CiAgIDx4bXBNTTpIaXN0b3J5PgogICAgPHJkZjpTZXE+CiAgICAgPHJkZjpsaQogICAgICBzdEV2dDphY3Rpb249InByb2R1Y2VkIgogICAgICBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZmZpbml0eSBQaG90byAoU2VwIDI2IDIwMTkpIgogICAgICBzdEV2dDp3aGVuPSIyMDIwLTAyLTIwVDAxOjEyOjM0KzA0OjAwIi8+CiAgICA8L3JkZjpTZXE+CiAgIDwveG1wTU06SGlzdG9yeT4KICA8L3JkZjpEZXNjcmlwdGlvbj4KIDwvcmRmOlJERj4KPC94OnhtcG1ldGE+Cjw/eHBhY2tldCBlbmQ9InIiPz6fGddlAAABgmlDQ1BzUkdCIElFQzYxOTY2LTIuMQAAKJF1kd8rg1EYxz/baJqJGuXCxdK4Ghm1uFG2hJLWTBluttd+qP14e98tLbfK7YoSN35d8Bdwq1wrRaTkyoVr4ga9ntdWk+w5Pef5nO85z9M5zwFrJKNk9YYByOYKWngi4J6PLrjtT1hx0YEDX0zR1bFQaJq69n6LxYzXfWat+uf+teblhK6ApUl4VFG1gvCk8PRqQTV5S7hdSceWhU+EvZpcUPjG1OMVfjY5VeFPk7VIOAjWNmF36hfHf7GS1rLC8nI82UxRqd7HfIkzkZubldgt3oVOmAkCuJlinCB+fIzI7KePQfplRZ38gZ/8GfKSq8isUkJjhRRpCnhFLUr1hMSk6AkZGUpm///2VU8ODVaqOwPQ+GgYrz1g34SvsmF8HBjG1yHYHuA8V8vP78Pwm+jlmubZg9Z1OL2oafFtONuAzns1psV+JJu4NZmEl2NoiYLrChyLlZ5V9zm6g8iafNUl7OxCr5xvXfoGUW9n3CW3yyIAAAAJcEhZcwAACxMAAAsTAQCanBgAACAASURBVHic7N1ZjGRZnuf1//+ce21z8yUiPJaMzMjMquzq2qsZZkazFGpmUDOa7ukZJBCCBx5YhUAgIV4QPCF4AR7gZV55QIgHJJYBsQkx0jDAzHT1Uj29VldmZWXlFhmL7+5mdpdz/jxcM3Nz29zc3CPiRtb3U6ksT3O751676/937rnmKnJbAAAAAKBO3KteAAAAAACYRlABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1Q1ABAAAAUDsEFQAAAAC1k7zqBQCAleirXgAAAOrPXvUC3CCCCoArIzMA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aWH/s8/FxEwmW9t9440k9aM9c1gCq2jeH/hG6rzP+4OyKJobHRE92tvb2No8Ozqptt2tu3fywaDRbnmfHD191tneSpvNw6fPtu/eKcvQOzrubG0mSdo/PU0aadJoHD173tzotDobtx8++Nqf+u4P/6+/U4ZYZJlPErN4/GyvvbXZaDZP9va7d25Ve2mj00mbjf7xiYl0NrtZv//1P/NnHrz3lScffDjond375tfDWe/9H/yWiAwG/ZPn+93bt0Wkd3TU2do2i4PT07TZTFvNwdnp1u7db/9j3z948kUY5LffeWtwcvLk/Z88+vY3groPf/Dbu199dPp8f3B8+vzzL9rbW4OT05APNu7shiyLIXz7l7/f3tr+yW/85s7DB3cePfrod37ovH/0S9959tNPjp5+8d6f+3Mxy46fPeveue3b7ec/+/jws8eD/iDr9++/++iNr39tcHJ28PHHRZa99Z3v7n3ySf/o6OG3v3m2t3/85OnOGw9cI93/2Sdps7n15oPDT78os+zBN79WnPV/4TvfMq8/+ge/ffz86cad241O5+izL1T19rtvHX32NO/3Nu/t5oMyOz5qbG4kjWZ/f983Gt37927f2vnGd7/XfetBKMLp8z2nsXd88n//D/+zibS3t3pHR07UN1sWy1gGca61uZn3+yHLXeL/1D/5l+/evy+iGzvbg+MTEens3u4dHoSyVJMP//BHH/zOPwyhbG5tmVhxfJJ0Oo1ut/f06e4bb/zpv/5rzWbaOzlsbnR92swPTgoNzjsnYmUUldbWdnF6KiY/++Anf/j//H8mokmiSfLVr//CL/7S9zRtmIUiRu+8tpvl6amPFtSrEzFVpx/83h/94W/9jokd7R9+/P5P8qKovtRr/KhJHO3442/u0tG9Fx3FlTiqP8dXUjcRVyY7mkc/LDxkXwJd447KpSllVE+c3znRiVjiRfzoVzM3WCQZDdzUi22uspKuuhbH18vrDYi65Htyporv8YuzXbyX9rOOm1plfNHc1qYGRM1tf+7kbsGA2olceiGiqIiofuuNr/z6r//1f+Sv/YrrdprqMuec2cCF/+hf/7eefPZp2mr1j0+ybDCv4aoV3dm5XeZZdFqeDZqbG4OT0+7u7bPDo2ZnI8aQpGnW6zeajSgyODou7LwqevDO27tvPvyTasQMygAAIABJREFUv/+bjSTVhk83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";

		public static CustomPlaylistSO CreateInstance(String name, String coverImage, CustomBeatmapLevelCollectionSO beatmapLevelCollection)
		{
			CustomPlaylistSO customPlaylistSO = ScriptableObject.CreateInstance<CustomPlaylistSO>();
			customPlaylistSO._playListLocalizedName = name;
			byte[] imageBytes;
			try
			{
				imageBytes = Convert.FromBase64String(coverImage.Substring(coverImage.IndexOf(",") + 1));
			}
			catch (Exception)
			{
				imageBytes = Convert.FromBase64String(DEFAULT_IMAGE.Substring(DEFAULT_IMAGE.IndexOf(",") + 1));
			}
			Texture2D tex = new Texture2D(2, 2);
			tex.LoadImage(imageBytes);
			customPlaylistSO._coverImage = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			customPlaylistSO._beatmapLevelCollection = beatmapLevelCollection;
			return customPlaylistSO;
		}

		public string collectionName
		{
			get
			{
				return Localization.Get(_playListLocalizedName);
			}
			set
			{
				_playListLocalizedName = value;
			}
		}

		public Sprite coverImage
		{
			get
			{
				return this._coverImage;
			}
		}

		public IBeatmapLevelCollection beatmapLevelCollection
		{
			get
			{
				return this._beatmapLevelCollection;
			}
		}

		[SerializeField]
		protected string _playListLocalizedName;

		[SerializeField]
		protected Sprite _coverImage;

		[SerializeField]
		protected CustomBeatmapLevelCollectionSO _beatmapLevelCollection;
	}
}

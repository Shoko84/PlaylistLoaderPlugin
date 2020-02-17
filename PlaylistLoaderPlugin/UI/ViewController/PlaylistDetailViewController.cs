using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Attributes;
using TMPro;
using BeatSaberMarkupLanguage.Components;
using UnityEngine;
using System;

namespace PlaylistLoaderPlugin.UI
{
    public class PlaylistDetailViewController : BSMLResourceViewController
    {
        public override string ResourceName => "PlaylistLoaderPlugin.UI.BSML.PlaylistDetailView.bsml";
        public Action<string,int> didChangeName;
        public Action<string,int> didChangeAuthor;
        public Action<string,int> didChangeDescription;
        private int _index = 0;

        [UIComponent("name")]
        internal TextMeshProUGUI _name;

        [UIComponent("author")]
        internal TextMeshProUGUI _author;

        [UIComponent("playlistDescription")]
        internal TextPageScrollView _description;

        [UIValue("nameValue")]
        internal string NameValue = "";

        [UIValue("authorValue")]
        internal string AuthorValue = "";

        [UIValue("descriptionValue")]
        internal string DescriptionValue = "";

        internal void ClearData()
        {
            Initialize("", "", "", _index);
        }
        internal void Initialize(string name, string author, string description, int index)
        {
            setName(name);
            setAuthor(author);
            setDescription(description);
            _index = index;
        }

        [UIAction("namePressed")]
        internal void namePressed(string name)
        {
            didChangeName?.Invoke(name, _index);
        }

        [UIAction("authorPressed")]
        internal void AuthorPressed(string author)
        {
            didChangeAuthor?.Invoke(author, _index);
        }

        [UIAction("descriptionPressed")]
        internal void DescriptionPressed(string description)
        {
            didChangeDescription?.Invoke(description, _index);
        }

        internal void setName(string name)
        {
            NameValue = name;
            _name.SetText("Name - " + name);
        }

        internal void setAuthor(string author)
        {
            AuthorValue = author;
            _author.SetText("Author - " + author);
        }

        internal void setDescription(string description)
        {
            DescriptionValue = description;
            _description.SetText(description);
        }
    }
}
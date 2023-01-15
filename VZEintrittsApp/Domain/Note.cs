﻿using Prism.Mvvm;

namespace VZEintrittsApp.Domain
{
    public class Note : BindableBase
    {
        private string text;
        public string Text
        {
            get => text;
            set
            {
                if (value != text) SetProperty(ref text, value);
            }
        }
    }
}

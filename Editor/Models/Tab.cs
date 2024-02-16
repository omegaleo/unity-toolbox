using System;
using UnityEngine;

namespace Omega_Leo_Toolbox.Editor.Models
{
    public class Tab
    {
        public string ButtonName;
        public Texture2D Icon;
        public Action Action;

        public Tab(string buttonName, Texture2D icon, Action action)
        {
            ButtonName = buttonName;
            Icon = icon;
            Action = action;
        }
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OmegaLeo.Toolbox.Editor.Settings
{
    public class GithubSettingsProvider : SettingsProvider
    {
        private const string TOKEN_OPTION_PREF = "GithubToken";
        private const string USERNAME_OPTION_PREF = "GithubUsername";
        private const string REPOSITORY_OPTION_PREF = "GithubRepo";
        private const string TAG_FILTER_OPTION_PREF = "GithubTagFilter";
        private const string TAG_ORDER_OPTION_PREF = "GithubTagOrder";

        public static string GithubToken
        {
            get
            {
                return EditorPrefs.GetString(TOKEN_OPTION_PREF);
            }
            set
            {
                EditorPrefs.SetString(TOKEN_OPTION_PREF, value);
            }
        }
        
        public static string GithubUsername
        {
            get
            {
                return EditorPrefs.GetString(USERNAME_OPTION_PREF);
            }
            set
            {
                EditorPrefs.SetString(USERNAME_OPTION_PREF, value);
            }
        }
        
        public static string GithubRepo
        {
            get
            {
                return EditorPrefs.GetString(REPOSITORY_OPTION_PREF);
            }
            set
            {
                EditorPrefs.SetString(REPOSITORY_OPTION_PREF, value);
            }
        }
        
        public static string GithubTagFilter
        {
            get
            {
                return EditorPrefs.GetString(TAG_FILTER_OPTION_PREF);
            }
            set
            {
                EditorPrefs.SetString(TAG_FILTER_OPTION_PREF, value);
            }
        }
        
        public static string GithubTagOrder
        {
            get
            {
                return EditorPrefs.GetString(TAG_ORDER_OPTION_PREF);
            }
            set
            {
                EditorPrefs.SetString(TAG_ORDER_OPTION_PREF, value);
            }
        }
        
        public GithubSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }

        public override void OnGUI(string searchContext)
        {
            base.OnGUI(searchContext);

            GUILayout.Space(20f);

            string token = GithubToken;
            string value = EditorGUILayout.PasswordField("Token", token);

            if (!token.Equals(value))
            {
                GithubToken = value;
            }
            
            string username = GithubUsername;
            string value1 = EditorGUILayout.TextField("Username", username);

            if (!username.Equals(value1))
            {
                GithubUsername = value1;
            }
            
            string repo = GithubRepo;
            string value2 = EditorGUILayout.TextField("Repository", repo);

            if (!repo.Equals(value2))
            {
                GithubRepo = value2;
            }
            
            string tags = GithubTagFilter;
            string value3 = EditorGUILayout.TextField("Tags to show (Comma Separated)", tags);

            if (!tags.Equals(value3))
            {
                GithubTagFilter = value3;
            }
            
            string tagOrder = GithubTagOrder;
            string value4 = EditorGUILayout.TextField("Tag Order (Comma Separated)", tagOrder);

            if (!tagOrder.Equals(value4))
            {
                GithubTagOrder = value4;
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new GithubSettingsProvider("Omega Leo/Github Integration", SettingsScope.Project);
        }
    }
}
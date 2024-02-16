using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Omega_Leo_Toolbox.Editor.Models;
using OmegaLeo.Toolbox.Editor.Helpers;
using OmegaLeo.Toolbox.Editor.Models;
using OmegaLeo.Toolbox.Editor.Settings;
using OmegaLeo.Toolbox.Runtime.Extensions;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace OmegaLeo.Toolbox.Editor.Windows
{
    public class GithubWindow : EditorWindow
    {
        private string _token, _username, _repo;
        private int _currentTab = 0;
        private List<Issue> _issues = new List<Issue>();
        private Vector3 _scroll = Vector3.zero;
        
        [MenuItem("Omega Leo/Windows/Github")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GithubWindow window = (GithubWindow)EditorWindow.GetWindow(typeof(GithubWindow), false, "Github Integration");
            window.name = "Github Integration";
            window.titleContent = EditorGuiLayoutHelper.GetIconContent("Github Integration", "TestPassed");
            window.Show();
        }

        private void SetCredentials()
        {
            _token = GithubSettingsProvider.GithubToken;
            _username = GithubSettingsProvider.GithubUsername;
            _repo = GithubSettingsProvider.GithubRepo;
        }
        
        private void OnFocus()
        {
            SetCredentials();
        }

        private async void OnGUI()
        {
            if (GUILayout.Button("Reload credentials"))
            {
                SetCredentials();
            }

            _currentTab = EditorGuiLayoutHelper.TabbedGroup(_currentTab, new List<Tab>()
            {
                new Tab("Issues", EditorGuiLayoutHelper.GetIconTexture("TestPassed"), async () => { await IssuesTab();})
            });
        }

        private async Task UpdateLabels(string url, List<string> labels)
        {
            dynamic request = new
            {
                labels = labels
            };
            
            using var www = UnityWebRequest.Put(url, JsonConvert.SerializeObject(request));
            www.method = "PATCH";
            www.SetRequestHeader("Authorization", $"Bearer {_token}");
            www.SetRequestHeader("Content-Type", "application/json");

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var jsonResponse = www.downloadHandler.text;

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed: {www.error}");
            }
        }
        
        private async Task<string> CallAPI(string endpoint)
        {
            var url = $"https://api.github.com/repos/{_username}/{_repo}/{endpoint}";
            
            using var www = UnityWebRequest.Get(url);
            www.SetRequestHeader("Authorization", $"Bearer {_token}");
            www.SetRequestHeader("Content-Type", "application/json");

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var jsonResponse = www.downloadHandler.text;

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed: {www.error}");
            }

            return jsonResponse;
        }
        
        private async Task IssuesTab()
        {
            if (!_issues.Any() || GUILayout.Button("Refresh list"))
            {
                await RefreshIssueList();
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll, new GUILayoutOption[] { GUILayout.Height(position.height - 150), GUILayout.Width(position.width) });
            foreach (var issue in _issues)
            {
                issue.GenerateIssueBox();
                
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Open"))
                {
                    Process.Start(issue.Html_Url);
                }

                if (issue.CanGoBack())
                {
                    if (GUILayout.Button("←"))
                    {
                        issue.GoBack();
                        await UpdateLabels(issue.Url, issue.Labels.Select(x => x.Name).ToList());
                        await RefreshIssueList();
                    }
                }
            
                if (issue.CanGoForward())
                {
                    if (GUILayout.Button("→"))
                    {
                        issue.GoForward();
                        await UpdateLabels(issue.Url, issue.Labels.Select(x => x.Name).ToList());
                        await RefreshIssueList();
                    }
                }
            
                EditorGUILayout.EndHorizontal();
                
                EditorGuiLayoutHelper.HorizontalLine(Color.gray);
            }
            EditorGUILayout.EndScrollView();
        }

        private async Task RefreshIssueList()
        {
            var json = await CallAPI("issues");
            _issues = JsonConvert.DeserializeObject<List<Issue>>(json);

            if (GithubSettingsProvider.GithubTagFilter.IsNotNullOrEmpty())
            {
                _issues = _issues.Where(x =>
                    x.Labels.Any(y => GithubSettingsProvider.GithubTagFilter.Contains(y.Name))).ToList();
            }
        }
    }
}